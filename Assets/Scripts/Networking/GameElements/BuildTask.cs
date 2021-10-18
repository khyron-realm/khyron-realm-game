using DarkRift;

namespace Networking.GameElements
{
    /// <summary>
    ///     Build task for player data
    /// </summary>
    public class BuildTask : IDarkRiftSerializable
    {
        public ushort Id { get; set; }
        public byte Type { get; set; }
        public byte Element { get; set; }
        public long StartTime { get; set; }

        public BuildTask() { }
        
        public BuildTask(ushort id, byte type, byte element, long startTime)
        {
            Id = id;
            Type = type;
            Element = element;
            StartTime = startTime;
        }

        /// <summary>
        ///     Deserialization method for build task
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Type = e.Reader.ReadByte();
            Element = e.Reader.ReadByte();
            StartTime = e.Reader.ReadInt64();
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
            e.Writer.Write(StartTime);
        }
    }
}