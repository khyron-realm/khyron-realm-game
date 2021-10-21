using System.Collections;
using System.Collections.Generic;
using Networking.Game;
using UnityEngine;

namespace Save
{
    public static class GameDataValues
    {

        #region "Public members"
        public static ushort Version { get; set; }
        public static byte MaxPlayerLevel { get; set; }
        public static byte MaxRobotLevel { get; set; }
        public static uint MaxEnergy { get; set; }
        public static ushort MaxExperience { get; set; }
        public static uint MaxHousingSpace { get; set; }
        public static ushort ConversionTime { get; set; }
        public static List<ResourceDetails> Resources { get; set; }
        public static List<RobotDetails> Robots { get; set; }
        public static List<LevelFormulas> Levels { get; set; }
        #endregion
      
        public static void SaveDuringGamePlayPlayerData(GameData data)
        {
            Version = data.Version;
            MaxPlayerLevel = data.MaxPlayerLevel;
            MaxRobotLevel = data.MaxRobotLevel;
            MaxEnergy = data.MaxEnergy;
            MaxExperience = data.MaxExperience;
            MaxHousingSpace = data.MaxHousingSpace;
            ConversionTime = data.ConversionTime;
            Resources = new List<ResourceDetails>(data.Resources);
            Robots = new List<RobotDetails>(data.Robots);
            Levels = new List<LevelFormulas>(data.Levels);
        }
    }
}