using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RobotButtonInteractions
{
    public class RobotState : MonoBehaviour
    {
        private GameObject _robot;
        public GameObject Robot
        {
            set
            {
                _robot = value;
            }
        }

        [SerializeField]
        private Sprite _stateNotDeployed;

        [SerializeField]
        private Sprite _stateIdle;

        [SerializeField]
        private Sprite _stateMining;

        private Image _image;

        private void Start()
        {
            _image = transform.GetChild(0).GetComponent<Image>();

            GetComponent<DeployRobot>().OnDeployed += StateDeployed;
            _robot.GetComponent<RobotManager>().OnCommandGiven += StateMining;
            _robot.GetComponent<IMining<Vector3>>().OnFinishedMining += StateIdle;
            _image.sprite = _stateNotDeployed;
        }

        private void StateDeployed()
        {
            StateIdle();
            GetComponent<DeployRobot>().OnDeployed -= StateDeployed;
        }

        private void StateIdle()
        {
            _image.sprite = _stateIdle;
        }

        private void StateMining()
        {
            _image.sprite = _stateMining;
        }
    }
}