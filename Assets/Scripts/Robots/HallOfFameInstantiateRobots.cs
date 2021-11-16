using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;
using Panels;
using Levels;
using Networking.Headquarters;


namespace Manager.Robots
{
    public class HallOfFameInstantiateRobots : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private GameObject _templateToInstantiate;
        [SerializeField] private GameObject _canvas;
        #endregion

        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += CreateHallOfFame;
        }

        private void CreateHallOfFame()
        {
            foreach(RobotSO item in RobotsManager.robots)
            {
                GameObject newPanel = Instantiate(_templateToInstantiate);
                newPanel.transform.SetParent(_canvas.transform, false);

                newPanel.transform.GetChild(0).GetComponent<Image>().sprite = item.RobotLevel[0].statusImage;

                newPanel.transform.GetChild(2).transform.GetChild(0).GetComponent<ProgressBar>().MaxValue = LevelMethods.RobotHealth(LevelMethods.MaxRobotsLevel, item.RobotId);
                newPanel.transform.GetChild(2).transform.GetChild(0).GetComponent<ProgressBar>().CurrentValue = LevelMethods.RobotHealth(HeadquartersManager.Player.Robots[item.RobotId].Level ,item.RobotId);

                newPanel.transform.GetChild(3).transform.GetChild(0).GetComponent<ProgressBar>().MaxValue = LevelMethods.RobotMovementSpeed(LevelMethods.MaxRobotsLevel, item.RobotId);
                newPanel.transform.GetChild(3).transform.GetChild(0).GetComponent<ProgressBar>().CurrentValue = LevelMethods.RobotMovementSpeed(HeadquartersManager.Player.Robots[item.RobotId].Level, item.RobotId);

                newPanel.transform.GetChild(4).transform.GetChild(0).GetComponent<ProgressBar>().MaxValue = LevelMethods.RobotMiningDamage(LevelMethods.MaxRobotsLevel, item.RobotId);
                newPanel.transform.GetChild(4).transform.GetChild(0).GetComponent<ProgressBar>().CurrentValue = LevelMethods.RobotMiningDamage(HeadquartersManager.Player.Robots[item.RobotId].Level, item.RobotId);

                newPanel.transform.GetChild(5).transform.GetChild(0).GetComponent<ProgressBar>().MaxValue = item.BuildTime;
                newPanel.transform.GetChild(5).transform.GetChild(0).GetComponent<ProgressBar>().CurrentValue = item.BuildTime;          
            }
        }

        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataReceived -= CreateHallOfFame;
        }
    }
}