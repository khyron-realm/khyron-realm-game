using DarkRift;
using Networking.Mine;

namespace Networking.Auctions
{
    /// <summary>
    ///     Auction room details
    /// </summary>
    public class AuctionRoom : IDarkRiftSerializable
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public bool HasStarted { get; set; }
        public long EndTime { get; set; }
        public byte PlayerCount { get; set; }
        public MineGenerationValues MineValues { get; set; }
        public Bid LastBid { get; set; }
        public MineScan[] Scans { get; set; }

        public AuctionRoom()
        { }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadUInt32();
            Name = e.Reader.ReadString();
            HasStarted = e.Reader.ReadBoolean();
            EndTime = e.Reader.ReadInt64();
            PlayerCount = e.Reader.ReadByte();
            MineValues = e.Reader.ReadSerializable<MineGenerationValues>();
            LastBid = e.Reader.ReadSerializable<Bid>();
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}