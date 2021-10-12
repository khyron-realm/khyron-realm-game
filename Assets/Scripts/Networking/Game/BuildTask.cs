using DarkRift;

namespace Networking.Game
{
    /// <summary>
    ///     Build task for player data
    /// </summary>
    public class BuildTask : IDarkRiftSerializable
    {
        public byte Id { get; set; }
        public byte Element { get; set; }
        public byte Part { get; set; }
        public long EndTime { get; set; }

        public BuildTask() { }
        
        public BuildTask(byte id, byte element, byte part, long endTime)
        {
            Id = id;
            Element = element;
            Part = part;
            EndTime = endTime;
        }

        /// <summary>
        ///     Deserialization method for build task
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadByte();
            Element = e.Reader.ReadByte();
            Part = e.Reader.ReadByte();
            EndTime = e.Reader.ReadInt64();
        }
        
        /// <summary>
        ///     Serialization method for build task
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Element);
            e.Writer.Write(Part);
            e.Writer.Write(EndTime);
        }
    }
}