using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;
using Networking.Game;
using Save;
using Networking.GameElements;


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
        private static int timeOfExecution = 0;

        public static Dictionary<ushort, RobotSO> RobotsInTraining;
        #endregion

        private void Awake()
        {
            RobotsInTraining = new Dictionary<ushort, RobotSO>();

            _managerUI.OnButtonPressed += BuildRobot;

            HeadquartersManager.OnBuildingAccepted += BuildingAccepted;
            HeadquartersManager.OnBuildingRejected += BuildingRejected;

            HeadquartersManager.OnCancelBuildingAccepted += CancelBuildingAccepted;

            ManageTasks.OnBuildingRobotsWorking += RobotsInBuildingProcess;
        }


        /// <summary>
        /// Displays all robots in progress at the start of the game
        /// </summary>
        /// <param name="task"> the task with the parameters </param>
        /// <param name="robot"> the robot in progress </param>
        private void RobotsInBuildingProcess(BuildTask task, RobotSO robot)
        {
            if(CheckIfRobotIsFinished(task, robot))
            {
                print("----- ROBOT IN BUILDING -----");
                s_indexRobot = task.Id;
                s_robot = robot;

                RobotsInTraining.Add(s_indexRobot, s_robot);
                OnRobotAdded.Invoke(GameDataValues.Robots[s_robot._robotId].BuildTime);

                s_robotIcon = RobotsInBuildingOperations.CreateIconInTheRightForRobotInBuilding(s_robot);

                if (RobotsInTraining.Count > 0)
                {
                    OnStartOperation?.Invoke();
                }
            }            
        }
        private bool CheckIfRobotIsFinished(BuildTask task, RobotSO robot)
        {
            timeOfExecution += GameDataValues.Robots[robot._robotId].BuildTime;

            DateTime startTime = DateTime.FromBinary(task.StartTime);
            DateTime now = DateTime.UtcNow;

            int timeDiff = (int)now.Subtract(startTime).TotalSeconds;

            if(timeDiff > timeOfExecution)
            {
                HeadquartersManager.FinishBuildingRequest(task.Id, robot._robotId, DateTime.UtcNow, true);
                return false;
            }
            else
            {
                return true;
            }           
        }


        public void BuildRobot(RobotSO robot)
        {
            s_robot = robot;
            s_indexRobot++;
            HeadquartersManager.BuildingRequest(s_indexRobot, robot._robotId, DateTime.UtcNow);           
        }
        public static void CancelBuildRobot(RobotSO robot, GameObject robotIcon)
        {
            s_robot = robot;
            s_robotIcon = robotIcon;

            if (RobotsInBuilding.robotsInBuildingIcons[0] == robotIcon)
            {
                HeadquartersManager.FinishBuildingRequest((ushort)RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(s_robotIcon)).Key, robot._robotId, DateTime.UtcNow, false);
            }
            else
            {
                HeadquartersManager.FinishBuildingRequest((ushort)RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(s_robotIcon)).Key, robot._robotId, DateTime.UtcNow, false, false);
            }          
        }


        #region "Building Robots Handlers"
        private void BuildingAccepted()
        {           
            RobotsInTraining.Add(s_indexRobot, s_robot);
            OnRobotAdded.Invoke(GameDataValues.Robots[s_robot._robotId].BuildTime);
            
            s_robotIcon = RobotsInBuildingOperations.CreateIconInTheRightForRobotInBuilding(s_robot);

            if (RobotsInTraining.Count > 0)
            {
                OnStartOperation?.Invoke();
            }               
        }          

        /// <summary>
        /// 1 --> already existent
        /// 2 --> Not enough resources
        /// </summary>
        /// <param name="errorId"></param>
        private void BuildingRejected(byte errorId)
        {
            switch(errorId)
            {
                case 1:
                    print("Task with this id exists");
                    break;
                case 2:
                    print("Not enough resources");
                    break;
            }
        }
        #endregion

        
        private void CancelBuildingAccepted(byte taskType)
        {
            print("----------------------------------$$$-------------------------------");
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
                s_indexRobot = 0;
                OnStopOperation?.Invoke();
            }
        }
        private static void Remove(RobotSO robot, GameObject robotIcon)
        {          
            RobotsInTraining.Remove(RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(robotIcon)).Key);
            RobotsInBuilding.robotsInBuildingIcons.Remove(robotIcon);
        }


        private void OnDestroy()
        {
            _managerUI.OnButtonPressed -= BuildRobot;

            HeadquartersManager.OnBuildingAccepted -= BuildingAccepted;
            HeadquartersManager.OnBuildingRejected -= BuildingRejected;

            HeadquartersManager.OnCancelBuildingAccepted -= CancelBuildingAccepted;

            ManageTasks.OnBuildingRobotsWorking -= RobotsInBuildingProcess;
        }
    }
} 