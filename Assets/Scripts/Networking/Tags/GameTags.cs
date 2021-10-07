namespace Networking.Tags
{
    public static class GameTags
    {
        private const ushort Shift = Tags.Game * Tags.TagsPerPlugin;

        public const ushort PlayerConnected = 0 + Shift;
        public const ushort PlayerDisconnected = 1 + Shift;
        
        public const ushort UserInformation = 2 + Shift;

        public const ushort RequestFailed = 3 + Shift;

        public const ushort PlayerData = 4 + Shift;
        
        public const ushort ConversionStatus = 5 + Shift;
        public const ushort ConversionInProgress = 6 + Shift;
        public const ushort ConversionNotAvailable = 7 + Shift;
        public const ushort ConvertResources = 8 + Shift;
        public const ushort ConversionAccepted = 9 + Shift;
        public const ushort ConversionRejected = 10 + Shift;
        public const ushort ConversionFinished = 11 + Shift;
    }
}