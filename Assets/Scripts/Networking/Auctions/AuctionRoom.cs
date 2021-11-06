using System;
using DarkRift;
using Networking.Mines;

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
        public MineGenerator MineValues { get; set; }
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
            MineValues = e.Reader.ReadSerializable<MineGenerator>();
            LastBid = e.Reader.ReadSerializable<Bid>();
        }

        public void Serialize(SerializeEvent e)
        { }

        /// <summary>
        ///     Adds a scan to the auction mine
        /// </summary>
        /// <param name="scan">The scan made by the players</param>
        /// <returns>True if the bid is added or false otherwise</returns>
        internal void AddScan(MineScan scan)
        {
            Scans = Scans.Append(scan);
        }
    }
    
    public static class Extensions
    {
        public static T[] Append<T>(this T[] array, T item)
        {
            if (array == null || array.Length == 0) 
            {
                return new T[] { item };
            }
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
 
            return array;
        }
    }
}