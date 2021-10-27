using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;
using Networking.Headquarters;
using Networking.Levels;


namespace Manager.Robots
{   
    /// <summary>  
    /// 
    /// Instantiate buttons for each robot
    /// Add listeners such as OnButtonPressed that send the robot pressed
    /// 
    /// </summary>
    public class RobotsManagerUI : MonoBehaviour
    {
        #region "Input data"
        [Header("Panel/Canvas where buttons will be displayed")]
        [SerializeField] private GameObject _canvas;

        [Header("Button Prefab that will be instantiated")]
        [SerializeField] private Button _buttonToInstantiate;

        [SerializeField] private bool _hasPriceDisplayed;
        #endregion

        private List<Button> _buttons;
        public event Action<RobotSO> OnButtonPressed;


        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += PlayerDataReceived;

            _buttons = new List<Button>();            
        }

        private void PlayerDataReceived()
        {
            CreateButtons();
        }


        private void CreateButtons()
        {
            foreach (RobotSO item in RobotsManager.robots)
            {
                Button newButton = Instantiate(_buttonToInstantiate);
                newButton.transform.SetParent(_canvas.transform, false);
                newButton.GetComponent<Image>().sprite = item.Icon;

                if (_hasPriceDisplayed)
                    ShowPrice(item, newButton);

                MakeButtonsAvailable(item, newButton);
                AddListenerToButton(item, newButton);

                _buttons.Add(newButton);
            }
        }
        private static void ShowPrice(RobotSO item, Button newButton)
        {
            int temp = RobotsManager.robotsData[item.NameOfTheRobot.ToString()].RobotLevel;
            newButton.transform.GetChild(0).GetComponent<Text>().text = LevelMethods.RobotBuildCost(HeadquartersManager.Player.Robots[item.RobotId].Level, item.RobotId).ToString();
        }
        private static void MakeButtonsAvailable(RobotSO item, Button newButton)
        {
            if (RobotsManager.robotsData[item.NameOfTheRobot].AvailableRobot == false)
            {
                newButton.interactable = true;
            }
        }


        private void AddListenerToButton(RobotSO robot, Button newButton)
        {
            newButton.onClick.AddListener(
            delegate
            {
                OnButtonPressed?.Invoke(robot);
            });
        }


        public void MakeAllButtonsInactive()
        {
            foreach (Button item in _buttons)
            {
                item.enabled = false;
                item.GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
            }
        }
        public void MakeAllButtonsActive()
        {
            foreach (Button item in _buttons)
            {
                item.enabled = true;
                item.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }


        private void OnDestroy()
        {
            foreach (Button item in _buttons)
            {
                item.onClick.RemoveAllListeners();
            }

            HeadquartersManager.OnPlayerDataReceived -= PlayerDataReceived;
        }
    }
}