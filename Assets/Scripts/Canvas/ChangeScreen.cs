using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeScreen : MonoBehaviour, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        ScreenChange();
    }

}
