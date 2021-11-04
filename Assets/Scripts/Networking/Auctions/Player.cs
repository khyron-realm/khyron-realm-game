using DarkRift;

namespace Networking.Auctions
{
    /// <summary>
    ///     
    /// </summary>
    public class Player : IDarkRiftSerializable
    {
        public string Name { get; set; }
        public bool IsHost { get; private set; }
        
        public Player()
        { }

        public void Deserialize(DeserializeEvent e)
        {
            Name = e.Reader.ReadString();
            IsHost = e.Reader.ReadBoolean();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}