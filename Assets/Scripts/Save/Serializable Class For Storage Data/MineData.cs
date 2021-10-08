using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Save
{
    /// <summary>
    /// Serialized class that store data about Mine Generation
    /// </summary>
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


    /// <summary>
    /// Serialized class that stores data about resources and it is used in MineData
    /// </summary>
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