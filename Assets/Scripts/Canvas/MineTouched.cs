using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Mine
{
    public class MineTouched : MonoBehaviour, IPointerClickHandler
    {
        public event Action<GameObject> OnGameObjectTouched;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnGameObjectTouched?.Invoke(gameObject);
        }
    }
}