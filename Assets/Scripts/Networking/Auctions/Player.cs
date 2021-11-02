using DarkRift;

namespace Networking.Auctions
{
    /// <summary>
    ///     
    /// </summary>
    public class Player : IDarkRiftSerializable
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public bool IsHost { get; private set; }
        
        public Player()
        { }

        public Player(uint id, string name, bool isHost)
        {
            Id = id;
            IsHost = isHost;
            Name = name;
        }
        
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Name = e.Reader.ReadString();
            IsHost = e.Reader.ReadBoolean();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}