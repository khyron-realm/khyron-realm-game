using System;
using DarkRift;

namespace Networking.Mines
{
    public class ResourcesData : IDarkRiftSerializable
    {
        public int Seed;
        public ushort RarityCoefficient;
        public ushort Frequency;

        public ResourcesData()
        {
            Random random = new Random(Environment.TickCount);
            
            Seed = random.Next(-1000000, 1000000);
            RarityCoefficient = (ushort) random.Next(18000, 24000);
            Frequency = (ushort) random.Next(18000, 24000);
        }

        public void Deserialize(DeserializeEvent e)
        {
            Seed = e.Reader.ReadInt32();
            RarityCoefficient = e.Reader.ReadUInt16();
            Frequency = e.Reader.ReadUInt16();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Seed);
            e.Writer.Write(RarityCoefficient);
            e.Writer.Write(Frequency);
        }
    }
}