using DarkRift;

namespace Networking.Auctions
{
    /// <summary>
    ///     
    /// </summary>
    public class Player : IDarkRiftSerializable
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public bool IsHost { get; private set; }
        
        public Player(ushort id, string name, bool isHost)
        {
            Id = id;
            IsHost = isHost;
            Name = name;
        }

        public Player()
        {
        }

        public void SetHost(bool isHost)
        {
            IsHost = isHost;
        }
        
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Name = e.Reader.ReadString();
            IsHost = e.Reader.ReadBoolean();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(IsHost);
        }
    }
}