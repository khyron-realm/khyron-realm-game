using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Networking.Mines;
using Tiles.Tiledata;


namespace Mine
{
    public class MineGenerator : MonoBehaviour
    {
        #region "Input fields" 
        [SerializeField] private bool _isAuction;
        [Space(20f)]

        [SerializeField] private RuleTile _nullTile;
        [Space(20f)]

        [SerializeField] private RuleTile _groundTileType11;
        [SerializeField] private RuleTile _groundTileType12;
        [SerializeField] private RuleTile _groundTileType13;

        [SerializeField] private RuleTile _groundTileType21;
        [SerializeField] private RuleTile _groundTileType22;
        [SerializeField] private RuleTile _groundTileType23;
        [Space(20f)]
        

        [SerializeField] private int _rows;    
        [SerializeField] private int _columns;
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
            StoreAllBlocksPositions();

            DataOfTile.NullTile = _nullTile;
        }


        /// <summary>
        /// 
        /// Generate the mine 
        /// Instantiate the tiles on the tileMap 
        /// 
        /// </summary>
        public void Generate()
        {
            int[,] temp_hidden = GridHiddenValues.GenerateHiddenValues(_rows, _columns);
            int[,] temp_visibles = GridVisibleValues.GenerateVisibleValues(_rows, _columns, temp_hidden);

            MineEnergyEstimation.NumberOfEachResource(_resources.Count, _rows, _columns, temp_visibles);

            // Used when user want to create a new shape 
            // bool[,] temp_placeAble = GridGenerateAllPositionsForShaping.GenerateValuesForPlacing(_rows, _columns);

            for (int row = 0; row < _rows; row++)
            {
                List<DataOfTile> temp = new List<DataOfTile>();

                for (int col = 0; col < _columns; col++)
                {
                    if (_areaOfBlocks[row, col] == true) // _areaOfBlocks is replaced with temp_placeAble when user wishes to change the shape of the mine [in unity is done]
                    {
                        if(temp_hidden[row, col] == 0)
                        {
                            if(((row % 3) + (col % 3)) % 3 == 0)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType11);
                                StoreData(_groundTileType11, temp_visibles, row, temp, col, _healthOfBlocks[0], 0, 0);
                            }
                            if(((row % 3) + (col % 3)) % 3 == 1)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType12);
                                StoreData(_groundTileType12, temp_visibles, row, temp, col, _healthOfBlocks[0], 1, 0);
                            }
                            if (((row % 3) + (col % 3)) % 3 == 2)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType13);
                                StoreData(_groundTileType13, temp_visibles, row, temp, col, _healthOfBlocks[0], 2, 0);
                            }
                        }
                        else
                        {
                            if (((row % 3) + (col % 3)) % 3 == 0)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType21);
                                StoreData(_groundTileType21, temp_visibles, row, temp, col, _healthOfBlocks[1], 3, 1);
                            }
                            if(((row % 3) + (col % 3)) % 3 == 1)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType22);
                                StoreData(_groundTileType22, temp_visibles, row, temp, col, _healthOfBlocks[1], 4, 1);
                            }
                            if(((row % 3) + (col % 3)) % 3 == 2)
                            {
                                StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType23);
                                StoreData(_groundTileType23, temp_visibles, row, temp, col, _healthOfBlocks[1], 5, 1);
                            }
                        }                            
                    }
                }
                StoreAllTiles.Instance.Tiles.Add(temp);
            }


            if (_isAuction) return;

            for (int i = 0; i < 45; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (MineManager.MineList[MineManager.CurrentMine].Blocks[i * 30 + j] == false)
                    {
                        StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int(i, j, 0), DataOfTile.NullTile);
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// Saves the block data 
        /// Every block has health or health and resource type
        /// 
        /// </summary>
        /// <param name="temp_visible">The array with visible blocks</param>
        /// <param name="row">The current row</param>
        /// <param name="temp">The list with all data about tiles</param>
        /// <param name="col">The current column</param>
        /// <param name="health">The current health of the block</param>
        private void StoreData(RuleTile tile, int[,] temp_visible, int row, List<DataOfTile> temp, int col, int health, int coeficient, byte type)
        {
            if (temp_visible[row, col] > 1)
            {
                switch (coeficient)
                {
                    case 0:
                        temp.Add(new DataOfTile(health, tile, _resources[temp_visible[row, col] - 2].Soft1, _resources[temp_visible[row, col] - 2], type));
                        break;

                    case 1:
                        temp.Add(new DataOfTile(health, tile, _resources[temp_visible[row, col] - 2].Soft2, _resources[temp_visible[row, col] - 2], type));
                        break;

                    case 2:
                        temp.Add(new DataOfTile(health, tile, _resources[temp_visible[row, col] - 2].Soft3, _resources[temp_visible[row, col] - 2], type));
                        break;

                    case 3:
                        temp.Add(new DataOfTile(health, tile, _resources[temp_visible[row, col] - 2].Hard1, _resources[temp_visible[row, col] - 2], type));
                        break;

                    case 4:
                        temp.Add(new DataOfTile(health, tile, _resources[temp_visible[row, col] - 2].Hard2, _resources[temp_visible[row, col] - 2], type));
                        break;

                    case 5:
                        temp.Add(new DataOfTile(health, tile, _resources[temp_visible[row, col] - 2].Hard3, _resources[temp_visible[row, col] - 2], type));
                        break;
                }              
            }
            else
            {
                temp.Add(new DataOfTile(health, tile, type));
            }
        }


        /// <summary>
        /// Get all blocks positions and store them in a list
        /// </summary>
        private void StoreAllBlocksPositions()
        {
            StoreAllTiles.Instance.Tilemap.CompressBounds();
            BoundsInt bounds = StoreAllTiles.Instance.Tilemap.cellBounds;
            TileBase[] allTiles = StoreAllTiles.Instance.Tilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    if(tile != null)
                    {
                        StoreAllTiles.Instance.TilesPositions.Add(new Vector2Int(x, y));
                    }
                }
            }
        }


        /// <summary>
        /// 
        ///  Convert the List save as SO into 2 array
        ///  
        /// </summary>
        private void Make2DArray()
        {
            for (int i = 0; i < _shape.values.Length; i++)
            {
                _areaOfBlocks[(int)(i / _columns), (int)(i % _columns)] = _shape.values[i];
            }
        }
    }
}