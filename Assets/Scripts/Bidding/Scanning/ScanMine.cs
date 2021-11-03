using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;
using Networking.Mine;
using Tiles.Tiledata;


namespace Bidding
{
    public class ScanMine : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private int _vision;
        [SerializeField] private bool _showScannedArea;
        #endregion


        #region "Private members"
        private HashSet<Vector3Int> _blocksToDiscover;
        private bool _done = false;
        #endregion


        private void Awake()
        {
            _blocksToDiscover = new HashSet<Vector3Int>();
        }


        /// <summary>
        /// Changes the sprites of the blocks with the ones specific for resources if exists
        /// </summary>
        /// <param name="temp"> The center of the scan </param>
        /// <returns> A bool that confirms if the scan is done </returns>
        public bool Discover(Vector3Int temp)
        {
            CreateCircleVision();

            if (StoreAllTiles.Instance.Tilemap.GetTile(temp) != null)
            {
                foreach (Vector3Int item in _blocksToDiscover)
                {
                    if (StoreAllTiles.Instance.Tilemap.GetTile(temp + item) != null )
                    {
                        _done = true;

                        if (StoreAllTiles.Instance.Tiles[temp.x + item.x][temp.y + item.y].ResourceTile != null)
                            StoreAllTiles.Instance.Tilemap.SetTile(temp + item, StoreAllTiles.Instance.Tiles[temp.x + item.x][temp.y + item.y].ResourceTile);                      
                    }
                }

                if (AuctionsManager.CurrentAuctionRoom.Scans.Length < 3)
                { 
                    AuctionsManager.AddScan(new MineScan(1, (ushort)temp.x, (ushort)temp.y));
                }
            }

            if (_done)
            {
                _done = false;
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Create a kind of a circle on in the 2d array that represent the circle
        /// </summary>
        private void CreateCircleVision()
        {
            VisionField(_vision, Vector3Int.up, Vector3Int.left);
            VisionField(_vision, Vector3Int.up, Vector3Int.right);
            VisionField(_vision, Vector3Int.down, Vector3Int.left);
            VisionField(_vision, Vector3Int.down, Vector3Int.right);
        }


        /// <summary>
        /// Creates a dial of values for scan
        /// </summary>
        /// <param name="length">Radius of the circle</param>
        /// <param name="one"> Directional vector one </param>
        /// <param name="two"> Directional vector two </param>
        private void VisionField(int length, Vector3Int one, Vector3Int two)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - i; j++)
                {
                    _blocksToDiscover.Add(i * one + j * two);
                }
            }
        }
    }
}