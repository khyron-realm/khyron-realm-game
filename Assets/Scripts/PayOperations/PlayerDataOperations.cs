using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Headquarters;
using Manager.Robots;
using Networking.Levels;


namespace PlayerDataUpdate
{
    /// <summary>
    /// 
    /// Tags: 0 -> conversion
    ///       1 -> upgrading
    ///       2 -> building
    ///       
    ///      255-> nothing [null equivalent]
    /// 
    /// </summary>
    public static class PlayerDataOperations
    {
        #region "Event"
        public static event Action<byte> OnResourcesModified;         // Resources modified
        public static event Action<byte> OnNotEnoughResources;        // Not enough resources

        public static event Action<byte> OnEnergyModified;            // Energy Modified
        public static event Action<byte> OnNotEnoughEnergy;           // Not enoigh energy

        public static event Action<byte> OnRobotUpgraded;             // Robot Upgraded
        public static event Action<byte> OnRobotMaxLevelReached;      // Robot Upgrading Max level reached

        public static event Action<byte> OnEnoughSpaceForRobots;      // Enough space for building robots
        public static event Action<byte> OnNotEnoughSpaceForRobots;   // Not enough space for building robots

        public static event Action<byte> OnRobotAdded;                // Robots added
        public static event Action<byte> OnRobotRemoved;              // Robots removed

        public static event Action<byte> OnExperienceUpdated;         // Experience modified

        public static event Action<byte> OnLevelUpdated;              // Level Updated
        public static event Action<byte> OnMaximumLevelAchieved;      // Maximum levle achieved

        #endregion

        private static bool CheckIfEnoughResources(int resource, byte index)
        {
            int temp = (int)HeadquartersManager.Player.Resources[index].Count + resource;

            if(temp >= 0)
            {
                if(temp <= LevelMethods.MaxResourcesAmount(HeadquartersManager.Player.Level)[index])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        ///  Resources operations for paying
        /// </summary>
        /// <param name="silicon"> silicon amount </param>
        /// <param name="lithium"> lithium amount </param>
        /// <param name="titanium"> titanium amount </param>
        public static void PayResources(int silicon, int lithium, int titanium, byte tag)
        {
            if(CheckIfEnoughResources(silicon, 0) && CheckIfEnoughResources(lithium, 1) && CheckIfEnoughResources(titanium, 2))
            {
                HeadquartersManager.Player.Resources[0].Count += (uint)silicon;
                HeadquartersManager.Player.Resources[1].Count += (uint)lithium;
                HeadquartersManager.Player.Resources[2].Count += (uint)titanium;

                OnResourcesModified?.Invoke(tag);
            }
            else
            {
                OnNotEnoughResources?.Invoke(tag);
            }       
        }


        /// <summary>
        ///  Energy operation
        /// </summary>
        /// <param name="amount"> energy amount </param>
        public static void PayEnergy(int amount, byte tag)
        {
            int temp = (int)HeadquartersManager.Player.Energy + amount;

            if (temp >= 0)
            {
                if (temp <= LevelMethods.MaxEnergyCount(HeadquartersManager.Player.Level))
                {
                    HeadquartersManager.Player.Energy += (uint)amount;
                    OnEnergyModified?.Invoke(tag);
                }
                else
                {
                    OnNotEnoughEnergy?.Invoke(tag);
                }
            }
            else
            {
                OnNotEnoughEnergy?.Invoke(tag);
            }
        }


        /// <summary>
        /// Increase robot level by 1
        /// </summary>
        /// <param name="id"></param>
        public static void UpgradeRobot(byte id, byte tag)
        {
            if(HeadquartersManager.Player.Robots[id].Level <= LevelMethods.MaxRobotsLevel)
            {
                HeadquartersManager.Player.Robots[id].Level += 1;
                OnRobotUpgraded?.Invoke(tag);
            }
            else
            {
                OnRobotMaxLevelReached?.Invoke(tag);
            }          
        }


        /// <summary>
        ///  Check if robot can be built
        /// </summary>
        /// <param name="robot"></param>
        public static void CheckIfMaxRobotCapNotReached(byte id, int curentInTraining, byte tag)
        {
            int robotCurentCap = (HeadquartersManager.Player.Robots[0].Count * RobotsManager.robots[0].HousingSpace) + (HeadquartersManager.Player.Robots[1].Count * RobotsManager.robots[1].HousingSpace) + (HeadquartersManager.Player.Robots[2].Count * RobotsManager.robots[2].HousingSpace) + curentInTraining;

            if(robotCurentCap + RobotsManager.robots[id].HousingSpace <= LevelMethods.HousingSpace(HeadquartersManager.Player.Level))
            {
                OnEnoughSpaceForRobots?.Invoke(tag);
            }
            else
            {
                OnNotEnoughSpaceForRobots?.Invoke(tag);
            }
        }


        /// <summary>
        ///  Adds robot to count
        /// </summary>
        /// <param name="id"> robot id </param>
        public static void AddRobot(byte id, byte tag)
        {
            HeadquartersManager.Player.Robots[id].Count += 1;
            OnRobotAdded?.Invoke(tag);
        }


        /// <summary>
        ///  Removed robot from count
        /// </summary>
        /// <param name="id"> robot id </param>
        public static void RemoveRobot(byte id, byte tag)
        {
            HeadquartersManager.Player.Robots[id].Count -= 1;
            OnRobotRemoved?.Invoke(tag);
        }


        /// <summary>
        /// Increases experience by amount
        /// </summary>
        public static void ExperienceUpdate(int amount, byte tag)
        {
            uint temp = LevelMethods.Experience(HeadquartersManager.Player.Level);

            if(HeadquartersManager.Player.Level < LevelMethods.MaxPlayerLevel)
            {
                if ((HeadquartersManager.Player.Experience + amount) >= temp)
                {
                    LevelUpdate(0);
                    HeadquartersManager.Player.Experience += (uint)amount;
                    HeadquartersManager.Player.Experience -= temp;
                }
                else
                {
                    HeadquartersManager.Player.Experience += (uint)amount;
                }

                OnExperienceUpdated?.Invoke(tag);
            }
            else
            {
                OnMaximumLevelAchieved?.Invoke(tag);
            }
            
        }


        /// <summary>
        /// Increase level by 1
        /// </summary>
        public static void LevelUpdate(byte tag)
        {
            HeadquartersManager.Player.Level += 1;
            OnLevelUpdated?.Invoke(tag);                  
        }


        //######################
        //
        // public static void CheckPlayerDataId
        //
        //######################
    }
} 