using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Store;
using Manager.PayOperation;


namespace Manager.Train
{
    public class StoreTrainRobotsOperations : MonoBehaviour
    {
        [SerializeField] private RobotsManagerUI _managerUI;

        // events
        public static event Action OnStartOperation;
        public static event Action OnStopOperation;

        public static event Action<Robot> OnRobotAdded;
        public static event Action<Robot> OnRobotRemoved;

        public static event Action OnMaximumCapacityAchieved;

        private void Awake()
        {
            _managerUI.OnButtonPressed += AddRobotsToBuild;
        }


        // Add and remove robots from build order
        public static void AddRobotsToBuild(Robot robot)
        {
            if (StoreTrainRobots.robotsTrained.Count + StoreTrainRobots.robotsInTraining.Count < StoreTrainRobots.robotsLimit)
            {               
                if(PayRobots.StartPaymenetProcedure(robot))
                {
                    OnRobotAdded?.Invoke(robot);
                    StoreTrainRobots.robotsInTraining.Add(robot);
                    ManageIconsDuringTraining.CreateIconInTheRightForRobotInBuilding(robot);

                    if (StoreTrainRobots.robotsInTraining.Count > 0)
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
        public static void RemoveRobotsToBuild(Robot robot, GameObject robotIcon)
        {
            if (robotIcon == ManageIcons.robotsInBuildingIcons[0])
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
            ManageIconsDuringTraining.DezactivateIcon(robotIcon);

            if (StoreTrainRobots.robotsInTraining.Count < 1)
            {
                OnStopOperation?.Invoke();
            }
        }


        private static void Remove(Robot robot, GameObject robotIcon)
        {
            OnRobotRemoved?.Invoke(robot);
            PayRobots.RefundRobot(robot);
            StoreTrainRobots.robotsInTraining.Remove(robot);
            ManageIcons.robotsInBuildingIcons.Remove(robotIcon);
        }

        private void OnDestroy()
        {
            _managerUI.OnButtonPressed -= AddRobotsToBuild;
        }
    }
}