namespace Networking.Tags
{
    public static class GameTags
    {
        private const ushort Shift = Tags.Game * Tags.TagsPerPlugin;

        public const ushort PlayerConnected = 0 + Shift;
        public const ushort PlayerDisconnected = 1 + Shift;
        
        public const ushort RequestFailed = 2 + Shift;

        public const ushort PlayerData = 3 + Shift;
        public const ushort PlayerDataUnavailable = 4 + Shift;
        public const ushort GameData = 5 + Shift;
        public const ushort GameDataUnavailable = 6 + Shift;
        
        public const ushort ConvertResources = 7 + Shift;
        public const ushort FinishConversion = 8 + Shift;
        public const ushort FinishConversionAccepted = 9 + Shift;
        public const ushort ConversionAccepted = 10 + Shift;
        public const ushort ConversionRejected = 11 + Shift;

        public const ushort UpgradeRobot = 12 + Shift;
        public const ushort FinishUpgrade = 13 + Shift;
        public const ushort FinishUpgradeAccepted = 14 + Shift;
        public const ushort UpgradeRobotAccepted = 15 + Shift;
        public const ushort UpgradeRobotRejected = 16 + Shift;

        public const ushort BuildRobot = 17 + Shift;
        public const ushort FinishBuild = 18 + Shift;
        public const ushort FinishBuildAccepted = 19 + Shift;
        public const ushort CancelInProgressBuild = 20 + Shift;
        public const ushort CancelOnHoldBuild = 21 + Shift;
        public const ushort BuildRobotAccepted = 22 + Shift;
        public const ushort BuildRobotRejected = 23 + Shift;
        
        public const ushort LevelUpdate = 24 + Shift;
        public const ushort ExperienceUpdate = 25 + Shift;
        public const ushort EnergyUpdate = 26 + Shift;
        public const ushort ResourcesUpdate = 27 + Shift;
        public const ushort RobotsUpdate = 28 + Shift;
    }
}