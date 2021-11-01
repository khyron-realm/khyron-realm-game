using DarkRift;
using Networking.Mine;

namespace Networking.Auctions
{
    /// <summary>
    ///     Auction room details
    /// </summary>
    public class AuctionRoom : IDarkRiftSerializable
    {
        public ushort Id { get; set; }
        public string Name { get; set; }
        public byte MaxPlayers { get; set; }
        public byte PlayerCount { get; set; }
        public MineData Mine { get; set; }
        public Bid LastBid { get; set; }
        public long EndTime { get; set; }
        public MineScan[] Scans { get; set; }

        public AuctionRoom()
        { }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt16();
            Name = e.Reader.ReadString();
            MaxPlayers = e.Reader.ReadByte();
            PlayerCount = e.Reader.ReadByte();
            Mine = e.Reader.ReadSerializable<MineData>();
            LastBid = e.Reader.ReadSerializable<Bid>();
            EndTime = e.Reader.ReadInt64();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}