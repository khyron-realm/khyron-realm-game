using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public static class GridGenerateAllPositionsForShaping
    {
        private static bool[,] s_values;

        /// <summary>
        /// Generates blocks for every position in the mine
        /// Further used with ShapeTheMine class to create a shape of the mine
        /// </summary>
        /// <param name="rows"> Number of rown in the mine </param>
        /// <param name="columns"> Number of columns in the mine </param>
        /// <returns></returns>
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