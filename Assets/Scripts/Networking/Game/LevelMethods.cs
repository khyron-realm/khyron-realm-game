using System;

namespace Unlimited_NetworkingServer_MiningGame.Game
{
    public class LevelMethods
    {
        #region Constants

        public const byte MAX_PLAYER_LEVEL = 100;
        public const byte MAX_ROBOTS_LEVEL = 10;

        #endregion
        
        #region Player
        
        /// <summary>
        ///     The experience necessary for increasing the level
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public uint Experience(byte level)
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
        public uint[] ResourceConversionCost(byte level)
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
        public uint ResourceConversionGeneration(byte level)
        {
            return (uint) (9950 * level + Math.Pow(level, 2) * 50);
        }
        
        /// <summary>
        ///     The time necessary to convert resources
        /// </summary>
        /// <param name="level">The current level</param>
        /// <returns>The calculated value for the level</returns>
        public ushort ResourceConversionTime(byte level)
        {
            if (level < 5)
                return 5;
            else
                return (ushort) (level/5 * 15);
        }

        /// <summary>
        ///     The energy necessary to upgrade a robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public uint RobotUpgradeCost(byte level, byte robotType)
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
        public ushort RobotUpgradeTime(byte level)
        {
            if (level < 5)
                return 5;
            else
                return (ushort) (level/5 * 15);
        }
        
        /// <summary>
        ///     The energy necessary to build a robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public uint RobotBuildCost(byte level, byte robotType)
        {
            switch (robotType)
            {
                case RobotTypes.Worker: 
                return (uint) (50 * Math.Pow(level, 2) - (50 * level) + 100);
                case RobotTypes.Probe:
                return (uint) (75 * Math.Pow(level, 2) - (75 * level) + 150);
                case RobotTypes.Crusher:
                return (uint) (200 * Math.Pow(level, 2) - (200 * level) + 300);
                default:
                return 0;
            }
        }

        /// <summary>
        ///     The time necessary to build the robot
        /// </summary>
        /// <param name="level">The current level</param>
        /// <param name="robotType">The type of robot</param>
        /// <returns>The calculated value for the level</returns>
        public ushort RobotBuildTime(byte level, byte robotType)
        {
            switch (robotType)
            {
                case RobotTypes.Worker: 
                    return 5;
                case RobotTypes.Probe:
                    return 10;
                case RobotTypes.Crusher:
                    return 30;
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
        public ushort RobotHealth(byte level, byte robotType)
        {
            switch (robotType)
            {
                case RobotTypes.Worker: 
                    return (ushort) (7 * Math.Log(level, 1.1) + 400);
                case RobotTypes.Probe:
                    return (ushort) (9 * Math.Log(level, 1.1) + 600);
                case RobotTypes.Crusher:
                    return (ushort) (11 * Math.Log(level, 1.1) + 800);
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
        public ushort RobotMovementSpeed(byte level, byte robotType)
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
        public ushort RobotMiningDamage(byte level, byte robotType)
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
        public ushort RobotSelfDamage(byte level, byte robotType)
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
        public ushort HousingSpace(byte level)
        {
            return (ushort) (10 + 4 * Math.Sqrt(level));
        }
        
        #endregion
    }
}