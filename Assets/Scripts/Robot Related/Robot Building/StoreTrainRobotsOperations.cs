using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Store;


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
        public static event Action<Robot> OnTransactionDone;

        private static bool _enough = false;

        private void Awake()
        {
            _managerUI.OnButtonPressed += AddRobotsToBuild;
            ManageResourcesOperations.OnNotEnoughResources += EnoughToPay;
        }


        // Add and remove robots from build order
        public static void AddRobotsToBuild(Robot robot)
        {
            if (StoreTrainRobots.robotsTrained.Count + StoreTrainRobots.robotsInTraining.Count < StoreTrainRobots.robotsLimit)
            {
                OnTransactionDone?.Invoke(robot);
                
                if(!_enough)
                {
                    OnRobotAdded?.Invoke(robot);
                    StoreTrainRobots.robotsInTraining.Add(robot);
                    ManageIconsDuringTraining.CreateIconInTheRightForRobotInBuilding(robot);

                    if (StoreTrainRobots.robotsInTraining.Count > 0)
                    {
                        OnStartOperation?.Invoke();
                    }

                }
                else
                {
                    _enough = false;
                }             
            }
        }
        public static void RemoveRobotsToBuild(Robot robot, GameObject robotIcon)
        {
            if (robotIcon == ManageIcons.robotsInBuildingIcons[0])
            {
                OnStopOperation?.Invoke();
                OnRobotRemoved?.Invoke(robot);

                StoreTrainRobots.robotsInTraining.Remove(robot);
                ManageIcons.robotsInBuildingIcons.Remove(robotIcon);
                
                OnStartOperation?.Invoke();
            }
            else
            {
                OnRobotRemoved?.Invoke(robot);
                StoreTrainRobots.robotsInTraining.Remove(robot);
                ManageIcons.robotsInBuildingIcons.Remove(robotIcon);              
            }


            BuildRobots.RecalculateTime();
            ManageIconsDuringTraining.DezactivateIcon(robotIcon);

            if (ManageIcons.robotsInBuildingIcons.Count > 0)
            {
                ManageIconsDuringTraining.ActivateDezactivateIconLoadingBar(ManageIcons.robotsInBuildingIcons[0], true);
            }

            if (StoreTrainRobots.robotsInTraining.Count < 1)
            {
                OnStopOperation?.Invoke();
            }
        }

        private void EnoughToPay()
        {
            _enough = true;
        }
    }
}