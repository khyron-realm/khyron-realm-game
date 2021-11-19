using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sounds;

namespace Mine
{
    public class MineTouched : MonoBehaviour, IOpen
    {
        [Header("Index of the sound in the manager")]
        [SerializeField] private byte _soundIndex;
        [SerializeField] private GameObject _mountain;

        public event Action<GameObject, bool, bool> OnGameObjectTouched;
        public event Action<byte> OnMineSelected;
     
        // Index
        public byte IndexPosition;

        // Bools
        public bool HasMine;
        public bool IsAuction = false;

        private void Start()
        {
            if(HasMine)
            {
                _mountain.SetActive(true);
            }
            else
            {
                if(_mountain != null)
                    _mountain.SetActive(false);
            }        
        }

        public void Open()
        {
            OnGameObjectTouched?.Invoke(gameObject, HasMine, IsAuction);

            if(HasMine)
                OnMineSelected?.Invoke(IndexPosition);

            SoundManager.MakeSound(_soundIndex);
        }
    }
}