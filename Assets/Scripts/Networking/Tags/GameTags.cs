namespace Networking.Tags
{
    public static class GameTags
    {
        private const ushort Shift = Tags.Game * Tags.TagsPerPlugin;

        public const ushort PlayerConnected = 0 + Shift;
        public const ushort PlayerDisconnected = 1 + Shift;
        public const ushort PlayerInformation = 2 + Shift;

        public const ushort RequestFailed = 3 + Shift;

        public const ushort PlayerData = 4 + Shift;
    }
}