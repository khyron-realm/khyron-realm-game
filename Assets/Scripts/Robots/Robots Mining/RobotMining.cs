using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiles.Tiledata;
using DG.Tweening;
using Manager.Robots.Damage;


namespace Manager.Robots.Mining
{
    public class RobotMining : MonoBehaviour
    {
        [SerializeField] private RobotsGetDamage _damage;

        #region "Private members"

        private bool _check;
        private bool _movementFinished;
        private float _movementSpeed = 6f;

        private List<Vector3Int> _allPositions;
        private float _offSet;

        #endregion

        #region "Directions in the mine"

        public static Vector3Int Up;
        public static Vector3Int Down;
        public static Vector3Int Right;
        public static Vector3Int Left;

        public static Vector3Int UpLeft;
        public static Vector3Int UpRight;
        public static Vector3Int DownLeft;
        public static Vector3Int DownRight;

        #endregion

        private void Awake()
        {
            Up = Vector3Int.up;
            Down = Vector3Int.down;
            Right = Vector3Int.right;
            Left = Vector3Int.left;
            UpLeft = Up + Left;
            UpRight = Up + Right;
            DownLeft = Down + Left;
            DownRight = Down + Right;

            _allPositions = new List<Vector3Int> { Up, Down, Right, Left, UpLeft, UpRight, DownLeft, DownRight };
        }


        public void StartMining(Robot robot, GameObject robotGameObject)
        {
            _damage.GetHealthFromRobot(robot);
            _damage.GetRobotGameObject(robotGameObject);

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
                WayOfMining(block, time);

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

                    _damage.DoDamage(20);

                    if (StoreAllTiles.Instance.TilesPositions.Count < 1)
                    {
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// The way of orientation for the robot left or right mining
        /// </summary>
        /// <param name="block"> The position of the block </param>
        /// <param name="time"> The time required to go to the block </param>
        private void WayOfMining(Vector2Int block, float time)
        {
            if (block.x > gameObject.transform.position.x)
            {
                gameObject.transform.DOMove(new Vector3(block.x + 0.5f - 1f, block.y + 0.5f, 0), time).OnComplete(() => { _movementFinished = true; });
            }
            else
            {
                gameObject.transform.DOMove(new Vector3(block.x + 0.5f + 1f, block.y + 0.5f, 0), time).OnComplete(() => { _movementFinished = true; });
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


        //test
        private Vector2Int BlockToMine1()
        {
            Vector3Int robotPos = new Vector3Int((int)(gameObject.transform.position.x), (int)(gameObject.transform.position.y), 0);
            
            int setIndex = Random.Range(0, _allPositions.Count - 1);
            Vector3Int temp = _allPositions[setIndex] + robotPos;

            StoreAllTiles.Instance.TilesPositions.Remove((Vector2Int)temp);

            return (Vector2Int)temp;
        }
    }
}