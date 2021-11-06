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
        { }

        public void Deserialize(DeserializeEvent e)
        {
            var generationSeed = e.Reader.ReadInt32();
            var randomGenerator = new Random(generationSeed);
            
            Seed = randomGenerator.Next(-1000000, 1000000);
            RarityCoefficient = (ushort) randomGenerator.Next(18000, 24000);
            Frequency = (ushort) randomGenerator.Next(18000, 24000);
        }

        public void Serialize(SerializeEvent e)
        { }
    }
}