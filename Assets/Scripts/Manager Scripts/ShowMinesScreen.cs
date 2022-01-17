using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Mines;

public class ShowMinesScreen : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        MineManager.OnReceivedMines += Set;
    }

    private void Set()
    {
        if(ManageStatesOfScreen.ScreenToDeployUser)
            _button.onClick.Invoke();
    }

    private void OnDestroy()
    {
        MineManager.OnReceivedMines -= Set;
    }
}