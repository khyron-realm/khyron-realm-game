using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

public class RefreshMineValues : MonoBehaviour
{
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

    public static int GenerateRandomValuesForSeeds()
    {
        return Random.Range(-99999, 99999);
    }

    public static float GenerateRandomValuesForCoeficients()
    {
        return Random.Range(0.18f, 0.24f);
    }
}