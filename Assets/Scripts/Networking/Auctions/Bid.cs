using DarkRift;

namespace Networking.Auctions
{
    /// <summary>
    ///     Auction bid data
    /// </summary>
    public class Bid : IDarkRiftSerializable
    { 
        public ushort Id { get; set; }
        public ushort UserId { get; set; }
        public uint Amount { get; set; }

        public Bid()
        { }
        
        public Bid(ushort id, ushort userId, uint amount)
        {
            Id = id;
            UserId = userId;
            Amount = amount;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            UserId = e.Reader.ReadUInt16();
            Amount = e.Reader.ReadUInt32();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}