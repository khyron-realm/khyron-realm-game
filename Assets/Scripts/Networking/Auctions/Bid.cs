using DarkRift;

namespace Networking.Auctions
{
    /// <summary>
    ///     Auction bid data
    /// </summary>
    public class Bid : IDarkRiftSerializable
    { 
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public uint Amount { get; set; }

        public Bid()
        { }
        
        public Bid(uint id, uint userId, uint amount)
        {
            Id = id;
            UserId = userId;
            Amount = amount;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt32();
            UserId = e.Reader.ReadUInt32();
            Amount = e.Reader.ReadUInt32();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}