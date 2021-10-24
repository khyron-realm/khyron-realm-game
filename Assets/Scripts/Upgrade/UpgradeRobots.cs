using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;
using Save;
using CountDown;
using Networking.Headquarters;
using PlayerDataUpdate;


namespace Manager.Upgrade
{
    public class UpgradeRobots : MonoBehaviour
    {
        #region "Input data" 
        [SerializeField] private Image _displayStatsImage;
        [SerializeField] private Text _nameOfTheRobot;
        [SerializeField] private Button _upgradeButton;

        [SerializeField] private RobotsManagerUI _robotManager;
        [SerializeField] private Timer _timer;
        #endregion

        #region "Private members"

        private RobotSO _selectedRobot;

        private static byte Tag = 1;

        #endregion

        #region "Awake & Start"
        private void Awake()
        {
            _robotManager.OnButtonPressed += DisplayRobotToUpgrade;
            _upgradeButton.onClick.AddListener(UpgradeRobot);

            _timer.TimeTextState(false);

            HeadquartersManager.OnUpgradingError += UpgradingError;

            PlayerDataOperations.OnEnergyModified += UpgradeCompatible;
            PlayerDataOperations.OnRobotUpgraded += RobotUpgradeSendFinished;

            ManageTasks.OnUpgradingWorking += UpgradeInProgress;
        }
        private void Start()
        {
            DisplayRobotToUpgrade(RobotsManager.robots[0]);
        }
        #endregion


        private void UpgradeInProgress(BuildTask task, RobotSO robot)
        {
            _selectedRobot = robot;
            UpgradingMethod((GameDataValues.Robots[_selectedRobot._robotId].UpgradeTime * 60) - TimeTillFinish(task.StartTime), GameDataValues.Robots[_selectedRobot._robotId].UpgradeTime * 60);
        }       
        public void UpgradeRobot()
        {
            PlayerDataOperations.PayEnergy(-GameDataValues.Robots[_selectedRobot._robotId].UpgradePrice, Tag);         
        }
        private void UpgradeCompatible(byte tag)
        {
            if(Tag == tag)
            {
                HeadquartersManager.UpgradingRequest(_selectedRobot._robotId, DateTime.UtcNow, HeadquartersManager.Player.Energy);
                UpgradingMethod(GameDataValues.Robots[_selectedRobot._robotId].UpgradeTime * 60);
            }
        }


        #region "Upgrading handlers"
        private void UpgradingError(byte errorId)
        {
            print("Upgrade rejected");
        }
        #endregion


        private void UpgradingMethod(long time, int maxValue = 0)
        {
            _upgradeButton.enabled = false;

            _robotManager.MakeAllButtonsInactive();

            _timer.AddTime((int)time);

            if(maxValue == 0)
            {
                _timer.SetMaxValueForTime((int)time);
            }
            else
            {
                _timer.SetMaxValueForTime(maxValue);
            }
           
            _timer.TimeTextState(true);
            StartCoroutine(Upgrading((int)time));           
        }


        /// <summary>
        /// Show the robot to upgrade in the right [image + text]
        /// </summary>
        /// <param name="robot"></param>
        private void DisplayRobotToUpgrade(RobotSO robot)
        {
            _selectedRobot = robot;

            int temp = GetInfoLevel();

            _displayStatsImage.sprite = robot.robotLevel[temp].upgradeImage;
            _nameOfTheRobot.text = robot.nameOfTheRobot;
        }
        private int GetInfoLevel()
        {
            return RobotsManager.robotsData[_selectedRobot.nameOfTheRobot.ToString()].RobotLevel;
        }


        private IEnumerator Upgrading(int time)
        {
            int temp = 0;
            while(temp < time)
            {
                temp += 1;
                yield return _timer.ActivateTimer();
            }

            PlayerDataOperations.UpgradeRobot(_selectedRobot._robotId, Tag);     
        }


        private void RobotUpgradeSendFinished(byte tag)
        {
            if(Tag == tag)
            {
                HeadquartersManager.FinishUpgradingRequest(_selectedRobot._robotId, HeadquartersManager.Player.Robots[_selectedRobot._robotId]);

                _timer.TimeTextState(false);
                _upgradeButton.enabled = true;
                _robotManager.MakeAllButtonsActive();
            }          
        }


        private static int TimeTillFinish(long time)
        {
            DateTime startTime = DateTime.FromBinary(time);
            DateTime now = DateTime.UtcNow;

            int timeRemained = (int)now.Subtract(startTime).TotalSeconds;
            return timeRemained;
        }


        private void OnDestroy()
        {
            _robotManager.OnButtonPressed -= DisplayRobotToUpgrade;

            HeadquartersManager.OnUpgradingError -= UpgradingError;
            PlayerDataOperations.OnRobotUpgraded += UpgradeCompatible;
            ManageTasks.OnUpgradingWorking -= UpgradeInProgress;
        }
    }
}