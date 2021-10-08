using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

namespace Mine
{
    public class RefreshMineValues : MonoBehaviour
    {
        /// <summary>
        /// Refreshes the values for the mine generation
        /// </summary>
        /// <param name="HiddenSeed"> The seed to generate the mine </param>
        /// <param name="ResourcesData"> Data about resources so can be generated </param>
        public void DoTheRefresh(ref int HiddenSeed, ref List<ResourcesData> ResourcesData)
        {
            HiddenSeed = GenerateRandomValuesForSeeds();

            ResourcesData.Clear();
            ResourcesData.Add(new ResourcesData(0, 0, 0));
            ResourcesData.Add(new ResourcesData(0, 0, 0));
            ResourcesData.Add(new ResourcesData(0, 0, 0));

            for (int i = 0; i < ResourcesData.Count; i++)
            {
                ResourcesData[i].Seed = GenerateRandomValuesForSeeds();
                ResourcesData[i].RarityCoeficient = GenerateRandomValuesForCoeficients();
                ResourcesData[i].Frequency = GenerateRandomValuesForCoeficients();
            }


        }


        /// <summary>
        /// Generate a random number for the seeds
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomValuesForSeeds()
        {
            return Random.Range(-99999, 99999);
        }


        /// <summary>
        /// Generate a random number the coeficients of the mine [RarityCoeficient and Frequency]
        /// </summary>
        /// <returns></returns>
        public static float GenerateRandomValuesForCoeficients()
        {
            return Random.Range(0.18f, 0.24f);
        }
    }
}