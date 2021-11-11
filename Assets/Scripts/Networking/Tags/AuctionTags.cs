namespace Networking.Tags
{
    public static class AuctionTags
    {
        private const ushort Shift = Tags.Auctions * Tags.TagsPerPlugin;

        public const ushort Create = 0 + Shift;
        public const ushort Join = 1 + Shift;
        public const ushort Leave = 2 + Shift;

        public const ushort GetRoom = 3 + Shift;
        public const ushort GetRoomFailed = 4 + Shift;
        public const ushort GetOpenRooms = 5 + Shift;
        public const ushort GetOpenRoomsFailed = 6 + Shift;

        public const ushort CreateSuccess = 7 + Shift;
        public const ushort CreateFailed = 8 + Shift;
        
        public const ushort JoinSuccess = 9 + Shift;
        public const ushort JoinFailed = 10 + Shift;
        public const ushort LeaveSuccess = 11 + Shift;
        
        public const ushort PlayerJoined = 12 + Shift;
        public const ushort PlayerLeft = 13 + Shift;

        public const ushort StartAuction = 14 + Shift;
        public const ushort StartAuctionSuccess = 15 + Shift;
        public const ushort StartAuctionFailed = 16 + Shift;

        public const ushort AuctionFinished = 17 + Shift;

        public const ushort AddBid = 18 + Shift;
        public const ushort Overbid = 19 + Shift;
        public const ushort AddBidSuccessful = 20 + Shift;
        public const ushort AddBidFailed = 21 + Shift;

        public const ushort AddScan = 22 + Shift;
        public const ushort AddScanFailed = 23 + Shift;

        public const ushort AddFriendToAuction = 24 + Shift;
        public const ushort AddFriendToAuctionSuccessful = 25 + Shift;
        public const ushort AddFriendToAuctionFailed = 26 + Shift;
        
        public const ushort RequestFailed = 27 + Shift;
    }
}