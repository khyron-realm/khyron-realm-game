using DarkRift;

namespace Networking.Game
{
    /// <summary>
    ///     Stores the resource data
    /// </summary>
    public class ResourceDetails : IDarkRiftSerializable
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public ushort ConversionRate { get; set; }
        public uint MaxCount { get; set; }

        public ResourceDetails() {}
        
        public ResourceDetails(byte id, string name, ushort conversionRate, uint maxCount)
        {
            Id = id;
            Name = name;
            ConversionRate = conversionRate;
            MaxCount = maxCount;
        }

        /// <summary>
        ///     Deserialization method for resource data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadByte();
            Name = e.Reader.ReadString();
            ConversionRate = e.Reader.ReadUInt16();
            MaxCount = e.Reader.ReadUInt32();
        }

        /// <summary>
        ///     Serialization method for resource data
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(ConversionRate);
            e.Writer.Write(MaxCount);
        }
    }
}