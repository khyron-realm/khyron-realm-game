using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;
using CountDown;
using Levels;
using Networking.Headquarters;
using PlayerDataUpdate;
using AuxiliaryClasses;


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
        public static event Action<byte> OnRobotSelected;

        #endregion


        #region "Awake & Start"
        private void Awake()
        {
            _robotManager.OnButtonPressed += DisplayRobotToUpgrade;
            _upgradeButton.onClick.AddListener(UpgradeRobot);

            _timer.TimeTextState(false);

            HeadquartersManager.OnPlayerDataReceived += ShowFirstRobot;
            HeadquartersManager.OnUpgradingError += UpgradingError;

            PlayerDataOperations.OnEnergyModified += UpgradeCompatible;
            PlayerDataOperations.OnRobotUpgraded += RobotUpgradeSendFinished;

            ManageTasks.OnUpgradingWorking += UpgradeInProgress;
        }


        private void ShowFirstRobot()
        {
            DisplayRobotToUpgrade(RobotsManager.robots[0]);
        }
        #endregion


        #region "Upgrading"
        private void UpgradeInProgress(BuildTask task, RobotSO robot)
        {
            _selectedRobot = robot;
            UpgradingMethod((LevelMethods.RobotUpgradeTime(HeadquartersManager.Player.Robots[robot.RobotId].Level) * 60) - AuxiliaryMethods.TimeTillFinishEnd(task.StartTime), LevelMethods.RobotUpgradeTime(HeadquartersManager.Player.Robots[robot.RobotId].Level) * 60);
        }       
        public void UpgradeRobot()
        {
            PlayerDataOperations.PayEnergy(-(int)LevelMethods.RobotUpgradeCost(HeadquartersManager.Player.Level, _selectedRobot.RobotId), OperationsTags.UPGRADING_ROBOTS);         
        }
        private void UpgradeCompatible(byte tag)
        {
            if (OperationsTags.UPGRADING_ROBOTS != tag) return;
            
            HeadquartersManager.UpgradingRequest(_selectedRobot.RobotId, DateTime.UtcNow, HeadquartersManager.Player.Energy);
            UpgradingMethod(LevelMethods.RobotUpgradeTime(HeadquartersManager.Player.Robots[_selectedRobot.RobotId].Level) * 60);           
        }
        private void UpgradingMethod(long time, int maxValue = 0)
        {
            _upgradeButton.enabled = false;

            _robotManager.MakeAllButtonsInactive();

            _timer.AddTime((int)time);

            if (maxValue == 0)
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
        #endregion


        #region "Upgrading handlers"
        private void UpgradingError(byte errorId)
        {
            print("Upgrade rejected");
        }
        #endregion


        #region "Show Robot To Upgrade"
        /// <summary>
        /// Show the robot to upgrade in the right [image + text]
        /// </summary>
        /// <param name="robot"></param>
        private void DisplayRobotToUpgrade(RobotSO robot)
        {
            _selectedRobot = robot;

            int temp = GetInfoLevel();

            _displayStatsImage.sprite = robot.RobotLevel[temp].upgradeImage;
            _nameOfTheRobot.text = robot.NameOfTheRobot;

            OnRobotSelected?.Invoke(robot.RobotId);
        }
        private int GetInfoLevel()
        {
            //return HeadquartersManager.Player.Robots[_selectedRobot.RobotId].Level;
            return 0;
        }
        #endregion

         
        #region "Upgrading procedure"
        private IEnumerator Upgrading(int time)
        {
            int temp = 0;
            while(temp < time)
            {
                temp += 1;
                yield return _timer.ActivateTimer();
            }

            PlayerDataOperations.UpgradeRobot(_selectedRobot.RobotId, OperationsTags.UPGRADING_ROBOTS);     
        }
        private void RobotUpgradeSendFinished(byte tag)
        {
            if (OperationsTags.UPGRADING_ROBOTS != tag) return;
            
            PlayerDataOperations.ExperienceUpdate(10, 0);
            HeadquartersManager.FinishUpgradingRequest(_selectedRobot.RobotId, HeadquartersManager.Player.Robots[_selectedRobot.RobotId], HeadquartersManager.Player.Experience);

            _timer.TimeTextState(false);
            _timer.SetMaxValueForTime(1);

            _upgradeButton.enabled = true;
            _robotManager.MakeAllButtonsActive();                     
        }
        #endregion


        private void OnDestroy()
        {
            _robotManager.OnButtonPressed -= DisplayRobotToUpgrade;
            _upgradeButton.onClick.RemoveAllListeners();

            HeadquartersManager.OnPlayerDataReceived -= ShowFirstRobot;
            HeadquartersManager.OnUpgradingError -= UpgradingError;

            PlayerDataOperations.OnEnergyModified -= UpgradeCompatible;
            PlayerDataOperations.OnRobotUpgraded -= RobotUpgradeSendFinished;

            ManageTasks.OnUpgradingWorking -= UpgradeInProgress;
        }
    }
}