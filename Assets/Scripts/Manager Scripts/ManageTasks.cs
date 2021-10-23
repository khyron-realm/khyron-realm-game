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
            for (int i = 0; i < HeadquartersManager.Player.BuildQueue.Length; i++)
            {
                if (HeadquartersManager.Player.BuildQueue[i].Type == 0)
                {
                    OnConvertingWorking?.Invoke(HeadquartersManager.Player.BuildQueue[i]);
                }
                if (HeadquartersManager.Player.BuildQueue[i].Type == 1)
                {
                    OnUpgradingWorking?.Invoke(HeadquartersManager.Player.BuildQueue[i], CheckWhatRobotItIs(HeadquartersManager.Player.BuildQueue[i].Element));
                }
                if (HeadquartersManager.Player.BuildQueue[i].Type == 2)
                {
                    OnBuildingRobotsWorking?.Invoke(HeadquartersManager.Player.BuildQueue[i], CheckWhatRobotItIs(HeadquartersManager.Player.BuildQueue[i].Element));
                }
            }
        }


        private RobotSO CheckWhatRobotItIs(byte id)
        {
            foreach (RobotSO item in RobotsManager.robots)
            {
                if(item._robotId == id)
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