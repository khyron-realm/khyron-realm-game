using DarkRift;

namespace Networking.Game
{
    /// <summary>
    ///     Build task for player data
    /// </summary>
    public class BuildTask : IDarkRiftSerializable
    {
        public byte Id { get; set; }
        public byte Type { get; set; }
        public byte Element { get; set; }
        public long EndTime { get; set; }

        public BuildTask() { }
        
        public BuildTask(byte id, byte type, byte element, long endTime)
        {
            Id = id;
            Type = type;
            Element = element;
            EndTime = endTime;
        }

        /// <summary>
        ///     Deserialization method for build task
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadByte();
            Type = e.Reader.ReadByte();
            Element = e.Reader.ReadByte();
            EndTime = e.Reader.ReadInt64();
        }
        
        /// <summary>
        ///     Serialization method for build task
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Type);
            e.Writer.Write(Element);
            e.Writer.Write(EndTime);
        }
    }
}