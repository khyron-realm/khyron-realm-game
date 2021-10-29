using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;
using Networking.Headquarters;
using Networking.Levels;
using PlayerDataUpdate;


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
        public static event Action<int, int> OnFirstRobotAdded;
        #endregion

        #region "Private members"
        private static RobotSO s_robot;
        private static GameObject s_robotIcon;

        private static ushort s_indexRobot = 0;    //uniq index of the robot
        private static int s_timeOfExecution = 0;

        public static SortedDictionary<ushort, RobotSO> RobotsInTraining; // robots in building process

        public static int TotalHousingSpaceDuringBuilding = 0;

        // ### CheckIfRobotIsFinished ###

        private static bool s_getTimeFromFirstTask = false;
        private static bool s_firstRobotAdded = true;

        private static BuildTask s_taskLastDone;
        private static RobotSO s_robotLastBuilt;

        private static DateTime s_initTimeOfBuilding;

        // ### end ###
        #endregion


        private void Awake()
        {
            RobotsInTraining = new SortedDictionary<ushort, RobotSO>();

            _managerUI.OnButtonPressed += BuildRobot;

            HeadquartersManager.OnBuildingError += BuildingError;
            HeadquartersManager.OnCancelBuildingError += CancelBuildingError;

            ManageTasks.OnBuildingRobotsWorking += RobotsInBuildingProcess;

            PlayerDataOperations.OnEnoughSpaceForRobots += HaveSpaceForRobot;
            PlayerDataOperations.OnEnergyModified += HaveEnergyForRobot;
            PlayerDataOperations.OnEnergyModified += CancelRobotConfirmation;
        }


        #region "Building Task Management"
        /// <summary>
        /// Displays all robots in progress at the start of the game
        /// </summary>
        /// <param name="task"> the task with the parameters </param>
        /// <param name="robot"> the robot in progress </param>

        private void RobotsInBuildingProcess(BuildTask task, RobotSO robot)
        {
            if(!CheckIfRobotIsFinished(task, robot))
            {              
                s_indexRobot = task.Id;
                s_robot = robot;

                TotalHousingSpaceDuringBuilding += RobotsManager.robots[s_robot.RobotId].HousingSpace; 
                RobotsInTraining.Add(s_indexRobot, s_robot);
                  
                s_robotIcon = RobotsInBuildingOperations.CreateIconInTheRightForRobotInBuilding(s_robot);

                if (RobotsInTraining.Count > 0)
                {
                    OnStartOperation?.Invoke();
                }
            }

            if(HeadquartersManager.Player.BuildQueue[HeadquartersManager.Player.BuildQueue.Length - 1] == task && s_taskLastDone != null)
            {
                HeadquartersManager.FinishBuildingRequest(s_taskLastDone.Id, s_robotLastBuilt.RobotId, DateTime.UtcNow, HeadquartersManager.Player.Robots);
            }
        }
        private bool CheckIfRobotIsFinished(BuildTask task, RobotSO robot)
        {
            s_timeOfExecution += RobotsManager.robots[robot.RobotId].BuildTime;

            // Get the time for the first robot
            if (s_getTimeFromFirstTask == false)
            {
                s_initTimeOfBuilding = DateTime.FromBinary(task.StartTime);
                s_getTimeFromFirstTask = true;
            }

            // Task is done , robot is built
            if(s_initTimeOfBuilding.AddSeconds(s_timeOfExecution) <= DateTime.UtcNow)
            {
                s_taskLastDone = task;
                s_robotLastBuilt = robot;

                PlayerDataOperations.AddRobot(robot.RobotId, 255);

                return true;
            }
            else
            {
                if (s_firstRobotAdded)
                {
                    if((int)s_initTimeOfBuilding.AddSeconds(s_timeOfExecution).Subtract(DateTime.UtcNow).TotalSeconds == 0)
                    {
                        s_taskLastDone = task;
                        s_robotLastBuilt = robot;

                        PlayerDataOperations.AddRobot(robot.RobotId, 255);

                        return true;
                    }
                    else
                    {
                        OnFirstRobotAdded?.Invoke(RobotsManager.robots[robot.RobotId].BuildTime - (int)s_initTimeOfBuilding.AddSeconds(s_timeOfExecution).Subtract(DateTime.UtcNow).TotalSeconds, (int)s_initTimeOfBuilding.AddSeconds(s_timeOfExecution).Subtract(DateTime.UtcNow).TotalSeconds);
                        s_firstRobotAdded = false;
                    }    
                }
                else
                {
                    OnRobotAdded?.Invoke(RobotsManager.robots[robot.RobotId].BuildTime);
                }

                return false;
            }           
        }
        #endregion


        #region "Build Robot"
        /// <summary>
        /// Add robot to building process
        /// </summary>
        /// <param name="robot"> robot to build </param>
        public void BuildRobot(RobotSO robot)
        {           
            if(RobotsInTraining.Count < 1)
            {
                s_indexRobot = 0;
            }

            s_robot = robot;
            s_indexRobot++;

            PlayerDataOperations.CheckIfMaxRobotCapNotReached(robot.RobotId, TotalHousingSpaceDuringBuilding, OperationsTags.BUILDING_ROBOTS);
                      
        }
        private void HaveSpaceForRobot(byte tag)
        {
            if(OperationsTags.BUILDING_ROBOTS == tag)
            {
                PlayerDataOperations.PayEnergy(-(int)LevelMethods.RobotBuildCost(HeadquartersManager.Player.Robots[s_robot.RobotId].Level, s_robot.RobotId), OperationsTags.BUILDING_ROBOTS);
            }           
        }
        private void HaveEnergyForRobot(byte tag)
        {
            if(OperationsTags.BUILDING_ROBOTS == tag)
            {
                if (RobotsInTraining.Count < 1)
                {
                    HeadquartersManager.BuildingRequest(s_indexRobot, s_robot.RobotId, DateTime.UtcNow.ToBinary(), HeadquartersManager.Player.Energy);
                }
                else
                {
                    HeadquartersManager.BuildingRequest(s_indexRobot, s_robot.RobotId, 0, HeadquartersManager.Player.Energy);
                }

                TotalHousingSpaceDuringBuilding += RobotsManager.robots[s_robot.RobotId].HousingSpace;

                RobotsInTraining.Add(s_indexRobot, s_robot);
                OnRobotAdded.Invoke(RobotsManager.robots[s_robot.RobotId].BuildTime);

                s_robotIcon = RobotsInBuildingOperations.CreateIconInTheRightForRobotInBuilding(s_robot);

                if (RobotsInTraining.Count > 0)
                {
                    OnStartOperation?.Invoke();
                }
            }
        }
        #endregion


        #region "Cancel Robot Building"
        /// <summary>
        /// Cancel robot in building process
        /// </summary>
        /// <param name="robot"> robot to cancel </param>
        /// <param name="robotIcon"> robot icon to remove </param>
        public static void CancelBuildRobot(RobotSO robot, GameObject robotIcon)
        {
            s_robot = robot;
            s_robotIcon = robotIcon;

            PlayerDataOperations.PayEnergy((int)LevelMethods.RobotBuildCost(HeadquartersManager.Player.Robots[robot.RobotId].Level, robot.RobotId), OperationsTags.BUILDING_ROBOTS_CANCEL);
        }
        private static void CancelRobotConfirmation(byte tag)
        {
            if(OperationsTags.BUILDING_ROBOTS_CANCEL == tag)
            {
                bool temp;

                if (RobotsInBuilding.robotsInBuildingIcons[0] == s_robotIcon)
                {
                    HeadquartersManager.CancelBuildingRequest((ushort)RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(s_robotIcon)).Key, s_robot.RobotId, DateTime.UtcNow, HeadquartersManager.Player.Energy, true);
                    temp = true;
                }
                else
                {
                    HeadquartersManager.CancelBuildingRequest((ushort)RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(s_robotIcon)).Key, s_robot.RobotId, DateTime.UtcNow, HeadquartersManager.Player.Energy, false);
                    temp = false;
                }

                switch (temp)
                {
                    case true:
                        OnStopOperation?.Invoke();
                        Remove(s_robotIcon);
                        OnStartOperation?.Invoke();
                        break;

                    case false:
                        Remove(s_robotIcon);
                        break;
                }

                TotalHousingSpaceDuringBuilding -= RobotsManager.robots[s_robot.RobotId].HousingSpace;
                BuildRobots.RecalculateTime();

                if (RobotsInTraining.Count < 1)
                {
                    OnStopOperation?.Invoke();
                }
            }
        }
        private static void Remove(GameObject robotIcon)
        {           
            RobotsInTraining.Remove(RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(robotIcon)).Key);

            RobotsInBuildingOperations.DezactivateIcon(robotIcon);
            RobotsInBuilding.robotsInBuildingIcons.Remove(robotIcon);           
        }
        #endregion


        #region "Errors"
        /// <summary>
        /// 1 --> already existent
        /// 2 --> Not enough resources
        /// </summary>
        /// <param name="errorId"></param>
        private void BuildingError(byte errorId)
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

        private void CancelBuildingError(byte taskType)
        {
            print("Build Error");
        }
        #endregion


        private void OnDestroy()
        {
            _managerUI.OnButtonPressed -= BuildRobot;

            HeadquartersManager.OnBuildingError -= BuildingError;
            HeadquartersManager.OnCancelBuildingError -= CancelBuildingError;

            ManageTasks.OnBuildingRobotsWorking -= RobotsInBuildingProcess;

            PlayerDataOperations.OnEnoughSpaceForRobots -= HaveSpaceForRobot;
            PlayerDataOperations.OnEnergyModified -= HaveEnergyForRobot;
            PlayerDataOperations.OnEnergyModified -= CancelRobotConfirmation;
        }
    }
} 