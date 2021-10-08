using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Tiledata
{
    public class DataOfTile
    {
        private int _health;
        private MineResources _resource = null;

        // If block is Mined
        public static event Action<MineResources> OnMinedBlock;
        private bool once = false; // for safety

        // constructor with health
        public DataOfTile(int _health)
        {
            this._health = _health;
        }

        // constructor with health and resources 
        public DataOfTile(int _health, MineResources _resource)
        {
            this._health = _health;
            this._resource = _resource;
        }

        #region "Public getters and setters"
        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                _health = value;

                if (_health < 1 && once == false && _resource != null)
                {
                    OnMinedBlock?.Invoke(_resource);
                    once = true;
                }
            }
        }
        public MineResources Resource
        {
            get
            {
                return _resource;
            }
            set
            {
                _resource = value;
            }
        }
        #endregion
    }
}