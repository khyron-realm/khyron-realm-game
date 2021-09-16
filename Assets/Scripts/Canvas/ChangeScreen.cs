using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentScreen;

    [SerializeField]
    private GameObject _screenToGo;

    public void ScreenChange()
    {
        _currentScreen.SetActive(false);
        _screenToGo.SetActive(true);
    }
}
