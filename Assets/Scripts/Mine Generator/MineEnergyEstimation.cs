using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnergyEstimation
{
    private static List<int> ResourcesCount;

    public static void NumberOfEachResource(int numberOfResources, int rows, int columns, int[,] values)
    {
        ResourcesCount = new List<int>();

        for (int i = 0; i < numberOfResources; i++)
        {
            ResourcesCount.Add(0);
        }      

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if(values[i, j] > 1)
                {
                    ResourcesCount[values[i, j] - 2]++;
                }               
            }
        }

        
        //Debug.Log(ResourcesCount[0] * 1.5f * 2);
        //Debug.Log(ResourcesCount[1] * 1.2f);
        //Debug.Log(ResourcesCount[2] * 0.3f * 4);
    }
}