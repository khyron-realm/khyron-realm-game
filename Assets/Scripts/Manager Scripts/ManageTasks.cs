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
            for (int i = 0; i < HeadquartersManager.Player.TaskQueue.Length; i++)
            {
                if (HeadquartersManager.Player.TaskQueue[i].Type == 0)
                {
                    OnConvertingWorking?.Invoke(HeadquartersManager.Player.TaskQueue[i]);
                }
                if (HeadquartersManager.Player.TaskQueue[i].Type == 1)
                {
                    OnUpgradingWorking?.Invoke(HeadquartersManager.Player.TaskQueue[i], CheckWhatRobotItIs(HeadquartersManager.Player.TaskQueue[i].Element));
                }
                if (HeadquartersManager.Player.TaskQueue[i].Type == 2)
                {
                    OnBuildingRobotsWorking?.Invoke(HeadquartersManager.Player.TaskQueue[i], CheckWhatRobotItIs(HeadquartersManager.Player.TaskQueue[i].Element));
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