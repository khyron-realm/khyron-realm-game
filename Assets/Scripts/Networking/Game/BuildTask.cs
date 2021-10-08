using DarkRift;

namespace Networking.Game
{
    /// <summary>
    ///     Build task for player data
    /// </summary>
    public class BuildTask : IDarkRiftSerializable
    {
        public byte Status { get; set; }
        public byte ElementId { get; set; }
        public byte ElementPart { get; set; }
        public long EndTime { get; set; }

        public BuildTask() { }
        
        public BuildTask(byte status, byte elementId, byte elementPart, long endTime)
        {
            Status = status;
            ElementId = elementId;
            ElementPart = elementPart;
            EndTime = endTime;
        }

        /// <summary>
        ///     Deserialization method for build task
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Status = e.Reader.ReadByte();
            ElementId = e.Reader.ReadByte();
            ElementPart = e.Reader.ReadByte();
            EndTime = e.Reader.ReadInt64();
        }
        
        /// <summary>
        ///     Serialization method for build task
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Status);
            e.Writer.Write(ElementId);
            e.Writer.Write(ElementPart);
            e.Writer.Write(EndTime);
        }
    }
}