using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InstatiateButtonsForRobotsInMine : MonoBehaviour
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
        print(GetRobotsTrained.RobotsBuilt.Count);
        foreach (Robot item in GetRobotsTrained.RobotsBuilt)
        {
            Button newButton = Instantiate(_buttonToInstantiate);
            newButton.transform.SetParent(_canvas.transform, false);
            newButton.GetComponent<Image>().sprite = item.icon;

            AddListenerToButton(item, newButton);

            _buttons.Add(newButton);
        }
    }

    private void AddListenerToButton(Robot item, Button newButton)
    {
        newButton.onClick.AddListener(
        delegate
        {
            AddListenerRobotToEachButton(item);
            TouchedListener(newButton);
        });
    }
    private void AddListenerRobotToEachButton(Robot robot)
    {
        OnButtonPressed?.Invoke(robot);
    }

    private void TouchedListener(Button newButton)
    {
        newButton.transform.DOScale(1.12f, 0.2f);
        foreach (Button item in _buttons)
        {
            if(newButton != item)
            {
                item.transform.DOScale(1, 0.2f);
            }
        }
    }
}   