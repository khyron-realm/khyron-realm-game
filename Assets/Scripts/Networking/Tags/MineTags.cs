namespace Networking.Tags
{
    /// <summary>
    ///     Tags for mine plugin
    /// </summary>
    public static class MineTags
    {
        private const ushort Shift = Tags.Mine * Tags.TagsPerPlugin;
        
        public const ushort RequestFailed = 0 + Shift;
        
        public const ushort GetMines = 1 + Shift;
        public const ushort GetMinesFailed = 2 + Shift; 

        public const ushort SaveMine = 3 + Shift;
        public const ushort SaveMineFailed = 4 + Shift;

        public const ushort FinishMine = 5 + Shift;
        public const ushort FinishMineFailed = 6 + Shift;
    }
}