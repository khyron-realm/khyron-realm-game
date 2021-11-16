using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Headquarters;
using Levels;


namespace Manager.Upgrade
{
    public class UpgradeInfo : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Text _upgradePrice;
        [SerializeField] private Text _upgradeTime;
        #endregion


        private void Awake()
        {
            UpgradeRobots.OnRobotSelected += TextUpdate;
        }


        private void TextUpdate(byte temp)
        {
            _upgradePrice.text = LevelMethods.RobotUpgradeCost(HeadquartersManager.Player.Robots[temp].Level, temp).ToString();
            _upgradeTime.text = LevelMethods.RobotUpgradeTime(HeadquartersManager.Player.Robots[temp].Level).ToString() + "m"; ;
        }


        private void OnDestroy()
        {
            UpgradeRobots.OnRobotSelected -= TextUpdate;
        }
    }
}