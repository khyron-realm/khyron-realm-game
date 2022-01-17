namespace Networking.Tags
{
    public static class HeadquartersTags
    {
        private const ushort Shift = Tags.Headquarters * Tags.TagsPerPlugin;

        public const ushort PlayerConnected = 0 + Shift;
        public const ushort PlayerDisconnected = 1 + Shift;
        
        public const ushort RequestFailed = 2 + Shift;

        public const ushort PlayerData = 3 + Shift;
        public const ushort PlayerDataUnavailable = 4 + Shift;
        public const ushort GameData = 5 + Shift;
        public const ushort GameDataUnavailable = 6 + Shift;
        
        public const ushort ConvertResources = 7 + Shift;
        public const ushort ConvertResourcesError = 8 + Shift;
        public const ushort FinishConversion = 9 + Shift;
        public const ushort FinishConversionError = 10 + Shift;

        public const ushort UpgradeRobot = 11 + Shift;
        public const ushort UpgradeRobotError = 12 + Shift;
        public const ushort FinishUpgrade = 13 + Shift;
        public const ushort FinishUpgradeError = 14 + Shift;

        public const ushort BuildRobot = 15 + Shift;
        public const ushort BuildRobotError = 16 + Shift;
        public const ushort FinishBuild = 17 + Shift;
        public const ushort FinishBuildMultiple = 18 + Shift;
        public const ushort FinishBuildError = 19 + Shift;
        public const ushort CancelInProgressBuild = 20 + Shift;
        public const ushort CancelOnHoldBuild = 21 + Shift;
        public const ushort CancelBuildError = 22 + Shift;

        public const ushort UpdateLevel = 23 + Shift;
        public const ushort UpdateLevelError = 24 + Shift;
        public const ushort UpdateEnergy = 25 + Shift;
        public const ushort UpdateEnergyError = 26 + Shift;
        
        public const ushort RemoveBackgroundTask = 27 + Shift;
        public const ushort RemoveBackgroundTaskFailed = 28 + Shift;
    }
}