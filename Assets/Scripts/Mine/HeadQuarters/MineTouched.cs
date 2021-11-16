using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Mine
{
    public class MineTouched : MonoBehaviour, IOpen
    {
        [SerializeField] private AudioSource _clip;
        [SerializeField] private GameObject _mountain;

        public event Action<GameObject, bool, bool> OnGameObjectTouched;
        public event Action<byte> OnMineSelected;

        public bool HasMine;
        public byte index;

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
                OnMineSelected?.Invoke(index);

            if(_clip != null)
                _clip.Play();
        }
    }
}