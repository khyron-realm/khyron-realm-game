using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.PayOperation;
using Manager.Robots;


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

        private void Awake()
        {
            _managerUI.OnButtonPressed += AddRobotsToBuild;
        }


        // Add and remove robots from build order
        public static void AddRobotsToBuild(Robot robot)
        {
            if (StoreRobots.RobotsTrained.Count + StoreRobots.RobotsInTraining.Count < StoreRobots.RobotsLimit)
            {               
                if(PayRobots.StartPaymenetProcedure(robot))
                {
                    if (StoreRobots.RobotsInTraining.Count > 0)
                    {
                        OnStartOperation?.Invoke();
                    }

                    OnRobotAdded?.Invoke(robot);
                    StoreRobots.RobotsInTraining.Add(robot);
                    RobotsInBuildingOperations.CreateIconInTheRightForRobotInBuilding(robot);                   
                }         
            }
            else
            {
                OnMaximumCapacityAchieved?.Invoke();
            }
        }
        public static void RemoveRobotsToBuild(Robot robot, GameObject robotIcon)
        {
            if (robotIcon == RobotsInBuilding.robotsInBuildingIcons[0])
            {
                OnStopOperation?.Invoke();
                Remove(robot, robotIcon);
                OnStartOperation?.Invoke();
            }
            else
            {
                Remove(robot, robotIcon);
            }


            BuildRobots.RecalculateTime();
            RobotsInBuildingOperations.DezactivateIcon(robotIcon);

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
            _managerUI.OnButtonPressed -= AddRobotsToBuild;
        }
    }
}