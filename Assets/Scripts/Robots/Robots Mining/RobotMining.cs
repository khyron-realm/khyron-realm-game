using System;
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
        private float _movementSpeed = 2f;

        private bool _temp = false;

        private List<Vector3Int> _allPositions;

        private static List<Tuple<float, Vector2Int>> blocksToMine;

        #endregion

        private void Awake()
        {
            blocksToMine = new List<Tuple<float, Vector2Int>>();
            GenerateValues();

            _allPositions = new List<Vector3Int>();

            _allPositions.Add(Vector3Int.up);
            _allPositions.Add(Vector3Int.down);
            _allPositions.Add(Vector3Int.right);
            _allPositions.Add(Vector3Int.left);
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

                Vector2Int block = GenerateAreaOfRobot();

                float time = TimeForMoving(ref block);
                WayOfMining(block, time);

                yield return new WaitForSeconds(time);

                if (StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int(block.x, block.y, 0)) != null)
                {
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
                    }
                }

                 _damage.DoDamage(20);

                if (StoreAllTiles.Instance.TilesPositions.Count < 1)
                {
                    break;
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
                gameObject.transform.DOMove(new Vector3(block.x + 0.5f, block.y + 0.5f, 0), time).OnComplete(() => { _movementFinished = true; });
            }
            else
            {
                gameObject.transform.DOMove(new Vector3(block.x + 0.5f, block.y + 0.5f, 0), time).OnComplete(() => { _movementFinished = true; });
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
            Vector2Int temp = StoreAllTiles.Instance.TilesPositions[UnityEngine.Random.Range(0, StoreAllTiles.Instance.TilesPositions.Count)];
            StoreAllTiles.Instance.TilesPositions.Remove(temp);
            return temp;
        }


        //// test
        //private Vector2Int PatternGridMining()
        //{
        //    _numberOfBlocksOnARow++;

        //    Vector3Int blockToMine;
        //    Vector3Int robotPos = new Vector3Int((int)(gameObject.transform.position.x), (int)(gameObject.transform.position.y), 0);

        //    if (_numberOfBlocksOnARow % 5 == 0)
        //    {             
        //        blockToMine = robotPos + Down;
        //        _temp = !_temp;

        //        return (Vector2Int)blockToMine;               
        //    }

        //    if(_temp)
        //    {               
        //        blockToMine = robotPos + Right;
        //    }
        //    else
        //    {
        //        blockToMine = robotPos + Left;
        //    }

        //    return (Vector2Int)blockToMine;           
        //}


        private static void GenerateValues()
        {
            int randOne = UnityEngine.Random.Range(-99999, 99999);
            int randTwo = UnityEngine.Random.Range(-99999, 99999);

            //float nx;
            //float ny;


            for (int i = 0; i < 45; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    //if(StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int(i, j ,0)) != null)
                    
                    //nx = i / 45 - 0.5f;
                    //ny = j / 30 - 0.5f;

                    //float d = Mathf.Abs(nx) + Mathf.Abs(ny);

                    float e = Mathf.Pow(Mathf.PerlinNoise(i * 0.2f + randOne, j * 0.3f + randTwo), 1.2f);
                    //e = (1 + e - d) / 2;

                    blocksToMine.Add(new Tuple<float, Vector2Int>(e, new Vector2Int(i, j)));                                                                 
                }
            }           

            //blocksToMine.Sort((a, b) => b.Item1.CompareTo(a.Item1));
        }


        //private Vector2Int MineBlock()
        //{
        //    Vector2Int temp = blocksToMine[0].Item2;

        //    if(blocksToMine.Count > 1)
        //    {
        //        blocksToMine.RemoveAt(0);
        //    }
            
        //    return temp;
        //}



        private Vector2Int GenerateAreaOfRobot()
        {
            float keepValue = 0;

            Vector2Int food = StoreAllTiles.Instance.TilesPositions[UnityEngine.Random.Range(0, StoreAllTiles.Instance.TilesPositions.Count)];

            foreach (Vector2Int item in _allPositions)
            {
                Vector2Int robotPosition = new Vector2Int((int)(transform.position.x -0.5f), (int)(transform.position.y - 0.5f)) + item;

                int position = robotPosition.x * 30 + robotPosition.y;

                if(position >= 0 && position < 1350 && StoreAllTiles.Instance.TilesPositions.Contains(robotPosition))
                {
                    if (blocksToMine[position].Item1 > keepValue && StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int(robotPosition.x, robotPosition.y, 0)) != null)
                    {
                        keepValue = blocksToMine[position].Item1;
                        food = blocksToMine[position].Item2;
                    }
                }              
            }

            StoreAllTiles.Instance.TilesPositions.Remove(food);

            return food;
        }
    }
}