using DarkRift;

namespace Networking.Game
{
    public class Resource : IDarkRiftSerializable
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public ushort ConversionRate { get; set; }
        public ushort Count { get; set; }

        public Resource() {}
        
        public Resource(ushort id, string name, ushort conversionRate, ushort count)
        {
            Id = id;
            Name = name;
            ConversionRate = conversionRate;
            Count = count;
        }

        /// <summary>
        ///     Deserialization method for resource data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Name = e.Reader.ReadString();
            ConversionRate = e.Reader.ReadUInt16();
            Count = e.Reader.ReadUInt16();
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
            e.Writer.Write(Count);
        }
    }
}