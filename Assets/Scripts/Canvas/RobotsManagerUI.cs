using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers.Robots;


// Takes care of creating the buttons and assigning Robots to them
public class RobotsManagerUI : MonoBehaviour
{
    [Header("Panel/Canvas where buttons will be displayed")]
    [SerializeField] private GameObject _canvas;

    [Header("Button Prefab that will be instantiated")]
    [SerializeField] private Button _buttonToInstantiate;

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

            MakeButtonsAvailable(item, newButton);
            AddListenerToButton(item, newButton);
            
            _buttons.Add(newButton);
        }
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
}