using DarkRift;

namespace Networking.Mines
{
    /// <summary>
    ///     Mine details
    /// </summary>
    public class Mine : IDarkRiftSerializable
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public MineGenerator Generator { get; set; }
        public bool[] Blocks { get; set; }
        public MineScan[] Scans { get; set; }
        public byte MapPosition { get; set; }

        public Mine()
        { }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt32();
            Name = e.Reader.ReadString();
            Generator = e.Reader.ReadSerializable<MineGenerator>();
            Blocks = e.Reader.ReadBooleans();
            Scans = e.Reader.ReadSerializables<MineScan>();
            MapPosition = e.Reader.ReadByte();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}