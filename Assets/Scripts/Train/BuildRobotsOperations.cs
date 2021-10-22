using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;
using Save;
using Networking.Headquarters;
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
        #endregion

        #region "Private members"
        private static RobotSO s_robot;
        private static GameObject s_robotIcon;

        private static ushort s_indexRobot = 0;
        private static int timeOfExecution = 0;

        public static Dictionary<ushort, RobotSO> RobotsInTraining;

        private static bool _onceMsg = false;
        private static bool _onceTask = false;

        private static int _timeDiff = 0;

        private static byte TagBuild = 2;
        private static byte TagCancel = 3;

        public static int TotalHousingSpaceDuringBuilding = 0;
        #endregion


        private void Awake()
        {
            RobotsInTraining = new Dictionary<ushort, RobotSO>();

            _managerUI.OnButtonPressed += BuildRobot;

            HeadquartersManager.OnBuildingError += BuildingError;
            HeadquartersManager.OnCancelBuildingError += CancelBuildingError;

            ManageTasks.OnBuildingRobotsWorking += RobotsInBuildingProcess;

            PlayerDataOperations.OnEnoughSpaceForRobots += HaveSpaceForRobot;
            PlayerDataOperations.OnEnergyModified += HaveEnergyForRobot;
            PlayerDataOperations.OnEnergyModified += CancelRobotConfirmation;
        }


        /// <summary>
        /// Displays all robots in progress at the start of the game
        /// </summary>
        /// <param name="task"> the task with the parameters </param>
        /// <param name="robot"> the robot in progress </param>
        private void RobotsInBuildingProcess(BuildTask task, RobotSO robot)
        {
            if(!CheckIfRobotIsFinished(task, robot))
            {
                if(_onceMsg == false)
                {
                    HeadquartersManager.FinishBuildingRequest(task.Id, robot._robotId, DateTime.UtcNow, HeadquartersManager.Player.Robots[robot._robotId]);
                    _onceMsg = true;
                }

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

            if (_onceTask == false)
            {
                DateTime startTime = DateTime.FromBinary(task.StartTime);
                DateTime now = DateTime.UtcNow;

                _timeDiff = (int)now.Subtract(startTime).TotalSeconds;

                _onceTask = true;
            }
            
            if(_timeDiff > timeOfExecution)
            {            
                return true;
            }
            else
            {
                PlayerDataOperations.AddRobot(robot._robotId, 255);
                return false;
            }           
        }



        /// <summary>
        /// Add robot to building process
        /// </summary>
        /// <param name="robot"> robot to build </param>
        public void BuildRobot(RobotSO robot)
        {
            s_robot = robot;
            s_indexRobot++;

            PlayerDataOperations.CheckIfMaxRobotCapNotReached(robot._robotId, TotalHousingSpaceDuringBuilding, TagBuild);
                      
        }
        private void HaveSpaceForRobot(byte tag)
        {
            if(TagBuild == tag)
            {
                PlayerDataOperations.PayEnergy(-GameDataValues.Robots[s_robot._robotId].BuildPrice, TagBuild);
            }           
        }
        private void HaveEnergyForRobot(byte tag)
        {
            if(TagBuild == tag)
            {
                if (RobotsInTraining.Count < 1)
                {
                    HeadquartersManager.BuildingRequest(s_indexRobot, s_robot._robotId, DateTime.UtcNow.ToBinary(), GameDataValues.Robots[s_robot._robotId].BuildPrice);
                }
                else
                {
                    HeadquartersManager.BuildingRequest(s_indexRobot, s_robot._robotId, 0, GameDataValues.Robots[s_robot._robotId].BuildPrice);
                }

                TotalHousingSpaceDuringBuilding += GameDataValues.Robots[s_robot._robotId].HousingSpace;

                RobotsInTraining.Add(s_indexRobot, s_robot);
                OnRobotAdded.Invoke(GameDataValues.Robots[s_robot._robotId].BuildTime);

                s_robotIcon = RobotsInBuildingOperations.CreateIconInTheRightForRobotInBuilding(s_robot);

                if (RobotsInTraining.Count > 0)
                {
                    OnStartOperation?.Invoke();
                }
            }
        }



        /// <summary>
        /// Cancel robot in building process
        /// </summary>
        /// <param name="robot"> robot to cancel </param>
        /// <param name="robotIcon"> robot icon to remove </param>
        public static void CancelBuildRobot(RobotSO robot, GameObject robotIcon)
        {
            s_robot = robot;
            s_robotIcon = robotIcon;

            PlayerDataOperations.PayEnergy(GameDataValues.Robots[s_robot._robotId].BuildPrice, TagCancel);
        }
        private static void CancelRobotConfirmation(byte tag)
        {
            if(TagCancel == tag)
            {
                bool temp;

                if (RobotsInBuilding.robotsInBuildingIcons[0] == s_robotIcon)
                {
                    HeadquartersManager.CancelBuildingRequest((ushort)RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(s_robotIcon)).Key, s_robot._robotId, DateTime.UtcNow, 100, true);
                    temp = true;
                }
                else
                {
                    HeadquartersManager.CancelBuildingRequest((ushort)RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(s_robotIcon)).Key, s_robot._robotId, DateTime.UtcNow, 100, false);
                    temp = false;
                }

                switch (temp)
                {
                    case true:
                        OnStopOperation?.Invoke();
                        Remove(s_robot, s_robotIcon);
                        OnStartOperation?.Invoke();
                        break;

                    case false:
                        Remove(s_robot, s_robotIcon);
                        break;
                }

                TotalHousingSpaceDuringBuilding -= GameDataValues.Robots[s_robot._robotId].HousingSpace;

                BuildRobots.RecalculateTime();
                RobotsInBuildingOperations.DezactivateIcon(s_robotIcon);

                if (RobotsInTraining.Count < 1)
                {
                    s_indexRobot = 0;
                    OnStopOperation?.Invoke();
                }
            }
        }
        private static void Remove(RobotSO robot, GameObject robotIcon)
        {
            RobotsInTraining.Remove(RobotsInTraining.ElementAt((ushort)RobotsInBuilding.robotsInBuildingIcons.IndexOf(robotIcon)).Key);
            RobotsInBuilding.robotsInBuildingIcons.Remove(robotIcon);
        }


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