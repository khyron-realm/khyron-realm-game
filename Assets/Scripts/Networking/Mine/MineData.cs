using DarkRift;

namespace Networking.Mine
{
    /// <summary>
    ///     Mine details
    /// </summary>
    public class MineData : IDarkRiftSerializable
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public MineGenerationValues GenerationValues { get; set; }
        public bool[] BlocksValues { get; set; }
        public MineScan[] Scans { get; set; }

        public MineData()
        { }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt32();
            Name = e.Reader.ReadString();
            GenerationValues = e.Reader.ReadSerializable<MineGenerationValues>();
            BlocksValues = e.Reader.ReadBooleans();
            Scans = e.Reader.ReadSerializables<MineScan>();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}