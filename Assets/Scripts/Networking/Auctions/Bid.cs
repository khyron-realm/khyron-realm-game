using DarkRift;

namespace Networking.Auctions
{
    /// <summary>
    ///     Auction bid data
    /// </summary>
    public class Bid : IDarkRiftSerializable
    { 
        public ushort Id { get; set; }
        public string PlayerName { get; set; }
        public uint Amount { get; set; }

        public Bid()
        { }
        
        public Bid(ushort id, string playerName, uint amount)
        {
            Id = id;
            PlayerName = playerName;
            Amount = amount;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            PlayerName = e.Reader.ReadString();
            Amount = e.Reader.ReadUInt32();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}