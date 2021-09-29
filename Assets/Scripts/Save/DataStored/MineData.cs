using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Save
{
    [System.Serializable]
    public class MineData
    {
        public int HiddenSeed;
        public List<ResourcesData> ResourcesData;

        public MineData(MineValues data)
        {
            HiddenSeed = data.HiddenSeed;
            ResourcesData = new List<ResourcesData>(data.ResourcesData);
        }
    }


    [System.Serializable]
    public class ResourcesData
    {
        public int Seed;
        public float RarityCoeficient;
        public float Frequency;

        public ResourcesData(int Seed, float RarityCoeficient, float Frequency)
        {
            this.Seed = Seed;
            this.RarityCoeficient = RarityCoeficient;
            this.Frequency = Frequency;
        }
    }
}