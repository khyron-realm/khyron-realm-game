using DarkRift;

namespace Networking.Mine
{
    /// <summary>
    ///     Mine seed values
    /// </summary>
    public class MineSeed : IDarkRiftSerializable
    {
        private short Global { get; set; }
        private short Silicon { get; set; }
        private short Lithium { get; set; }
        private short Titanium { get; set; }

        public MineSeed(short global, short silicon, short lithium, short titanium)
        {
            Global = global;
            Silicon = silicon;
            Lithium = lithium;
            Titanium = titanium;
        }

        public MineSeed()
        { }

        public void Deserialize(DeserializeEvent e)
        {
            Global = e.Reader.ReadInt16();
            Silicon = e.Reader.ReadInt16();
            Lithium = e.Reader.ReadInt16();
            Titanium = e.Reader.ReadInt16();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}