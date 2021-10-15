using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;
using Manager.Store;
using CountDown;
using Networking.Game;
using Networking.GameElements;


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
        private Robot _selectedRobot;
        private RobotLevel _curentLevelOfTheRobot;
        #endregion


        private void Awake()
        {
            _robotManager.OnButtonPressed += DisplayRobotToUpgrade;
            _upgradeButton.onClick.AddListener(UpgradeRobot);

            _timer.TimeTextState(false);

            UnlimitedPlayerManager.OnUpgradingAccepted += UpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingRejected += UpgradingRejected;

           // ManageTasks.OnUpgradingWorking += UpgradeInProgress;
        }
        private void Start()
        {
            DisplayRobotToUpgrade(RobotsManager.robots[0]);
        }


        private void UpgradeInProgress(BuildTask task, Robot robot)
        {
            _selectedRobot = robot;      
            UpgradingAccepted(TimeTillFinish(task.EndTime));
        }
        private static long TimeTillFinish(long time)
        {
            DateTime finalTime = DateTime.FromBinary(time);
            DateTime now = DateTime.Now;

            long timeTillEnd = Mathf.Abs(finalTime.Second - now.Second);
            return timeTillEnd;
        }


        /// <summary>
        /// Upgrade robot method
        /// </summary>
        public void UpgradeRobot()
        {
            UnlimitedPlayerManager.UpgradingRequest(_selectedRobot._robotId);
        }


        /// <summary>
        /// If Upgrading is good proced
        /// </summary>
        /// <param name="time"></param>
        private void UpgradingAccepted(long time)
        {
            print("---- Upgrading working ----");
            //UpgradingMethod(TimeTillFinish(time));
        }
        private void UpgradingRejected(byte errorId)
        {
            print("---- upgrading rejected ----");
        }


        private void UpgradingMethod(long time)
        {
            int temp = GetInfoLevel();
            _curentLevelOfTheRobot = _selectedRobot.robotLevel[temp];

            if (ResourcesOperations.Remove(StoreResourcesAmount.energy, _curentLevelOfTheRobot.priceToUpgrade.energy))
            {
                _upgradeButton.enabled = false;

                _robotManager.MakeAllButtonsInactive();

                _timer.AddTime((int)time);
                _timer.TimeTextState(true);
                StartCoroutine(Upgrading());
            }
        }


        /// <summary>
        /// Show the robot to upgrade in the right [image + text]
        /// </summary>
        /// <param name="robot"></param>
        private void DisplayRobotToUpgrade(Robot robot)
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


        private IEnumerator Upgrading()
        {
            int temp = 0;
            while(temp < _curentLevelOfTheRobot.timeToUpgrade)
            {
                temp += 1;
                yield return _timer.ActivateTimer();
            }

            _upgradeButton.enabled = true;
            _robotManager.MakeAllButtonsActive();
        }


        private void OnDestroy()
        {
            UnlimitedPlayerManager.OnUpgradingAccepted -= UpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingRejected -= UpgradingRejected;
        }
    }
}