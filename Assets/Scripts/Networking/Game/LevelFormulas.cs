using DarkRift;

namespace Networking.Game
{
    public class LevelFormulas : IDarkRiftSerializable
    {
        public byte Id;
        public double A;
        public double B;
        public double C;

        public LevelFormulas() {}
        
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

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadByte();
            A = e.Reader.ReadDouble();
            B = e.Reader.ReadDouble();
            C = e.Reader.ReadDouble();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(A);
            e.Writer.Write(B);
            e.Writer.Write(C);
        }
    }
}