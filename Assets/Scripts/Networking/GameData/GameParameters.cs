using DarkRift;

namespace Networking.GameData
{
    public class GameParameters : IDarkRiftSerializable
    {
        public GameParameters(ushort version, byte maxPlayerLevel, byte maxRobotLevel, uint maxEnergy,
            ushort maxExperience, uint maxHousingSpace, ushort conversionTime, ResourceDetails[] resources,
            RobotDetails[] robots, LevelFormulas[] levels)
        {
            Version = version;
            MaxPlayerLevel = maxPlayerLevel;
            MaxRobotLevel = maxRobotLevel;
            MaxEnergy = maxEnergy;
            MaxExperience = maxExperience;
            MaxHousingSpace = maxHousingSpace;
            ConversionTime = conversionTime;
            Resources = resources;
            Robots = robots;
            Levels = levels;
        }

        public GameParameters() { }

        public ushort Version { get; set; }
        public byte MaxPlayerLevel { get; set; }
        public byte MaxRobotLevel { get; set; }
        public uint MaxEnergy { get; set; }
        public ushort MaxExperience { get; set; }
        public uint MaxHousingSpace { get; set; }
        public ushort ConversionTime { get; set; }              // In minutes
        public ResourceDetails[] Resources { get; set; }
        public RobotDetails[] Robots { get; set; }
        public LevelFormulas[] Levels { get; set; }

        #region Serialization

        public void Deserialize(DeserializeEvent e)
        {
            throw new System.NotImplementedException();
        }

        public void Serialize(SerializeEvent e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}