using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ClearInputField : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InputField _inputField;


    public void OnPointerClick(PointerEventData eventData)
    {
        _inputField.text = "";
    }
}