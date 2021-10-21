using DarkRift;

namespace Networking.Headquarters
{
    /// <summary>
    ///     Stores the resource data
    /// </summary>
    public class Resource : IDarkRiftSerializable
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public uint Count { get; set; }

        public Resource() {}
        
        public Resource(byte id, string name, uint count)
        {
            Id = id;
            Name = name;
            Count = count;
        }

        /// <summary>
        ///     Deserialization method for resource data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadByte();
            Name = e.Reader.ReadString();
            Count = e.Reader.ReadUInt32();
        }

        /// <summary>
        ///     Serialization method for resource data
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(Count);
        }
    }
}