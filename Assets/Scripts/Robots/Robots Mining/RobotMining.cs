using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiles.Tiledata;
using DG.Tweening;


namespace Manager.Robots.Mining
{
    public class RobotMining : MonoBehaviour
    {
        #region "Private members"
        private bool _check;
        private bool _movementFinished;
        private float _movementSpeed = 6f;
        #endregion


        public void StartMining()
        {
            StartCoroutine("MiningProcess");
        }
        

        private IEnumerator MiningProcess()
        {
            while(true)
            {
                _check = false;
                _movementFinished = false;

                Vector2Int block = GenerateRandomBlockToMine();

                float time = TimeForMoving(ref block);

                gameObject.transform.DOMove(new Vector3(block.x - 0.5f, block.y + 0.25f, 0), time).OnComplete(() => { _movementFinished = true; });
                yield return new WaitForSeconds(time);

                if (_movementFinished == true)
                {
                    while (!_check)
                    {
                        StoreAllTiles.Instance.Tiles[(int)(block.x)][(int)(block.y)].Health -= (int)(40f);

                        if (StoreAllTiles.Instance.Tiles[(int)(block.x)][(int)(block.y)].Health < 0)
                        {
                            StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(block.x), (int)(block.y), 0), null);
                            _check = true;
                        }

                        yield return null;
                    }

                    if (StoreAllTiles.Instance.TilesPositions.Count < 1)
                    {
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// The timerequired for moving the robot for breaking the block
        /// </summary>
        /// <param name="block"> the position of the block </param>
        /// <returns> The time required for moving </returns>
        private float TimeForMoving(ref Vector2Int block)
        {
            float dist = Vector3.Distance(gameObject.transform.position, new Vector3(block.x, block.y, 0));
            float time = dist / _movementSpeed;
            return time;
        }


        /// <summary>
        /// Random block to mine in the mine
        /// </summary>
        /// <returns> the position of the block</returns>
        private Vector2Int GenerateRandomBlockToMine()
        {
            Vector2Int temp = StoreAllTiles.Instance.TilesPositions[Random.Range(0, StoreAllTiles.Instance.TilesPositions.Count)];
            StoreAllTiles.Instance.TilesPositions.Remove(temp);
            return temp;
        }
    }
}