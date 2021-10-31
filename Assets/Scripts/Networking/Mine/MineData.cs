using DarkRift;

namespace Networking.Mine
{
    /// <summary>
    ///     Mine details
    /// </summary>
    public class MineData : IDarkRiftSerializable
    {
        public ushort Id { get; set; }
        public ushort Size { get; set; }
        public MineSeed Seed { get; set; }
        public Block[] Scans { get; set; }

        public MineData()
        { }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Size = e.Reader.ReadUInt16();
            Seed = e.Reader.ReadSerializable<MineSeed>();
            Scans = e.Reader.ReadSerializables<Block>();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}