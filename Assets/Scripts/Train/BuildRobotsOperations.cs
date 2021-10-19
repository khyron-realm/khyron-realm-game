using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;
using Networking.Game;
using Save;


namespace Manager.Train
{
    public class BuildRobotsOperations : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private RobotsManagerUI _managerUI;
        #endregion

        #region "Events"
        public static event Action OnStartOperation;
        public static event Action OnStopOperation;
        public static event Action<int> OnRobotAdded;
        #endregion

        #region "Private members"
        private static RobotSO s_robot;
        private static GameObject s_robotIcon;

        private static ushort s_indexRobot = 0;

        public static Dictionary<ushort, RobotSO> RobotsInTraining;
        #endregion

        private void Awake()
        {
            RobotsInTraining = new Dictionary<ushort, RobotSO>();

            _managerUI.OnButtonPressed += BuildRobot;

            UnlimitedPlayerManager.OnBuildingAccepted += BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected += BuildingRejected;

            UnlimitedPlayerManager.OnCancelBuildingAccepted += CancelBuildingAccepted;
        }


        public void BuildRobot(RobotSO robot)
        {            
            s_robot = robot;
            UnlimitedPlayerManager.BuildingRequest(s_indexRobot, robot._robotId, DateTime.UtcNow);
        }
        public static void CancelBuildRobot(RobotSO robot, GameObject robotIcon)
        {
            s_robot = robot;
            s_robotIcon = robotIcon;
            UnlimitedPlayerManager.FinishBuildingRequest(s_indexRobot, robot._robotId, DateTime.UtcNow, false);
        }


        #region "Building Robots Handlers"
        private void BuildingAccepted()
        {
            RobotsInTraining.Add(s_indexRobot, s_robot);
            OnRobotAdded.Invoke(GameDataValues.Robots[s_robot._robotId].BuildTime);

            s_indexRobot++;
            s_robotIcon = RobotsInBuildingOperations.CreateIconInTheRightForRobotInBuilding(s_robot);

            if (RobotsInTraining.Count > 0)
            {
                OnStartOperation?.Invoke();
            }               
        }          
        private void BuildingRejected(byte errorId)
        {
            print("!!!---- Building Robots Rejected ----!!!");
        }
        #endregion

        
        private void CancelBuildingAccepted(byte taskType)
        {
            switch (taskType)
            {
                case 0:
                    OnStopOperation?.Invoke();
                    Remove(s_robot, s_robotIcon);
                    OnStartOperation?.Invoke();
                    break;

                case 1:
                    Remove(s_robot, s_robotIcon);
                    break;
            }

            BuildRobots.RecalculateTime();
            RobotsInBuildingOperations.DezactivateIcon(s_robotIcon);

            if (RobotsInTraining.Count < 1)
            {
                OnStopOperation?.Invoke();
            }
        }
        private static void Remove(RobotSO robot, GameObject robotIcon)
        {
            RobotsInTraining.Remove(robot._robotId);
            RobotsInBuilding.robotsInBuildingIcons.Remove(robotIcon);
        }


        private void OnDestroy()
        {
            _managerUI.OnButtonPressed -= BuildRobot;

            UnlimitedPlayerManager.OnBuildingAccepted -= BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected -= BuildingRejected;

            UnlimitedPlayerManager.OnCancelBuildingAccepted -= CancelBuildingAccepted;
        }
    }
}