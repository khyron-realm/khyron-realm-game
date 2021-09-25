using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    // Generate the whole mine without missing blocks
    public class GridGenerateAllPositionsForShaping : MonoBehaviour
    {
        private static bool[,] s_values;

        public static bool[,] GenerateValuesForPlacing(int rows, int columns)
        {
            s_values = new bool[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    s_values[i, j] = true;
                }
            }

            return s_values;
        }
    }
}