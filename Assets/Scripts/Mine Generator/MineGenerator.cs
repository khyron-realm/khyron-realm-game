using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TilesData;


namespace Grid
{
    //[ExecuteInEditMode]
    public class MineGenerator : MonoBehaviour
    {
        #region "Input fields" 
        [SerializeField] private RuleTile _groundTileType1;  
        [SerializeField] private RuleTile _groundTileType2;
        [Space(20f)]
        

        [SerializeField] private int _rows;    
        [SerializeField] private int _columns;
        [Space(20f)]


        [SerializeField]
        [Range(0, 1)] private float _diversification;
        [Space(20f)]


        [SerializeField] private List<int> _healthOfBlocks;
        [Space(20f)]


        [SerializeField] private List<MineResources> _resources;
        [Space(20f)]


        [SerializeField] private MineShape _shape;
        private bool[,] _areaOfBlocks;
        #endregion

        private void Start()
        {
            _areaOfBlocks = new bool[_rows, _columns];
            Make2DArray();
            Generate();
        }


        public void Generate()
        {
            int[,] temp_hidden = GridHiddenValues.GenerateHiddenValues(_rows, _columns, _diversification);
            int[,] temp_visibles = GridVisibleValues.GenerateVisibleValues(_rows, _columns, _resources, temp_hidden);

            MineEnergyEstimation.NumberOfEachResource(_resources.Count, _rows, _columns, temp_visibles);

            // Used when user want to create a new shape 
            // bool[,] temp_placeAble = GridGenerateAllPositionsForShaping.GenerateValuesForPlacing(_rows, _columns);

            for (int row = 0; row < _rows; row++)
            {
                List<StoreDataAboutTiles> temp = new List<StoreDataAboutTiles>();

                for (int col = 0; col < _columns; col++)
                {
                    if (_areaOfBlocks[row, col] == true) // _areaOfBlocks is replaced with temp_placeAble when user wishes to change the shape of the mine [in unity is done]
                    {
                        if(temp_hidden[row, col] == 0)
                        {
                            StoreAllTiles.instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType1);
                            StoreData(temp_visibles, row, temp, col, _healthOfBlocks[0]);
                        }
                        else
                        {
                            StoreAllTiles.instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType2);
                            StoreData(temp_visibles, row, temp, col, _healthOfBlocks[1]);
                        }                            
                    }
                }
                StoreAllTiles.instance.tiles.Add(temp);
            }
        }


        private void StoreData(int[,] temp_visible, int row, List<StoreDataAboutTiles> temp, int col, int health)
        {
            if (temp_visible[row, col] > 1)
            {
                temp.Add(new StoreDataAboutTiles(health, _resources[temp_visible[row, col] - 2]));
            }
            else
            {
                temp.Add(new StoreDataAboutTiles(health));
            }
        }


        // Convert from 1 dimensional array to 2d array
        private void Make2DArray()
        {
            for (int i = 0; i < _shape.values.Length; i++)
            {
                _areaOfBlocks[(int)(i / _columns), (int)(i % _columns)] = _shape.values[i];
            }
        }
    }
}