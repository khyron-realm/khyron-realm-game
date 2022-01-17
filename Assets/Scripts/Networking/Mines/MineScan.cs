using DarkRift;

namespace Networking.Mines
{
    /// <summary>
    ///     Mine block
    /// </summary>
    public class MineScan : IDarkRiftSerializable
    {
        public ushort X { get; set; }
        public ushort Y { get; set; }
        
        public MineScan()
        { }

        public MineScan(ushort x, ushort y)
        {
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
            e.Writer.Write(X);
            e.Writer.Write(Y);
        }
    }
}