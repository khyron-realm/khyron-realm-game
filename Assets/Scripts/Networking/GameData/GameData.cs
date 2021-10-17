using DarkRift;

namespace Networking.GameData
{
    public class GameData : IDarkRiftSerializable
    {
        public GameData(ushort version, byte maxPlayerLevel, byte maxRobotLevel, uint maxEnergy,
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

        public GameData() { }

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
            Version = e.Reader.ReadUInt16();
            MaxPlayerLevel = e.Reader.ReadByte();
            MaxRobotLevel = e.Reader.ReadByte();
            MaxEnergy = e.Reader.ReadUInt32();
            MaxExperience = e.Reader.ReadUInt16();
            MaxHousingSpace = e.Reader.ReadUInt32();
            ConversionTime = e.Reader.ReadUInt16();
            Resources = e.Reader.ReadSerializables<ResourceDetails>();
            Robots = e.Reader.ReadSerializables<RobotDetails>();
            Levels = e.Reader.ReadSerializables<LevelFormulas>();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Version);
            e.Writer.Write(MaxPlayerLevel);
            e.Writer.Write(MaxRobotLevel);
            e.Writer.Write(MaxEnergy);
            e.Writer.Write(MaxExperience);
            e.Writer.Write(MaxHousingSpace);
            e.Writer.Write(ConversionTime);
            e.Writer.Write(Resources);
            e.Writer.Write(Robots);
            e.Writer.Write(Levels);
        }

        #endregion
    }
}