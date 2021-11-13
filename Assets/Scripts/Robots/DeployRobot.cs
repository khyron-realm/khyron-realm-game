using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tiles.Tiledata;
using CameraActions;
using DG.Tweening;
using Networking.Headquarters;


/// <summary>
/// 
/// Deploy robot 
/// 
/// User press the button --> select a tile in the safe area --> that tile gets destroyed --> robot is placed and activated 
/// 
/// </summary>

namespace Manager.Robots
{
    public class DeployRobot : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private GameObject _robotMiner;
        [SerializeField] private GameObject _robotVision;

        [SerializeField] private Button _button;
        #endregion


        #region "Private members"

        private IMineOperations _mining;
        private Coroutine _corouteDeploy;
        private bool _once = false;

        private static RobotSO _robotSelected;
        private static GameObject robotToDeploy;
        private List<RobotSO> _list;

        private Text _infoText;

        public static float ZPosition = 0;

        private GameObject _robotsToInstantiate;

        #endregion


        private void Awake()
        {
            _robotsToInstantiate = GameObject.Find("RobotsPool");

            _button.onClick.AddListener(StartDeployOperation);
            RobotManagerUIForMine.OnButtonPressed += RobotMine;

            TapGeneralPurpose.OnTapDetected += Deploy;
        }


        /// <summary>
        /// Deselect the button associted with the robot
        /// </summary>
        public void DeselectRobot()
        {
            if(_corouteDeploy != null)
                StopCoroutine(_corouteDeploy);
            _once = false;
        }


        /// <summary>
        /// Select the button associted with the robot
        /// </summary>
        public void StartDeployOperation()
        {
            if(_once == false)
            {
                _corouteDeploy = StartCoroutine(TapGeneralPurpose.Instance.CheckForTap());
                _once = true;
            }              
        }
        
        
        /// <summary>
        /// Deploy coroutine that works until buttons is deselected or robot is deployed
        /// </summary>
        private void Deploy(Vector3 input, bool temp)
        {
            if(_once)
            {
                DeployRobotInTheMap(new Vector3Int((int)input.x, (int)input.y, 0));
            }                                               
        }


        /// <summary>
        /// Deploy the robot at the coordonates specified if certain criterias are met
        /// </summary>
        /// <param name="temp"> The position for deploy </param>
        private void DeployRobotInTheMap(Vector3Int temp)
        {
            if (!StoreAllTiles.Instance.TilesPositions.Contains((Vector2Int)temp)) return;
            
            GameObject robot;

            StoreAllTiles.Instance.Tilemap.SetTile(temp, DataOfTile.NullTile);
            StoreAllTiles.Instance.Tiles[temp.x][temp.y].Health = -1;

            robot = Instantiate(robotToDeploy);
            robot.transform.SetParent(_robotsToInstantiate.transform);

            ZPosition -= 0.1f;

            robot.transform.position = new Vector3(temp.x + 0.5f, temp.y + 0.5f + robotToDeploy.transform.position.y, ZPosition);

            _mining = robot.GetComponent<IMineOperations>();
            _mining.StartMineOperation(_robotSelected, robot);

            _list.RemoveAt(_list.Count - 1);

            HeadquartersManager.Player.Robots[_robotSelected.RobotId].Count -= 1;

            if (_list.Count < 1)
            {
                _button.onClick.RemoveAllListeners();

                _button.transform.DOLocalMoveX(-1, 0.4f).OnComplete(() => _button.transform.gameObject.SetActive(false));
                _button.image.DOColor(new Color(0, 0, 0, 0), 0.4f);
            }
            else
            {
                _infoText.text = _list.Count.ToString();
            }                   
        }


        /// <summary>
        /// Selects the robot that will be deployed in the mine
        /// </summary>
        /// <param name="list"></param>
        /// <param name="text"></param>
        private void RobotMine(List<RobotSO> list, Text text)
        {
            _list = list;
            _robotSelected = _list[0];
            _infoText = text;


            switch (_robotSelected.RobotId)
            {
                case 0:
                    robotToDeploy = _robotMiner;
                    break;
                case 1:
                    robotToDeploy = _robotVision;
                    break;
                case 2:
                    robotToDeploy = _robotVision;
                    break;
            }
        }


        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
            RobotManagerUIForMine.OnButtonPressed -= RobotMine;

            TapGeneralPurpose.OnTapDetected -= Deploy;
        }
    }
}