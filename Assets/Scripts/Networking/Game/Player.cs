using DarkRift;

namespace Networking.Game
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public ushort Level { get; set; }
        public ushort Experience { get; set; }
        public ushort Energy { get; set; }
        
        public Player(string id, string name, ushort level, ushort experience, ushort energy)
        {
            this.Id = id;
            this.Name = name;
            
            this.Level = level;
            this.Experience = experience;
            this.Energy = energy;
        }

        public void Deserialize(DeserializeEvent e)
        {
            Id = e.Reader.ReadString();
            Name = e.Reader.ReadString();
            Level = e.Reader.ReadUInt16();
            Experience = e.Reader.ReadUInt16();
            Energy = e.Reader.ReadUInt16();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Id);
            e.Writer.Write(Name);
            e.Writer.Write(Level);
            e.Writer.Write(Experience);
            e.Writer.Write(Energy);
        }
    }
}