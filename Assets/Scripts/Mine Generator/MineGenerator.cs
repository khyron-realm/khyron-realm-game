using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TilesData;


namespace Grid
{
    public class MineGenerator : MonoBehaviour
    {
        [SerializeField]
        [Space(20f)]
        private RuleTile _groundTileType1;

        [SerializeField]
        [Space(20f)]
        private RuleTile _groundTileType2;

        [SerializeField]
        [Space(20f)]
        private Vector2Int _startPosition;

        [SerializeField]
        [Space(20f)]
        private int _rows;

        [SerializeField]
        private int _columns;

        [SerializeField]
        [Space(20f)]
        [Range(0, 1)]
        private float _diversification;

        [SerializeField]
        [Space(20f)]
        private int _seedHidden;

        [SerializeField]
        private int _seedVisible;

        [SerializeField]
        private List<int> _resourcesSeed;

        [SerializeField]
        private List<int> _healthOfBlocks;

        [SerializeField]
        [Space(20f)]
        private List<MineResources> _resources;

        [SerializeField]
        [Space(20f)]
        private List<AreaToDestroy> _areaToDestroy;

        private void Start()
        {
            Generate();
        }


        public void Generate()
        {
            GridHiddenValues.seedHidden = _seedHidden;
            GridVisibleValues.seedVisible = _seedVisible;

            int[,] temp_hidden = GridHiddenValues.GenerateHiddenValues(_rows, _columns, _diversification);
            int[,] temp_visible = GridVisibleValues.GenerateVisibleValues(_rows, _columns, _resources, temp_hidden, _resourcesSeed);

            // In future we use a scriptable object with the data stored
            bool[,] temp_placeAble = GridGeneratePosition.GenerateValuesForPlacing(_rows, _columns, _areaToDestroy);

            for (int row = _startPosition.y; row < _rows + _startPosition.y; row++)
            {
                List<StoreDataAboutTiles> temp = new List<StoreDataAboutTiles>();

                for (int col = _startPosition.x; col < _columns + _startPosition.x; col++)
                {
                    if (temp_placeAble[row - _startPosition.y, col - _startPosition.x] == true)
                    {
                        if(temp_hidden[row - _startPosition.y, col - _startPosition.x] == 0)
                        {
                            StoreAllTiles.instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType1);
                            StoreData(temp_visible, row, temp, col, _healthOfBlocks[0]);
                        }
                        else
                        {
                            StoreAllTiles.instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType2);
                            StoreData(temp_visible, row, temp, col, _healthOfBlocks[1]);
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
    }
}