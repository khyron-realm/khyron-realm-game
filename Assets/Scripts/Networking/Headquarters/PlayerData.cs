using DarkRift;

namespace Networking.Headquarters
{
    /// <summary>
    ///     Stores the player data
    /// </summary>
    public class PlayerData : IDarkRiftSerializable
    {
        public string Id { get; set; }
        public byte Level { get; set; }
        public uint Experience { get; set; }
        public uint Energy { get; set; }
        public Resource[] Resources { get; set; }
        public Robot[] Robots { get; set; }
        public BuildTask[] ConversionQueue { get; set; }
        public BuildTask[] UpgradeQueue { get; set; }
        public BuildTask[] BuildQueue { get; set; }

        public PlayerData() { }

        public PlayerData(string id, byte level, uint experience, uint energy, Resource[] resources, Robot[] robots,
            BuildTask[] conversionQueue, BuildTask[] upgradeQueue, BuildTask[] buildQueue)
        {
            Id = id;
            Level = level;
            Experience = experience;
            Energy = energy;
            Resources = resources;
            Robots = robots;
            ConversionQueue = conversionQueue;
            UpgradeQueue = upgradeQueue;
            BuildQueue = buildQueue;
        }
        
        /// <summary>
        ///     Deserialization method for player data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadString();
            Level = e.Reader.ReadByte();
            Experience = e.Reader.ReadUInt32();
            Energy = e.Reader.ReadUInt32();
            Resources = e.Reader.ReadSerializables<Resource>();
            Robots = e.Reader.ReadSerializables<Robot>();
            ConversionQueue = e.Reader.ReadSerializables<BuildTask>();
            UpgradeQueue = e.Reader.ReadSerializables<BuildTask>();
            BuildQueue = e.Reader.ReadSerializables<BuildTask>();
        }
        
        /// <summary>
        ///     Serialization method for player data
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Level);
            e.Writer.Write(Experience);
            e.Writer.Write(Energy);
            e.Writer.Write(Resources);
            e.Writer.Write(Robots);
            e.Writer.Write(ConversionQueue);
            e.Writer.Write(UpgradeQueue);
            e.Writer.Write(BuildQueue);
        }
    }
}