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
        public const ushort PlayerDataUnavailable = 5 + Shift;
        
        public const ushort ConvertResources = 6 + Shift;
        public const ushort CancelConversion = 7 + Shift;
        public const ushort CancelConversionAccepted = 8 + Shift;
        public const ushort ConversionAccepted = 9 + Shift;
        public const ushort ConversionRejected = 10 + Shift;
        public const ushort ConversionFinished = 11 + Shift;
        
        public const ushort UpgradeRobot = 12 + Shift;
        public const ushort CancelUpgrade = 13 + Shift;
        public const ushort CancelUpgradeAccepted = 14 + Shift;
        public const ushort UpgradeRobotAccepted = 15 + Shift;
        public const ushort UpgradeRobotRejected = 16 + Shift;
        public const ushort UpgradeRobotFinished = 17 + Shift;
        
        public const ushort BuildRobot = 18 + Shift;
        public const ushort CancelBuild = 19 + Shift;
        public const ushort CancelBuildAccepted = 20 + Shift;
        public const ushort BuildRobotAccepted = 21 + Shift;
        public const ushort BuildRobotRejected = 22 + Shift;
        public const ushort BuildRobotFinished = 23 + Shift;
    }
}