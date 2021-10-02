using DarkRift;

namespace Networking.Game
{
    /// <summary>
    ///     Stores the player data
    /// </summary>
    public class PlayerData
    {
        public PlayerData(string id, string name, ushort level, ushort experience, ushort energy)
        {
            Id = id;
            Name = name;
            Level = level;
            Experience = experience;
            Energy = energy;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ushort Level { get; set; }
        public ushort Experience { get; set; }
        public ushort Energy { get; set; }

        /// <summary>
        ///     Deserialization method for player data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadString();
            Name = e.Reader.ReadString();
            Level = e.Reader.ReadUInt16();
            Experience = e.Reader.ReadUInt16();
            Energy = e.Reader.ReadUInt16();
        }

        /// <summary>
        ///     Serialization method for player data
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(Level);
            e.Writer.Write(Experience);
            e.Writer.Write(Energy);
        }
    }
}