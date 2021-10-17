using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.PayOperation;
using Manager.Robots;
using Networking.Game;


namespace Manager.Train
{
    public class BuildRobotsOperations : MonoBehaviour
    {
        [SerializeField] private RobotsManagerUI _managerUI;

        #region "Events"
        public static event Action OnStartOperation;
        public static event Action OnStopOperation;

        public static event Action<Robot> OnRobotAdded;
        public static event Action<Robot> OnRobotRemoved;

        public static event Action OnMaximumCapacityAchieved;
        #endregion

        private static Robot s_robot;
        private static GameObject s_robotIcon;


        private void Awake()
        {
            _managerUI.OnButtonPressed += BuildRobot;

            UnlimitedPlayerManager.OnBuildingAccepted += BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected += BuildingRejected;

            UnlimitedPlayerManager.OnCancelBuildingAccepted += CancelBuildingAccepted;
        }

        //public void TemporaryBuild(Robot robot)
        //{
        //    s_robot = robot;
        //    BuildingAccepted(2);
        //}


        public void BuildRobot(Robot robot)
        {            
            DateTime time = DateTime.Now;
            s_robot = robot;
            UnlimitedPlayerManager.BuildingRequest((byte)StoreRobots.RobotsInTraining.Count, robot._robotId, time);
        }
        public static void CancelBuildRobot(Robot robot, GameObject robotIcon)
        {

            s_robot = robot;
            s_robotIcon = robotIcon;
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.FinishBuildingRequest(robotId, (byte)StoreRobots.RobotsInTraining.IndexOf(robot), startTime, true, false);
        }


        private void BuildingAccepted()
        {
            print("Building robot");
            if (StoreRobots.RobotsTrained.Count + StoreRobots.RobotsInTraining.Count < StoreRobots.RobotsLimit)
            {
                if (PayRobots.StartPaymenetProcedure(s_robot))
                {
                    StoreRobots.RobotsInTraining.Add(s_robot);
                    RobotsInBuildingOperations.CreateIconInTheRightForRobotInBuilding(s_robot);

                    OnRobotAdded?.Invoke(s_robot);

                    if (StoreRobots.RobotsInTraining.Count > 0)
                    {
                        OnStartOperation?.Invoke();
                    }
                }
            }
            else
            {
                OnMaximumCapacityAchieved?.Invoke();
            }
        }        
        private void BuildingRejected(byte errorId)
        {
            print("---- Building Robots Rejected ----");
        }
            

        private void CancelBuildingAccepted()
        {
            print("Canceled");
            if (s_robotIcon == RobotsInBuilding.robotsInBuildingIcons[0])
            {
                OnStopOperation?.Invoke();
                Remove(s_robot, s_robotIcon);
                OnStartOperation?.Invoke();
            }
            else
            {
                Remove(s_robot, s_robotIcon);
            }


            BuildRobots.RecalculateTime();
            RobotsInBuildingOperations.DezactivateIcon(s_robotIcon);

            if (StoreRobots.RobotsInTraining.Count < 1)
            {
                OnStopOperation?.Invoke();
            }
        }
        private static void Remove(Robot robot, GameObject robotIcon)
        {
            OnRobotRemoved?.Invoke(robot);
            PayRobots.RefundRobot(robot);
            StoreRobots.RobotsInTraining.Remove(robot);
            RobotsInBuilding.robotsInBuildingIcons.Remove(robotIcon);
        }


        private void OnDestroy()
        {
            _managerUI.OnButtonPressed -= BuildRobot;

            UnlimitedPlayerManager.OnBuildingAccepted -= BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected -= BuildingRejected;

            UnlimitedPlayerManager.OnCancelBuildingAccepted += CancelBuildingAccepted;
        }
    }
}