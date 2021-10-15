namespace Networking.GameData
{
    public class LevelFormulas
    {
        public byte Id;
        public float A;
        public float B;
        public float C;

        public LevelFormulas(byte id, float a, float b, float c)
        {
            Id = id;
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue">Initial value for level 1</param>
        /// <param name="level">The current level (1<level<maxLevel)</param>
        /// <returns>The calculated value for the level</returns>
        public ushort Experience(ushort initialValue, byte level)
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue">Initial value for level 1</param>
        /// <param name="level">The current level (1<level<maxLevel)</param>
        /// <returns>The calculated value for the level</returns>
        public ushort RobotHealth(ushort initialValue, byte level)
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue">Initial value for level 1</param>
        /// <param name="level">The current level (1<level<maxLevel)</param>
        /// <returns>The calculated value for the level</returns>
        public ushort RobotMovementSpeed(ushort initialValue, byte level)
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue">Initial value for level 1</param>
        /// <param name="level">The current level (1<level<maxLevel)</param>
        /// <returns>The calculated value for the level</returns>
        public ushort RobotMiningSpeed(ushort initialValue, byte level)
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue">Initial value for level 1</param>
        /// <param name="level">The current level (1<level<maxLevel)</param>
        /// <returns>The calculated value for the level</returns>
        public ushort RobotBuildTime(ushort initialValue, byte level)
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue">Initial value for level 1</param>
        /// <param name="level">The current level (1<level<maxLevel)</param>
        /// <returns>The calculated value for the level</returns>
        public ushort RobotUpgradeTime(ushort initialValue, byte level)
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue">Initial value for level 1</param>
        /// <param name="level">The current level (1<level<maxLevel)</param>
        /// <returns>The calculated value for the level</returns>
        public ushort RobotPrice(ushort initialValue, byte level)
        {
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialValue">Initial value for level 1</param>
        /// <param name="level">The current level (1<level<maxLevel)</param>
        /// <returns>The calculated value for the level</returns>
        public ushort ResourceConversionRate(ushort initialValue, byte level)
        {
            return 0;
        }
    }
}