using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// Used in the BackGround Panel[Bg] to detect the click on it so main panel can be closed
public class ClosePanel : MonoBehaviour, IPointerClickHandler
{
    public static Action OnExit;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnExit?.Invoke();
    }
}