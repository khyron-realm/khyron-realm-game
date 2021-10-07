using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tiles.Tiledata;
using DG.Tweening;
using Manager.Robots.Damage;


namespace Manager.Robots.Mining
{
    public class RobotMining : MonoBehaviour, IMineOperations
    {
        #region "Input data"
        [SerializeField] private RobotsGetDamage _damage;
        [SerializeField] private Ease _robotsMovingType;
        #endregion

        #region "Private members"

        private bool _check;
        private bool _movementFinished = true;
        private float _movementSpeed = 2f;

        private List<Vector3Int> _allPositions;

        private static List<Tuple<float, Vector2Int>> blocksToMine;

        private MineResources _resourceMined;

        private Vector3 _positionOfResource;

        private float dist = 65532;
        private Vector2Int nearBlock;

        #endregion

        public event Action<MineResources, Vector3> OnResourceMined;

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


        public void StartMineOperation(Robot robot, GameObject robotGameObject)
        {
            _damage.GetHealthFromRobot(robot);
            _damage.GetRobotGameObject(robotGameObject);

            StartCoroutine("MiningProcess");
        }


        /// <summary>
        /// The Mining process
        /// </summary>
        private IEnumerator MiningProcess()
        {
            while(true)
            {
                _check = false;
                bool _mined = false;

                Vector2Int block = GenerateAreaOfRobot();
             
                if(StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int(block.x, block.y, 0)) != null && _movementFinished == true)
                {

                    if(StoreAllTiles.Instance.Tiles[block.x][block.y].Resource != null)
                    {
                        StoreAllTiles.Instance.Tilemap.SetTile(new Vector3Int((int)(block.x), (int)(block.y), 0), StoreAllTiles.Instance.Tiles[block.x][block.y].Resource.ResourceTile);
                        
                        _resourceMined = StoreAllTiles.Instance.Tiles[block.x][block.y].Resource;
                        _positionOfResource = new Vector3(block.x, block.y, 0);
                        _mined = true;
                    }

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

                    if(_mined)
                    {
                        OnResourceMined?.Invoke(_resourceMined, _positionOfResource);
                    }                 
                }

                _movementFinished = false;

                float time = TimeForMoving(ref block);

                gameObject.transform.DOMove(new Vector3(block.x + 0.5f, block.y + 0.5f, 0), time).SetEase(_robotsMovingType).OnComplete(() => { _movementFinished = true; });
                yield return new WaitForSeconds(time);

                _damage.DoDamage(20);

                if (StoreAllTiles.Instance.TilesPositions.Count < 1)
                {
                    break;
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
        /// Generates the values of "random" predictible path for robots
        /// </summary>
        private static void GenerateValues()
        {
            int randOne = UnityEngine.Random.Range(-99999, 99999);
            int randTwo = UnityEngine.Random.Range(-99999, 99999);

            for (int i = 0; i < 45; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    float e = Mathf.Pow(Mathf.PerlinNoise(i * 0.2f + randOne, j * 0.3f + randTwo), 1.2f);

                    blocksToMine.Add(new Tuple<float, Vector2Int>(e, new Vector2Int(i, j)));                                                                 
                }
            }           
        }


        /// <summary>
        /// Generate the position of the next block to mine
        /// </summary>
        /// <returns>Position of the next block</returns>
        private Vector2Int GenerateAreaOfRobot()
        {
            Vector2Int positionStandard = nearBlock;
            float keepValue = 0;           
            
            foreach (Vector2Int item in _allPositions)
            {
                Vector2Int robotPosition = new Vector2Int((int)(transform.position.x - 0.5f), (int)(transform.position.y - 0.5f)) + item;

                int position = robotPosition.x * 30 + robotPosition.y;

                if(position >= 0 && position < 1350 && StoreAllTiles.Instance.TilesPositions.Contains(robotPosition))
                {
                    if (blocksToMine[position].Item1 > keepValue && StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int(robotPosition.x, robotPosition.y, 0)) != null)
                    {
                        keepValue = blocksToMine[position].Item1;
                        nearBlock = blocksToMine[position].Item2;

                        if (StoreAllTiles.Instance.Tiles[robotPosition.x][robotPosition.y].Resource != null)
                        {
                            break;
                        }
                    }
                }              
            }

            if(nearBlock == positionStandard)
            {
                FindNearestBlock(new Vector2Int((int)(gameObject.transform.position.x - 0.5f), (int)(gameObject.transform.position.y - 0.5f)));
            }

            StoreAllTiles.Instance.TilesPositions.Remove(nearBlock);

            return nearBlock;
        }


        private void FindNearestBlock(Vector2Int temp)
        {
            dist = 65532;

            foreach (Vector2Int item in StoreAllTiles.Instance.TilesPositions)
            {
                if (Vector2Int.Distance(temp, item) < dist)
                {
                    dist = Vector2Int.Distance(temp, item);
                    
                    nearBlock = item;
                }
            }
        }
    }
}