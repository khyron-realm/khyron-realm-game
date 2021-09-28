using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// Used in the BackGround Panel[Bg] to detect the click on it so main panel can be closed
public class ClosePanelUsingBackround : MonoBehaviour, IPointerClickHandler
{
    public Action OnExit;

    // detects click on the curent UI element
    public void OnPointerClick(PointerEventData eventData)
    {
        OnExit?.Invoke();
    }
}