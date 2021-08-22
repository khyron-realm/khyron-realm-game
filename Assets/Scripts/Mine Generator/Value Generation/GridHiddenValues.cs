using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class GridHiddenValues
    {
        // Array of array with hidden values of all mine blocks

        public static int seedHidden;

        private static int[,] _hiddenValues;

        // Possible values for mine blocks
        private enum values {stoneType1, stoneType2};

        /// <summary>
        /// 
        ///     Generate hidden values for the blocks in the mine
        ///     0 - type 1 stone block
        ///     1 - type 2 stone block
        ///     
        ///     Logic: Generate dirt layer --> Generate stone layer --> Add special stone patterns to the mine
        /// 
        /// </summary>
        /// <param name="rows"> Rows number </param>
        /// <param name="columns"> Collumns number </param>
        /// <param name="diversification"> Diversification coeficient for patterns </param>
        /// <returns> 
        /// 
        ///     Hidden values for all blocks in the mine 
        ///     
        /// </returns>
        public static int[,] GenerateHiddenValues(int rows, int columns, float diversification)
        {
            _hiddenValues = new int[rows, columns];

            GenerateStoneLayer(rows, columns);
            AddStonePatternsToMine(rows, columns, diversification);

            return _hiddenValues;
        }


        /// <summary>
        /// 
        /// Generate one type stone layer all the rest of the mine
        /// 
        /// </summary>
        /// <param name="rows"> Number of rows in the mine </param>
        /// <param name="columns"> Number of columns in the mine </param>
        /// <param name="dirt_depth"> Depth of First layer </param>
        private static void GenerateStoneLayer(int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _hiddenValues[i, j] = (int)values.stoneType1;
                }
            }
        }


        /// <summary>
        /// 
        /// Add over the first type of stone random patterns of type 2 stone so user can be immersed in the mine
        /// 
        /// </summary>
        /// <param name="rows"> Number of rows </param>
        /// <param name="columns"> Number of columns </param>
        /// <param name="diversification"> Coeficient for diversifaction of patterns </param>
        /// <param name="dirt_depth"> Depth of First layer </param>
        private static void AddStonePatternsToMine(int rows, int columns, float diversification)
        {
            //float randomNoise = Random.Range(-10000f, 10000f);

            float randomNoise = seedHidden;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    AddPattern(i, j, diversification, randomNoise);
                }
            }
        }


        /// <summary>
        /// 
        /// The method that add patterns randomly to the mine using perlin noise
        /// 
        /// </summary>
        /// <param name="i"> Row position in the mine for block </param>
        /// <param name="j"> Column position in the mine for block </param>
        /// <param name="diversification"> Coeficient for diversifaction of patterns </param>
        /// <param name="randomNoise"> Random coeficient for perlin noise </param>
        private static void AddPattern(int i, int j, float diversification, float randomNoise)
        {
            // Perlin noise 
            //
            // Theory and more --> https://www.redblobgames.com/maps/terrain-from-noise/
            //
            float e1 = Mathf.PerlinNoise(i * diversification + randomNoise, j * diversification + randomNoise);
            float e2 = Mathf.PerlinNoise(2 * i * diversification + randomNoise, 2 * j * diversification + randomNoise);

            float temp = e1 + 0.2f * e2;

            temp = Mathf.RoundToInt(temp/1.2f);

            if (temp == 1)
            {
                _hiddenValues[i, j] = (int)values.stoneType2;
            }
        }
    }
}