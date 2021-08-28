using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TilesData;

/// <summary>
/// 
/// Deploy robot 
/// 
/// User press the button --> select a tile in the safe area --> that tile gets destroyed --> robot is placed and activated 
/// 
/// </summary>

namespace RobotDeployActions
{
    public class DeployRobot : MonoBehaviour
    {
        // Robot to deploy
        private GameObject _robot;

        // Button asociated with the robot
        private Button _button;

        public GameObject Robot
        {
            set
            {
                _robot = value;
            }
        }

        public event Action OnDeployed;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(StartDeployOperation);
        }

        public void DeselectRobot()
        {
            StopCoroutine("Deploy");
        }

        private void StartDeployOperation()
        {
            StartCoroutine("Deploy");
        }

        private IEnumerator Deploy()
        {
            bool check = true;

            while (check)
            {
                Vector3Int temp = UserTouch.TouchPositionInt(0, UserTouch.touchArea);
                Vector3Int nullVector = new Vector3Int(-99999, -99999, -99999);

                if (temp != nullVector && UserTouch.TouchPhaseEnded(0))
                {
                    DeployRobotInTheMap(temp);

                    check = false;
                }
                yield return null;
            }
        }

        private void DeployRobotInTheMap(Vector3Int temp)
        {
            StoreAllTiles.instance.Tilemap.SetTile(temp, null);
            StoreAllTiles.instance.tiles[temp.x][temp.y].Health = -1;

            _robot.SetActive(true);
            _robot.GetComponent<RobotManager>().commandBlock.SetActive(true);

            _robot.GetComponent<RobotManager>().commandBlock.transform.position = new Vector3(temp.x + 0.5f, temp.y + 0.5f, -6f);
            _robot.transform.position = new Vector3(temp.x + 0.5f, temp.y + 0.5f, 0f);

            _button.onClick.RemoveListener(StartDeployOperation);

            OnDeployed?.Invoke();
        }
    }
}