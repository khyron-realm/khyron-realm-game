using DarkRift;
using Networking.Game;

namespace Networking.GameElements
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
        public Resource[] Resources { get; set; }
        public Robot[] Robots { get; set; }
        public BuildTask[] TaskQueue { get; set; }

        public PlayerData() { }

        public PlayerData(string id, byte level, ushort experience, uint energy, Resource[] resources, Robot[] robots,
            BuildTask[] taskQueue)
        {
            Id = id;
            Level = level;
            Experience = experience;
            Energy = energy;
            Resources = resources;
            Robots = robots;
            TaskQueue = taskQueue;
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
            Resources = e.Reader.ReadSerializables<Resource>();
            Robots = e.Reader.ReadSerializables<Robot>();
            TaskQueue = e.Reader.ReadSerializables<BuildTask>();
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
            e.Writer.Write(TaskQueue);
        }
    }
}