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
        
        public const ushort ConvertResources = 5 + Shift;
        public const ushort ConversionAccepted = 6 + Shift;
        public const ushort ConversionRejected = 7 + Shift;
        public const ushort ConversionFinished = 8 + Shift;
        
        public const ushort UpgradeRobot = 9 + Shift;
        public const ushort UpgradeRobotAccepted = 10 + Shift;
        public const ushort UpgradeRobotRejected = 11 + Shift;
        public const ushort UpgradeRobotFinished = 12 + Shift;
        
        public const ushort BuildRobot = 13 + Shift;
        public const ushort BuildRobotAccepted = 14 + Shift;
        public const ushort BuildRobotRejected = 15 + Shift;
        public const ushort BuildRobotFinished = 16 + Shift;
    }
}