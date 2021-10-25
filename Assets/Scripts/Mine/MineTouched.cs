using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Mine
{
    public class MineTouched : MonoBehaviour, IOpen
    {
        [SerializeField] private bool _isMineAquired;
        [SerializeField] private GameObject _mineManager;

        public event Action<GameObject, bool, GameObject> OnGameObjectTouched;

        public void Open()
        {
            OnGameObjectTouched?.Invoke(gameObject, _isMineAquired, _mineManager);
        }
    }
}