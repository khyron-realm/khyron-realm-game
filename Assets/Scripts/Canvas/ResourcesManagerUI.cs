using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manager.Resources
{
    public class ResourcesManagerUI : MonoBehaviour
    {
        [Header("Panel/Canvas where buttons will be displayed")]
        [SerializeField] private GameObject _canvas;

        [Header("Button Prefab that will be instantiated")]
        [SerializeField] private Button _buttonToInstantiate;

        private List<Button> _buttons;
        public event Action<GameResources> OnButtonPressed;


        private void Awake()
        {
            _buttons = new List<Button>();
            CreateButtons();
        }


        private void CreateButtons()
        {
            foreach(GameResources item in ResourceManager.resources)
            {
                Button newButton = Instantiate(_buttonToInstantiate);
                newButton.transform.SetParent(_canvas.transform, false);
                newButton.GetComponent<Image>().sprite = item.icon;

                AddListenerToButton(item, newButton);

                _buttons.Add(newButton);
            }
        }


        private void AddListenerToButton(GameResources item, Button newButton)
        {
            newButton.onClick.AddListener(
            delegate
            {
                AddListenerRobotToEachButton(item);
            });
        }


        private void AddListenerRobotToEachButton(GameResources resource)
        {
            OnButtonPressed?.Invoke(resource);
        }
    }
}