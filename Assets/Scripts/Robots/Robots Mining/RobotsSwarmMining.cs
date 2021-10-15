using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiles.Tiledata;
using DG.Tweening;

namespace Manager.Robots.Mining
{
    public class RobotsSwarmMining : MonoBehaviour, IMineOperations
    {
        [SerializeField] private Ease _robotsMovingType;

        private Vector2Int _initPosition;
        private Vector2Int _lastPosition;

        private int _radius;

        private bool _movementFinished;

        private static readonly List<Vector2Int> _positionList = new List<Vector2Int>{ Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        private void Start()
        {
            Vector2Int initPosition = new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y) - new Vector2Int(_radius, _radius);
            Vector2Int LastPosition = new Vector2Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y) + new Vector2Int(_radius, _radius);
        }


        public void StartMineOperation(Robot robot, GameObject robotGameObject)
        {
            StartCoroutine(MineBlocks());        
        }


        private IEnumerator MineBlocks()
        {
            while (true)
            {
                bool _check = false;

                Vector2Int block = GetBlockToMine();

                while (!_check)
                {
                    StoreAllTiles.Instance.Tiles[(int)(block.x)][(int)(block.y)].Health -= (int)(20f);

                    if (StoreAllTiles.Instance.Tiles[(int)(block.x)][(int)(block.y)].Health < 0)
                    {
                        StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(block.x), (int)(block.y), 0), null);
                        _check = true;
                    }

                    yield return null;
                }

                yield return MoveTheObjectToThePosition(block);
            }
        }


        private Vector2Int GetBlockToMine()
        {
            List<Vector2Int> availableBlocks = new List<Vector2Int>();
            foreach (Vector2Int item in _positionList)
            {
                Vector2Int position = new Vector2Int((int)gameObject.transform.position.x + item.x, (int)gameObject.transform.position.y + item.y);

                if(StoreAllTiles.Instance.Tilemap.GetTile((Vector3Int)position) != null && CheckIfBlockIsInPerimeter(position))
                {
                    availableBlocks.Add(position);
                }
            }

            if(availableBlocks.Count > 0)
            {
                int randomPosition = Random.Range(0, availableBlocks.Count);
                return availableBlocks[randomPosition];
            }
            else
            {
                return Vector2Int.up;
            }
            
        }


        private bool CheckIfBlockIsInPerimeter(Vector2Int position)
        {
            if(position.x > _initPosition.x && position.x < _lastPosition.x)
            {
                if(position.y > _initPosition.y && position.y < _lastPosition.y)
                {
                    return true;
                }
            }

            return false;
        }


        private IEnumerator MoveTheObjectToThePosition(Vector2Int block)
        {
            gameObject.transform.DOMove(new Vector3(block.x + 0.5f, block.y + 0.5f, 0), 1).SetEase(_robotsMovingType).OnComplete(() =>
            {
                _movementFinished = true;
            });
            yield return new WaitForSeconds(1);
        }
    }
}