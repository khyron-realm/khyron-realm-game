using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;
using Manager.Store;

namespace Manager.Upgrade
{
    public class UpgradeRobots : MonoBehaviour
    {
        [SerializeField] private Image _displayStatsImage;
        [SerializeField] private Text _nameOfTheRobot;
        [SerializeField] private Button _upgradeButton;

        [SerializeField] private RobotsManagerUI _robotManager;
        [SerializeField] private Timer _timer;


        private Robot _selectedRobot;
        private RobotLevel _curentLevelOfTheRobot;

        private void Awake()
        {
            _robotManager.OnButtonPressed += DisplayRobotToUpgrade;
            _upgradeButton.onClick.AddListener(StartUpgradingProcedure);
            _timer.TimeTextState(false);
        }

        private void Start()
        {
            DisplayRobotToUpgrade(RobotsManager.robots[0]);
        }

        private void DisplayRobotToUpgrade(Robot robot)
        {
            _selectedRobot = robot;

            int temp = GetInfoLevel();
            _displayStatsImage.sprite = robot.robotLevel[temp].upgradeImage;
            _nameOfTheRobot.text = robot.nameOfTheRobot;
        }

        private void StartUpgradingProcedure()
        {
            int temp = GetInfoLevel();
            _curentLevelOfTheRobot = _selectedRobot.robotLevel[temp];

            if (ResourcesOperations.Remove(StoreDataResources.energy, _curentLevelOfTheRobot.priceToUpgrade.energy))
            {
                _upgradeButton.enabled = false;

                _robotManager.MakeAllButtonsInactive();

                _timer.AddTime(_curentLevelOfTheRobot.timeToUpgrade);
                _timer.TimeTextState(true);
                StartCoroutine(Upgrading());
            }
        }

        private int GetInfoLevel()
        {
            return RobotsManager.robotsData[_selectedRobot.nameOfTheRobot.ToString()].robotLevel;
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
    }
}