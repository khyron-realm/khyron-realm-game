using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;
using Networking.Headquarters;


namespace Manager
{
    public class ManageTasks : MonoBehaviour
    {

        public static event Action<BuildTask, RobotSO> OnUpgradingWorking;
        public static event Action<BuildTask> OnConvertingWorking;
        public static event Action<BuildTask, RobotSO> OnBuildingRobotsWorking;


        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += GetTasks;
        }


        private void GetTasks()
        {
            if(HeadquartersManager.Player.ConversionQueue.Length > 0)
            {
                OnConvertingWorking?.Invoke(HeadquartersManager.Player.ConversionQueue[0]);
            }
           
            if(HeadquartersManager.Player.UpgradeQueue.Length > 0)
            {
                OnUpgradingWorking?.Invoke(HeadquartersManager.Player.UpgradeQueue[0], CheckWhatRobotItIs(HeadquartersManager.Player.UpgradeQueue[0].Element));
            }

            if(HeadquartersManager.Player.BuildQueue.Length > 0)
            {
                for (int i = 0; i < HeadquartersManager.Player.BuildQueue.Length; i++)
                {                   
                    OnBuildingRobotsWorking?.Invoke(HeadquartersManager.Player.BuildQueue[i], CheckWhatRobotItIs(HeadquartersManager.Player.BuildQueue[i].Element));

                }
            }         
        }


        private RobotSO CheckWhatRobotItIs(byte id)
        {
            foreach (RobotSO item in RobotsManager.robots)
            {
                if(item.RobotId == id)
                {
                    return item;
                }
            }

            return null;
        }


        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataReceived -= GetTasks;
        }
    }
}