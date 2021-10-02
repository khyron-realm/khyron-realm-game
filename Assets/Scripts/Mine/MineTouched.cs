using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Mine
{
    public class MineTouched : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool _isMineAquired;

        public event Action<GameObject, bool> OnGameObjectTouched;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnGameObjectTouched?.Invoke(gameObject, _isMineAquired);  
        }
    }
}