using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotDeployActions
{
    public class CreateButtonForRobot : MonoBehaviour
    {
        [SerializeField]
        private GameObject _canvas;

        public static GameObject canvas;

        [SerializeField]
        private GameObject _buttonPrefab;

        public static GameObject buttonPrefab;

        public static List<GameObject> buttons;

        private void Awake()
        {
            canvas = _canvas;
            buttonPrefab = _buttonPrefab;

            buttons = new List<GameObject>();
        }

        public static void CreateButton(GameObject robot)
        {
            GameObject newButton = Instantiate(buttonPrefab);

            newButton.GetComponent<DeployRobot>().Robot = robot;
            newButton.GetComponent<ManagePreviewOfPath>().Robot = robot;
            newButton.GetComponent<MoveCameraToRobot>().Robot = robot;

            newButton.transform.SetParent(canvas.transform, false);

            buttons.Add(newButton);
        }
    }

}