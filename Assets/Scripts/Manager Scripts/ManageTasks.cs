using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Game;
using Manager.Robots;
using Networking.GameElements;


namespace Manager
{
    public class ManageTasks : MonoBehaviour
    {

        public static event Action<BuildTask, RobotSO> OnUpgradingWorking;
        public static event Action<BuildTask> OnConvertingWorking;
        public static event Action<BuildTask, RobotSO> OnBuildingRobotsWorking;


        private void Awake()
        {
            UnlimitedPlayerManager.OnPlayerDataReceived += GetTasks;
        }


        private void GetTasks()
        {
            for (int i = 0; i < UnlimitedPlayerManager.player.TaskQueue.Length; i++)
            {
                if (UnlimitedPlayerManager.player.TaskQueue[i].Type == 0)
                {
                    OnConvertingWorking?.Invoke(UnlimitedPlayerManager.player.TaskQueue[i]);
                }
                if (UnlimitedPlayerManager.player.TaskQueue[i].Type == 1)
                {
                    OnUpgradingWorking?.Invoke(UnlimitedPlayerManager.player.TaskQueue[i], CheckWhatRobotItIs(UnlimitedPlayerManager.player.TaskQueue[i].Element));
                }
                if (UnlimitedPlayerManager.player.TaskQueue[i].Type == 2)
                {
                    OnBuildingRobotsWorking?.Invoke(UnlimitedPlayerManager.player.TaskQueue[i], CheckWhatRobotItIs(UnlimitedPlayerManager.player.TaskQueue[i].Element));
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
            UnlimitedPlayerManager.OnPlayerDataReceived -= GetTasks;
        }
    }
}