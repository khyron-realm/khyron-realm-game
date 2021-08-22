using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class GridVisibleValues
    {
        public static int seedVisible;

        // Array of array with visible values of all mine blocks
        private static int[,] _visibleValues;

        /// <summary>
        /// 
        /// Generate visible values for the blocks in the mine
        ///     0 - type 1 stone block
        ///     1 - type 2 stone block
        ///     
        ///     Codes {2 - ...} are alocated for resources 
        /// 
        ///     Logic: Create visible values for all standard blocks in the mine[stone,dirt] --> Add resources to the mine
        /// 
        /// </summary>
        /// <param name="rows"> Rows number </param>
        /// <param name="columns"> Collumns number </param>
        /// <param name="allMineResources"> List with all resources to be generated in the mine and their settings </param>
        /// <param name="temp_hidden"> Hidden values for mine </param>
        /// <returns>
        ///     
        ///     Visible values for all blocks in the mine [stone, dirt, resources]
        /// 
        /// </returns>
        public static int[,] GenerateVisibleValues(int rows, int columns, List<MineResources> allMineResources, int[,] temp_hidden, List<int> seeds)
        {
            _visibleValues = new int[rows, columns];

            CreateVisibleValuesForStadardBlocks(rows, columns, temp_hidden);
            AddResourcesToTheMine(rows, columns, allMineResources, seeds);

            return _visibleValues;
        }


        /// <summary>
        /// 
        /// Generate visible values for the standard blocks in the mine ... dirt, stone, ...
        /// 
        /// </summary>
        /// <param name="rows"> Number o rows of the mine </param>
        /// <param name="columns"> Number of columns of the mine </param>
        /// <param name="temp_hidden"> Array of array of hidden values of blocks </param>
        private static void CreateVisibleValuesForStadardBlocks(int rows, int columns, int[,] temp_hidden)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (temp_hidden[i, j] == 0)
                    {
                        _visibleValues[i, j] = 0;
                    }
                    if (temp_hidden[i, j] == 1)
                    {
                        _visibleValues[i, j] = 1;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// Add resources to the mine as user requested
        /// 
        /// </summary>
        /// <param name="columns"> Number of columns in the mine </param>
        /// <param name="allMineResources"> The list of all resources with their settings </param>
        private static void AddResourcesToTheMine(int rows, int columns, List<MineResources> allMineResources, List<int> seeds)
        {
            foreach (MineResources resource in allMineResources)
            {
                // create index for the respective resource
                int code = allMineResources.IndexOf(resource) + 2;


                //float randomNoise = Random.Range(-1000f, 1000f);
                float randomNoise = seeds[code - 2];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        GenerateChunks(resource.frequency, i, j, code, randomNoise, resource.rarityCoeficient);
                    }
                }
            }
        }

        
        /// <summary>
        /// 
        /// Generate chunks of every resource using perlin noise as user requested
        /// 
        /// </summary>
        /// <param name="frequency"> Average dimension of a chunk </param>
        /// <param name="i"> Index i </param>
        /// <param name="j"> Index j </param>
        /// <param name="code"> Resource generated code </param>
        /// <param name="randomNoise"> Random number that enters in calculation of Perlin noise </param>
        /// <param name="rarityCoeficient"> Coeficient of rarity </param>
        private static void GenerateChunks(float frequency, int i, int j, int code, float randomNoise, float rarityCoeficient)
        {
            // # chunkDimension is Frequency 
            // # Perlin noise 
            // Theory and more --> https://www.redblobgames.com/maps/terrain-from-noise/
            //
            float e1 = Mathf.PerlinNoise(i * frequency + randomNoise, j * frequency + randomNoise);
            float e2 = Mathf.PerlinNoise( 2 * i * frequency + randomNoise, 2 * j * frequency + randomNoise);

            float temp = e1 + 0.3f * e2;
            temp = temp / 1.3f;
            temp = Mathf.Pow(temp, 1.2f);

            if (temp < rarityCoeficient)
            {
                _visibleValues[i, j] = code;    
            }
        }
    }
}