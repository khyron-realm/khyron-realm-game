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
        public MineGenerationValues GenerationValues { get; set; }
        public bool[] BlocksValues { get; set; }

        public MineData()
        { }

        public MineData(ushort id, ushort size)
        {
            Id = id;
            Size = size;
            GenerationValues = new MineGenerationValues();
            BlocksValues = new bool[] { };
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Size = e.Reader.ReadUInt16();
            GenerationValues = e.Reader.ReadSerializable<MineGenerationValues>();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Size);
            e.Writer.Write(GenerationValues);
            e.Writer.Write(BlocksValues);
        }

        /// <summary>
        ///     Set the blocks of the mine
        /// </summary>
        /// <param name="blocks">The current blocks</param>
        public void SetBlocks(bool[] blocks)
        {
            BlocksValues = blocks;
        }
    }
}