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
        public event Action<byte> OnMineSelected;

        public bool HasMine;
        public byte index;

        public bool IsAuction = false;

        public void Open()
        {
            OnGameObjectTouched?.Invoke(gameObject, HasMine, IsAuction);
            OnMineSelected?.Invoke(index);
        }
    }
}