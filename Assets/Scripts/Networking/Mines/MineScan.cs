using DarkRift;

namespace Networking.Mines
{
    /// <summary>
    ///     Mine block
    /// </summary>
    public class MineScan : IDarkRiftSerializable
    {
        public uint Player { get; set; }
        public ushort X { get; set; }
        public ushort Y { get; set; }
        
        public MineScan()
        { }

        public MineScan(ushort player, ushort x, ushort y)
        {
            Player = player;
            X = x;
            Y = y;
        }
        
        public void Deserialize(DeserializeEvent e)
        {
            X = e.Reader.ReadUInt16();
            Y = e.Reader.ReadUInt16();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Player);
            e.Writer.Write(X);
            e.Writer.Write(Y);
        }
    }
}