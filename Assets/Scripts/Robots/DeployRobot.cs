using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Tiles.Tiledata;
using CameraActions;
using Manager.Robots.Mining;
using DG.Tweening;


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
        [SerializeField] private GameObject _robot;
        [SerializeField] private Button _button;
        #endregion

        #region "Private members"

        private bool _hasMoved = false;
        private bool check = true;

        private RobotMining _mining;

        #endregion

        private void Awake()
        {      
            _button.onClick.AddListener(StartDeployOperation);
        }


        /// <summary>
        /// Deselect the button associted with the robot
        /// </summary>
        public void DeselectRobot()
        {
            StopCoroutine("Deploy");
        }


        /// <summary>
        /// Select the button associted with the robot
        /// </summary>
        public void StartDeployOperation()
        {
            StartCoroutine("Deploy");
        }
        
        
        /// <summary>
        /// Deploy coroutine that works until buttons is deselected or robot is deployed
        /// </summary>
        private IEnumerator Deploy()
        {          
            while (check)
            {
                Vector3Int temp = UserTouch.TouchPositionInt(0);
                Vector3Int nullVector = new Vector3Int(-99999, -99999, -99999);

                if(UserTouch.TouchPhaseMoved(0))
                {
                    _hasMoved = true;
                }

                // if finger have moved, dont deploy the robot cause user is searching for an area to deploy
                if (UserTouch.TouchPhaseEnded(0) && _hasMoved == true)
                {
                    _hasMoved = false;
                    temp = nullVector;
                }

                if (temp != nullVector && UserTouch.TouchPhaseEnded(0) && _hasMoved == false)
                {
                    DeployRobotInTheMap(temp);                  
                }
                yield return null;
            }
        }


        /// <summary>
        /// Deploy the robot at the coordonates specified if certain criterias are met
        /// </summary>
        /// <param name="temp"> The position for deploy </param>
        private void DeployRobotInTheMap(Vector3Int temp)
        {
            if (StoreAllTiles.Instance.Tilemap.GetTile(temp) != null)
            {
                StoreAllTiles.Instance.Tilemap.SetTile(temp, null);
                StoreAllTiles.Instance.Tiles[temp.x][temp.y].Health = -1;

                
                GameObject robot = Instantiate(_robot);
                robot.transform.position = new Vector3(temp.x + 0.5f, temp.y + 0.5f, 0f);

                _mining = robot.GetComponent<RobotMining>();
                _button.onClick.RemoveListener(StartDeployOperation);

                check = false;

                _mining.StartMining();


                _button.onClick.RemoveAllListeners();

                _button.transform.DOLocalMoveX(-1, 1f).OnComplete(() => _button.transform.gameObject.SetActive(false));
                _button.image.DOColor(new Color(0,0,0,0), 0.8f);
            }
            
        }
    }
}