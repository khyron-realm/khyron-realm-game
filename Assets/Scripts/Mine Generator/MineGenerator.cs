using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        private Color32 _hiddenColor;

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
                            StoreAllTiles.instance.Tilemap.SetColor(new Vector3Int(row, col, 0), _hiddenColor);
                            StoreData(temp_visible, row, temp, col, _healthOfBlocks[0]);
                        }
                        else
                        {
                            StoreAllTiles.instance.Tilemap.SetTile(new Vector3Int(row, col, 0), _groundTileType2);
                            StoreAllTiles.instance.Tilemap.SetColor(new Vector3Int(row, col, 0), _hiddenColor);
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

























        //CreateBlockWithStats(temp_hidden, temp_visible, row, col);

        //private void CreateBlockWithStats(int[,] temp_hidden, int[,] temp_visible, int row, int col)
        //{
        //    GameObject tile = Instantiate(_blockPrefab, transform);

        //    SetSpritesOfBlocks(temp_hidden, row, col, tile);
        //    SetBlockVisibleValue(temp_visible, row, col, tile);

        //    SetHealthForBlocks(temp_hidden, row, col, tile);
        //    SetDropCoeficientforBlocks(temp_hidden, row, col, tile);

        //    PositionTile(row, col, tile);
        //}

        //private void PositionTile(int row, int col, GameObject tile)
        //{
        //    float posX = col * _tileSize;
        //    float posY = row * -_tileSize;

        //    tile.transform.position = new Vector2(posX, posY);
        //}

        //private void SetSpritesOfBlocks(int[,] temp, int row, int col, GameObject tile)
        //{
        //    if (temp[row, col] == 0)
        //    {
        //        int coef = UnityEngine.Random.Range(0, _defaultBlocksType1.Count);
        //        tile.GetComponent<BlockManager>().HiddenSprite = _defaultBlocksType1[coef];
        //        tile.GetComponent<BlockManager>().VisibleSprite = _defaultBlocksType1[coef];
        //    }
        //    else if (temp[row, col] == 1)
        //    {
        //        int coef = UnityEngine.Random.Range(0, _defaultBlocksType2.Count);
        //        tile.GetComponent<BlockManager>().HiddenSprite = _defaultBlocksType2[coef];
        //        tile.GetComponent<BlockManager>().VisibleSprite = _defaultBlocksType2[coef];
        //    }
        //}

        //private void SetBlockVisibleValue(int[,] temp, int row, int col, GameObject tile)
        //{
        //    if (temp[row, col] > 1)
        //    {
        //        tile.GetComponent<BlockManager>().VisibleSprite = _resources[temp[row, col] - 2].sprite;
        //    }
        //}

        //private void SetHealthForBlocks(int[,] temp, int row, int col, GameObject tile)
        //{
        //    tile.GetComponent<IHealth>().InitialHealth = _healthOfBlocks[temp[row, col]];
        //}

        //private void SetDropCoeficientforBlocks(int[,] temp, int row, int col, GameObject tile)
        //{
        //    if (temp[row, col] > 1)
        //    {
        //        tile.GetComponent<BlockResource>().MinimumDrop = _resources[temp[row, col]].dropValueMin;
        //        tile.GetComponent<BlockResource>().MaximumDrop = _resources[temp[row, col]].dropValueMax;
        //    }
        //}
    }
}