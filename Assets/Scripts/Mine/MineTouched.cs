using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Mine
{
    public class MineTouched : MonoBehaviour
    {
        [SerializeField] private bool _isMineAquired;
        [SerializeField] private GameObject _mineManager;

        public event Action<GameObject, bool, GameObject> OnGameObjectTouched;

        private void OnMouseUpAsButton()
        {
            OnGameObjectTouched?.Invoke(gameObject, _isMineAquired, _mineManager);
        }
    }
}