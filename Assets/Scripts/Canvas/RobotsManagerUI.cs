using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Robots;
using Manager.Upgrade;


// Takes care of creating the buttons and assigning Robots to them
public class RobotsManagerUI : MonoBehaviour
{
    [Header("Panel/Canvas where buttons will be displayed")]
    [SerializeField] private GameObject _canvas;

    [Header("Button Prefab that will be instantiated")]
    [SerializeField] private Button _buttonToInstantiate;

    [SerializeField] private bool _hasPriceDisplayed;

    private List<Button> _buttons;
    public event Action<Robot> OnButtonPressed;


    private void Awake()
    {
        _buttons = new List<Button>();     
        CreateButtons();
    }


    private void CreateButtons()
    {
        foreach (Robot item in RobotsManager.robots)
        {
            Button newButton = Instantiate(_buttonToInstantiate);
            newButton.transform.SetParent(_canvas.transform, false);
            newButton.GetComponent<Image>().sprite = item.icon;
            
            if(_hasPriceDisplayed)
                ShowPrice(item, newButton);

            MakeButtonsAvailable(item, newButton);
            AddListenerToButton(item, newButton);

            _buttons.Add(newButton);
        }
    }
    private static void ShowPrice(Robot item, Button newButton)
    {
        int temp = RobotsManager.robotsData[item.nameOfTheRobot.ToString()].robotLevel;
        newButton.transform.GetChild(0).GetComponent<Text>().text = item.robotLevel[temp].priceToBuild.energy.ToString();
    }
    private static void MakeButtonsAvailable(Robot item, Button newButton)
    {
        if (RobotsManager.robotsData[item.nameOfTheRobot].availableRobot == false)
        {
            newButton.interactable = true;
        }
    }


    private void AddListenerToButton(Robot item, Button newButton)
    {
        newButton.onClick.AddListener(
        delegate
        {
            AddListenerRobotToEachButton(item);
        });
    }
    private void AddListenerRobotToEachButton(Robot robot)
    {
        OnButtonPressed?.Invoke(robot);
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
    }
}