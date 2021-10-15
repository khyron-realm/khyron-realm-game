using DarkRift;

namespace Networking.GameElements
{
    /// <summary>
    ///     Stores the resource data
    /// </summary>
    public class Robot : IDarkRiftSerializable
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public byte Level { get; set; }
        public byte Count { get; set; }

        public Robot() { }
        
        public Robot(byte id, string name, byte level, byte count)
        {
            Id = id;
            Name = name;
            Level = level;
            Count = count;
        }

        /// <summary>
        ///     Deserialization method for robot data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadByte();
            Name = e.Reader.ReadString();
            Level = e.Reader.ReadByte();
            Count = e.Reader.ReadByte();
        }

        /// <summary>
        ///     Serialization method for robot data
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(Level);
            e.Writer.Write(Count);
        }
    }
}