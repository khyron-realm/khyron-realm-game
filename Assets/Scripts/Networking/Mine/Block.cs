using DarkRift;

namespace Networking.Mine
{
    /// <summary>
    ///     Mine block
    /// </summary>
    public class Block : IDarkRiftSerializable
    {
        private ushort X { get; set; }
        private ushort Y { get; set; }

        public Block(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public Block()
        { }

        public void Deserialize(DeserializeEvent e)
        {
            X = e.Reader.ReadUInt16();
            Y = e.Reader.ReadUInt16();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}