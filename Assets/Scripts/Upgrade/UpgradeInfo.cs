using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Headquarters;
using Levels;
using TMPro;
using Panels;


namespace Manager.Upgrade
{
    public class UpgradeInfo : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Text _upgradePrice;
        [SerializeField] private Text _upgradeTime;

        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _movement;
        [SerializeField] private TextMeshProUGUI _mining;

        [SerializeField] private ProgressBar _healtBar;
        [SerializeField] private ProgressBar _movementBar;
        [SerializeField] private ProgressBar _miningBar;
        #endregion


        private void Awake()
        {
            UpgradeRobots.OnRobotSelected += TextUpdate;
        }


        private void TextUpdate(byte temp)
        {
            _upgradePrice.text = LevelMethods.RobotUpgradeCost(HeadquartersManager.Player.Robots[temp].Level, temp).ToString();
            _upgradeTime.text = LevelMethods.RobotUpgradeTime(HeadquartersManager.Player.Robots[temp].Level).ToString() + "m";

            _health.text = LevelMethods.RobotHealth(HeadquartersManager.Player.Robots[temp].Level, temp).ToString();
            _movement.text = LevelMethods.RobotMovementSpeed(HeadquartersManager.Player.Robots[temp].Level, temp).ToString();
            _mining.text = LevelMethods.RobotMiningDamage(HeadquartersManager.Player.Robots[temp].Level, temp).ToString();


            _healtBar.MaxValue = LevelMethods.RobotHealth(LevelMethods.MaxRobotsLevel, temp);
            _healtBar.CurrentValue = LevelMethods.RobotHealth(HeadquartersManager.Player.Robots[temp].Level, temp);

            _movementBar.MaxValue = LevelMethods.RobotMovementSpeed(LevelMethods.MaxRobotsLevel, temp);
            _movementBar.CurrentValue = LevelMethods.RobotMovementSpeed(HeadquartersManager.Player.Robots[temp].Level, temp);

            _miningBar.MaxValue = LevelMethods.RobotMiningDamage(LevelMethods.MaxRobotsLevel, temp);
            _miningBar.CurrentValue = LevelMethods.RobotMiningDamage(HeadquartersManager.Player.Robots[temp].Level, temp);
        }


        private void OnDestroy()
        {
            UpgradeRobots.OnRobotSelected -= TextUpdate;
        }
    }
}