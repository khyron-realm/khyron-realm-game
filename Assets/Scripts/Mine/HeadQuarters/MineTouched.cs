using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Mine
{
    public class MineTouched : MonoBehaviour, IOpen
    {
        public event Action<GameObject, bool, bool> OnGameObjectTouched;
        public bool IsMine = false;
        public bool IsAuction = false;

        public void Open()
        {
            OnGameObjectTouched?.Invoke(gameObject, IsMine, IsAuction);
        }
    }
}