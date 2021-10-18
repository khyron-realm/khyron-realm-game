using DarkRift;

namespace Networking.GameData
{
    /// <summary>
    ///     Stores the resource data
    /// </summary>
    public class RobotDetails : IDarkRiftSerializable
    {
        public RobotDetails() { }
        
        public RobotDetails(byte id, string name, ushort health, byte movementSpeed, byte miningDamage,
            ushort buildTime, ushort upgradeTime, ushort buildPrice, ushort upgradePrice, byte housingSpace)
        {
            Id = id;
            Name = name;
            Health = health;
            MovementSpeed = movementSpeed;
            MiningDamage = miningDamage;
            BuildTime = buildTime;
            UpgradeTime = upgradeTime;
            BuildPrice = buildPrice;
            UpgradePrice = upgradePrice;
            HousingSpace = housingSpace;
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public ushort Health { get; set; }              // Value
        public byte MovementSpeed { get; set; }         // Blocks / second
        public byte MiningDamage { get; set; }          // Damage / bloc and Damage per self health
        public ushort BuildTime { get; set; }           // Seconds
        public ushort UpgradeTime { get; set; }         // Minute
        public ushort BuildPrice { get; set; }          // Energy
        public ushort UpgradePrice { get; set; }        // Energy
        public byte HousingSpace { get; set; }          // Value


        /// <summary>
        ///     Deserialization method for robot data
        /// </summary>
        /// <param name="e">Deserialize event</param>
        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadByte();
            Name = e.Reader.ReadString();
            Health = e.Reader.ReadUInt16();
            MovementSpeed = e.Reader.ReadByte();
            MiningDamage = e.Reader.ReadByte();
            BuildTime = e.Reader.ReadUInt16();
            UpgradeTime = e.Reader.ReadUInt16();
            BuildPrice = e.Reader.ReadUInt16();
            UpgradePrice = e.Reader.ReadUInt16();
            HousingSpace = e.Reader.ReadByte();
        }

        /// <summary>
        ///     Serialization method for robot data
        /// </summary>
        /// <param name="e">Serialize event</param>
        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(Health);
            e.Writer.Write(MovementSpeed);
            e.Writer.Write(MiningDamage);
            e.Writer.Write(BuildTime);
            e.Writer.Write(UpgradeTime);
            e.Writer.Write(BuildPrice);
            e.Writer.Write(UpgradePrice);
            e.Writer.Write(HousingSpace);
        }
    }
}