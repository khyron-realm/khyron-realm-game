using DarkRift;

namespace Networking.Game
{
    /// <summary>
    ///     Stores the player data
    /// </summary>
    public class PlayerData : IDarkRiftSerializable
    {
        public string Id { get; set; }
        public byte Level { get; set; }
        public ushort Experience { get; set; }
        public uint Energy { get; set; }
        public Resource Silicon { get; set; }
        public Resource Lithium { get; set; }
        public Resource Titanium { get; set; }
        public Robot Worker { get; set; }
        public Robot Probe { get; set; }
        public Robot Crusher { get; set; }

        public PlayerData() { }

        public PlayerData(string id, byte level, ushort experience, uint energy, Resource silicon, Resource lithium,
            Resource titanium, Robot worker, Robot probe, Robot crusher)
        {
            Id = id;
            Level = level;
            Experience = experience;
            Energy = energy;
            Silicon = silicon;
            Lithium = lithium;
            Titanium = titanium;
            Worker = worker;
            Probe = probe;
            Crusher = crusher;
        }
        
        /// <summary>
        ///     Deserialization method for player data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadString();
            Level = e.Reader.ReadByte();
            Experience = e.Reader.ReadUInt16();
            Energy = e.Reader.ReadUInt32();
            Silicon = e.Reader.ReadSerializable<Resource>();
            Lithium = e.Reader.ReadSerializable<Resource>();
            Titanium = e.Reader.ReadSerializable<Resource>();
            Worker = e.Reader.ReadSerializable<Robot>();
            Probe = e.Reader.ReadSerializable<Robot>();
            Crusher = e.Reader.ReadSerializable<Robot>();
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
            e.Writer.Write(Silicon);
            e.Writer.Write(Lithium);
            e.Writer.Write(Titanium);
            e.Writer.Write(Worker);
            e.Writer.Write(Probe);
            e.Writer.Write(Crusher);
        }
    }
}