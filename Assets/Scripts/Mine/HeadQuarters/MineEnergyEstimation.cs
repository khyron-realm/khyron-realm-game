using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class MineEnergyEstimation
    {
        private static List<int> s_resourcesCount;

        /// <summary>
        /// 
        /// Store in a static list the number of each resource
        /// 
        /// </summary>
        /// <param name="numberOfResources"> Total number of resources </param>
        /// <param name="rows"> The number of rows </param>
        /// <param name="columns"> The number of columns </param>
        /// <param name="values"> The visible values of the mine </param>
        public static void NumberOfEachResource(int numberOfResources, int rows, int columns, int[,] values)
        {
            s_resourcesCount = new List<int>();

            for (int i = 0; i < numberOfResources; i++)
            {
                s_resourcesCount.Add(0);
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (values[i, j] > 1)
                    {
                        s_resourcesCount[values[i, j] - 2]++;
                    }
                }
            }

            //Debug.Log(ResourcesCount[0] * 1.5f * 2);
            //Debug.Log(ResourcesCount[1] * 1.2f);
            //Debug.Log(ResourcesCount[2] * 0.3f * 4);
        }
    }
}