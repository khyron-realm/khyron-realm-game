namespace Networking.Tags
{
    public static class GameTags
    {
        private const ushort Shift = Tags.Game * Tags.TagsPerPlugin;

        public const ushort PlayerConnectTag = 0 + Shift;
        public const ushort PlayerDisconnectTag = 1 + Shift;
        public const ushort PlayerInformationTag = 2 + Shift;

        public const ushort RequestFailed = 3 + Shift;

        public const ushort PlayerDataTag = 4 + Shift;
    }
}