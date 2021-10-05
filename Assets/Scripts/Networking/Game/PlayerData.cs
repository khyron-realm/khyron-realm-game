using DarkRift;

namespace Networking.Game
{
    /// <summary>
    ///     Stores the player data
    /// </summary>
    public class PlayerData : IDarkRiftSerializable
    {
        public string Id { get; set; }
        public ushort Level { get; set; }
        public ushort Experience { get; set; }
        public ushort Energy { get; set; }
        public Resource Silicon { get; set; }
        public Resource Lithium { get; set; }
        public Resource Titanium { get; set; }
        
        public PlayerData() {}
        
        public PlayerData(string id, ushort level, ushort experience, ushort energy, Resource silicon, Resource lithium,
            Resource titanium)
        {
            Id = id;
            Level = level;
            Experience = experience;
            Energy = energy;
            Silicon = silicon;
            Lithium = lithium;
            Titanium = titanium;
        }
        
        /// <summary>
        ///     Deserialization method for player data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadString();
            Level = e.Reader.ReadUInt16();
            Experience = e.Reader.ReadUInt16();
            Energy = e.Reader.ReadUInt16();
            Silicon = e.Reader.ReadSerializable<Resource>();
            Lithium = e.Reader.ReadSerializable<Resource>();
            Titanium = e.Reader.ReadSerializable<Resource>();
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
        }
    }
}