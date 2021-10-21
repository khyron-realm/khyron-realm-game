using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;
using Save;
using CountDown;
using Networking.Headquarters;


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
        #endregion

        #region "Awake & Start"
        private void Awake()
        {
            _robotManager.OnButtonPressed += DisplayRobotToUpgrade;
            _upgradeButton.onClick.AddListener(UpgradeRobot);

            _timer.TimeTextState(false);

            HeadquartersManager.OnUpgradingAccepted += UpgradingAccepted;
            HeadquartersManager.OnUpgradingRejected += UpgradingRejected;

            HeadquartersManager.OnFinishUpgradingAccepted += FinishedUpgrading;

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
            print(robot._robotId);
            UpgradingMethod((GameDataValues.Robots[_selectedRobot._robotId].UpgradeTime * 60) - TimeTillFinish(task.StartTime));
        }       
        public void UpgradeRobot()
        {
            HeadquartersManager.UpgradingRequest(_selectedRobot._robotId, DateTime.UtcNow);
        }


        #region "Upgrading handlers"
        //1--> in progress
        //2--> not enough resources
        private void UpgradingAccepted()
        {
            UpgradingMethod(GameDataValues.Robots[_selectedRobot._robotId].UpgradeTime * 60);
        }
        private void UpgradingRejected(byte errorId)
        {
            print("Upgrade rejected");
        }
        #endregion


        private void UpgradingMethod(long time)
        {
            _upgradeButton.enabled = false;

            _robotManager.MakeAllButtonsInactive();

            _timer.AddTime((int)time);
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

            HeadquartersManager.FinishUpgradingRequest(_selectedRobot._robotId);
        }
        private void FinishedUpgrading()
        {
            _timer.TimeTextState(false);
            _upgradeButton.enabled = true;
            _robotManager.MakeAllButtonsActive();
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

            HeadquartersManager.OnUpgradingAccepted -= UpgradingAccepted;
            HeadquartersManager.OnUpgradingRejected -= UpgradingRejected;

            HeadquartersManager.OnFinishUpgradingAccepted -= FinishedUpgrading;

            ManageTasks.OnUpgradingWorking -= UpgradeInProgress;
        }
    }
}