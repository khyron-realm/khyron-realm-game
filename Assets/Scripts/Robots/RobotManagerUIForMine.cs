using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace Manager.Robots
{
    public class RobotManagerUIForMine : MonoBehaviour
    {
        #region "Input data"
        [Header("Panel/Canvas where buttons will be displayed")]
        [SerializeField] private GameObject _canvas;

        [Header("Button Prefab that will be instantiated")]
        [SerializeField] private Button _buttonToInstantiate;
        #endregion

        #region "Private members"
        private List<Button> _buttons;
        #endregion

        public static event Action<Robot> OnButtonPressed;

        private void Awake()
        {
            _buttons = new List<Button>();
            CreateButtons();
        }


        /// <summary>
        /// Creates the buttons for each robot built
        /// </summary>
        private void CreateButtons()
        {
            foreach (Robot item in GetRobotsTrained.RobotsBuilt)
            {
                Button newButton = Instantiate(_buttonToInstantiate);
                newButton.transform.SetParent(_canvas.transform, false);
                newButton.GetComponent<Image>().sprite = item.icon;

                AddListenerToButton(item, newButton);

                _buttons.Add(newButton);
            }
        }


        /// <summary>
        /// Add listeners to button so 
        /// </summary>
        /// <param name="item"> The robot allocated for the button </param>
        /// <param name="newButton"> The button created </param>
        private void AddListenerToButton(Robot item, Button newButton)
        {
            newButton.onClick.AddListener(
            delegate
            {
                AddListenerRobotToEachButton(item);
                TouchedListener(newButton);
            });
        }


        /// <summary>
        /// Method that is invoked when the button of the robot is pressed and send the robot as parameter for further use
        /// </summary>
        /// <param name="robot"> The robot </param>
        private void AddListenerRobotToEachButton(Robot robot)
        {
            OnButtonPressed?.Invoke(robot);
        }


        /// <summary>
        /// Scales the current button so it is vissible and scale down the others
        /// </summary>
        /// <param name="newButton"> The current button </param>
        private void TouchedListener(Button newButton)
        {
            newButton.transform.DOScale(1.12f, 0.18f);
            foreach (Button item in _buttons)
            {
                if (newButton != item)
                {
                    item.transform.DOScale(1, 0.18f);
                    item.GetComponent<DeployRobot>().DeselectRobot();
                }
            }
        }
    }
}