using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;

namespace Manager.Upgrade
{
    public class UpgradeRobots : MonoBehaviour
    {
        [SerializeField] private Image _displayStatsImage;
        [SerializeField] private Text _nameOfTheRobot;
        [SerializeField] private Button _upgradeButton;

        [SerializeField] private RobotsManagerUI _robotManager;

        private void Awake()
        {
            _robotManager.OnButtonPressed += DisplayRobotToUpgrade;
        }


        private void DisplayRobotToUpgrade(Robot robot)
        {
            int temp = RobotsManager.robotsData[robot.nameOfTheRobot.ToString()].robotLevel;

            _displayStatsImage.sprite = robot.robotLevel[temp].upgradeImage;
            _nameOfTheRobot.text = robot.nameOfTheRobot;
        }
    }
}
