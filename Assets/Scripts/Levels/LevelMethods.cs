using System;

namespace Levels
{
    /// <summary>
    /// 
    /// </summary>
    public static class LevelMethods
    {
        #region Constants

        public const byte MaxPlayerLevel = 100;
        public const byte MaxRobotsLevel = 10;
        public const byte ScanNumbers = 3;

        #endregion

        #region Player

        /// <summary>
        ///     The experience necessary for increasing the level
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public static uint Experience(byte level)
        {
            if (level == 1)
                return 30;
            else if (level <= 50)
                return (uint) ((level - 1) * 50);
            else if (level <= 80)
                return (uint) ((level - 1) * 500 + 9500);
            else 
                return (uint) ((level - 1) * 1000 + 60000);
        }
        
        #endregion

        #region Headquarters

        /// <summary>
        ///     The amount of resources needed to convert resources
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public static uint[] ResourceConversionCost(byte level)
        {
            uint silicon = (uint) (400 * level);
            uint lithium = (uint) (200 * level);
            uint titanium = (uint) (100 * level);
            var resourceValues = new uint[] { silicon, lithium, titanium };
            
            return resourceValues;
        }

        /// <summary>
        ///     Amount of energy generated from the resource conversion
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public static uint ResourceConversionGeneration(byte level)
        {
            return (uint) (9950 * level + Math.Pow(level, 2) * 50);
        }
        
        /// <summary>
        ///     The time necessary to convert resources
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public static ushort ResourceConversionTime(byte level)
        {
            if (level < 5)
                return 5;
            else
                return (ushort) (level/5 * 10);
        }

        /// <summary>
        ///     The energy necessary to upgrade a robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public static uint RobotUpgradeCost(byte level, byte robotType)
        {
            switch (robotType)
            {
                case RobotTypes.Worker: 
                    return (uint) (500 * Math.Pow(level, 2) - (500 * level) + 1000);
                case RobotTypes.Probe:
                    return (uint) (1000 * Math.Pow(level, 2) - (1000 * level) + 1500);
                case RobotTypes.Crusher:
                    return (uint) (2000 * Math.Pow(level, 2) - (2000 * level) + 3000);
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     The time necessary to upgrade the robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public static ushort RobotUpgradeTime(byte level)
        {
            return (ushort) (5 * Math.Pow(level, 2) - (5 * level) + 5);
        }
        
        /// <summary>
        ///     The energy necessary to build a robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public static uint RobotBuildCost(byte level, byte robotType)
        {
            uint tempValue = 0;
            switch (robotType)
            {
                case RobotTypes.Worker: 
                    tempValue = (uint) (200 * Math.Sqrt(level) - 100);
                    return tempValue - tempValue % 10;
                case RobotTypes.Probe:
                    tempValue = (uint) (300 * Math.Sqrt(level) - 150);
                    return tempValue - tempValue % 10;
                case RobotTypes.Crusher:
                    tempValue = (uint) (600 * Math.Sqrt(level) - 300);
                    return tempValue - tempValue % 10;
                default:
                    return 0;
            }
        }
        
        /// <summary>
        ///     The health of the robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public static ushort RobotHealth(byte level, byte robotType)
        {
            ushort tempValue = 0;
            switch (robotType)
            {
                case RobotTypes.Worker: 
                    tempValue = (ushort) (8 * Math.Log(level, 1.1) + 1000);
                    return (ushort) (tempValue - tempValue % 10);
                case RobotTypes.Probe:
                    tempValue = (ushort) (9 * Math.Log(level, 1.1) + 1000);
                    return (ushort) (tempValue - tempValue % 10);
                case RobotTypes.Crusher:
                    tempValue = (ushort) (11 * Math.Log(level, 1.1) + 2000);
                    return (ushort) (tempValue - tempValue % 10);
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     The movement speed of the robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public static ushort RobotMovementSpeed(byte level, byte robotType)
        {
            switch (robotType)
            {
                case RobotTypes.Worker: 
                    return (ushort) (3);
                case RobotTypes.Probe:
                    return (ushort) (0);
                case RobotTypes.Crusher:
                    return (ushort) (1);
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     The mining damage of the robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public static ushort RobotMiningDamage(byte level, byte robotType)
        {
            switch (robotType)
            {
                case RobotTypes.Worker: 
                    return (ushort) (2 * Math.Log(level, 1.1) + 10);
                case RobotTypes.Probe:
                    return (ushort) (0);
                case RobotTypes.Crusher:
                    return (ushort) (3 * Math.Log(level, 1.1) + 30);
                default:
                    return 0;
            }
        }
        
        /// <summary>
        ///     The self damage of the robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public static ushort RobotSelfDamage(byte level, byte robotType)
        {
            switch (robotType)
            {
                case RobotTypes.Worker: 
                    return (ushort) (2 * Math.Log(level, 1.1) + 40);
                case RobotTypes.Probe:
                    return (ushort) (2 * Math.Log(level, 1.1) + 50);
                case RobotTypes.Crusher:
                    return (ushort) (3 * Math.Log(level, 1.1) + 60);
                default:
                    return 0;
            }
        }
        
        /// <summary>
        ///     The total number of housing spaces
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public static ushort HousingSpace(byte level)
        {
            return (ushort) (7.7 * Math.Sqrt((double)level) + 23);
        }
        
        /// <summary>
        ///     The maximum amount of energy available
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public static uint MaxEnergyCount(byte level)
        {
            uint tempValue = (uint) (100000 * Math.Sqrt(level));
            return tempValue - tempValue % 10000;
        }

        /// <summary>
        ///     The maximum amount of energy
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public static uint[] MaxResourcesAmount(byte level)
        {
            var tempValue = (uint) (10000 * Math.Sqrt(level));
            var silicon = tempValue - tempValue % 1000;
            var lithium = tempValue - tempValue % 1000;
            var titanium = tempValue - tempValue % 1000;
            var maxResources = new uint[] { silicon, lithium, titanium };

            return maxResources;
        }

        #endregion
    }
}