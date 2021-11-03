using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Mine
{
    public class MineTouched : MonoBehaviour, IOpen
    {
        [SerializeField] private GameObject _mineManager;

        public event Action<GameObject, GameObject> OnGameObjectTouched;

        public void Open()
        {
            OnGameObjectTouched?.Invoke(gameObject, _mineManager);
        }
    }
}