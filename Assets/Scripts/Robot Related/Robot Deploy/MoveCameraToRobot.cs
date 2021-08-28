using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RobotDeployActions
{
    public class MoveCameraToRobot : MonoBehaviour
    {
        private List<Camera> _cameras;
        private GameObject _robot;
        private Button _button;

        public GameObject Robot
        {
            set
            {
                _robot = value;
            }
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            GetComponent<DeployRobot>().OnDeployed += AddMoveCamera;

            _cameras = new List<Camera>();
            _cameras.Add(Camera.main);

            Transform[] allChildren = Camera.main.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                _cameras.Add(child.GetComponent<Camera>());
            }
        }

        private void AddMoveCamera()
        {
            _button.onClick.AddListener(MoveCamera);
        }

        private void MoveCamera()
        {
            ManageButtonsTouched.DisableAllFocusAnimations(gameObject);
            StartCoroutine("CameraMovementAnimation");
        }

        public void StopMoveCamera()
        {
            StopCoroutine("CameraMovementAnimation");
        }

        private IEnumerator CameraMovementAnimation()
        {
            float temp = 0f;

            Vector3 initialPosition = Camera.main.transform.position;
            Vector3 finalPosition = new Vector3(_robot.transform.position.x, _robot.transform.position.y, -10f);

            while (temp < 1f)
            {
                temp += Time.deltaTime * 1.6f;

                foreach (Camera item in _cameras)
                {
                    item.transform.position = Vector3.Lerp(initialPosition, finalPosition, Mathf.SmoothStep(0f, 1f, temp));
                }

                yield return null;
            }
        }
    }
}