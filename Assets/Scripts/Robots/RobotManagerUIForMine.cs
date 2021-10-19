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
        public static List<Button> Buttons;

        private List<RobotSO> _workers;
        private List<RobotSO> _crushers;
        private List<RobotSO> _probes; 
        #endregion

        public static event Action<List<RobotSO>, Text> OnButtonPressed;

        private void Awake()
        {
            DeployRobot.ZPosition = 0;

            _workers = new List<RobotSO>();
            _crushers = new List<RobotSO>();
            _probes = new List<RobotSO>();

            Buttons = new List<Button>();
            SeparateRobotsInCategories();
        }


        /// <summary>
        /// Separate Robots in theor category
        /// </summary>
        private void SeparateRobotsInCategories()
        {
            foreach(RobotSO item in GetRobotsTrained.RobotsBuilt)
            {
                if(item._robotId == 0)
                {
                    _workers.Add(item);
                }
                if(item._robotId == 1)
                {
                    _crushers.Add(item);
                }
                if (item._robotId == 2)
                {
                    _probes.Add(item);
                }
            }

            if(_workers.Count > 0)
            {
                CreateButton(_workers);
            }
            
            if(_crushers.Count > 0)
            {
                CreateButton(_crushers);
            }
            
            if(_probes.Count > 0)
            {
                CreateButton(_probes);
            }
        }


        /// <summary>
        /// Creates button for each robot to build
        /// </summary>
        /// <param name="list"></param>
        private void CreateButton(List<RobotSO> list)
        {
            Button newButton = Instantiate(_buttonToInstantiate);
            newButton.transform.SetParent(_canvas.transform, false);
            newButton.GetComponent<Image>().sprite = list[0].icon;
            newButton.transform.GetChild(0).GetComponent<Text>().text = list.Count.ToString();

            AddListenerToButton(newButton, list, newButton.transform.GetChild(0).GetComponent<Text>());

            Buttons.Add(newButton);
        }


        /// <summary>
        /// Add listeners to button so 
        /// </summary>
        /// <param name="item"> The robot allocated for the button </param>
        /// <param name="newButton"> The button created </param>
        private void AddListenerToButton(Button newButton, List<RobotSO> list, Text text)
        {
            newButton.onClick.AddListener(
            delegate
            {
                AddListenerRobotToEachButton(list, text);
                TouchedListener(newButton);
            });
        }


        /// <summary>
        /// Method that is invoked when the button of the robot is pressed and send the robot as parameter for further use
        /// </summary>
        /// <param name="robot"> The robot </param>
        private void AddListenerRobotToEachButton(List<RobotSO> list, Text text)
        {
            OnButtonPressed?.Invoke(list, text);
        }


        /// <summary>
        /// Scales the current button so it is vissible and scale down the others
        /// </summary>
        /// <param name="newButton"> The current button </param>
        private void TouchedListener(Button newButton)
        {
            newButton.transform.DOScale(1.12f, 0.18f);
            foreach (Button item in Buttons)
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