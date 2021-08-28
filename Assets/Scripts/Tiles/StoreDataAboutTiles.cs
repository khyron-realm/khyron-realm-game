using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TilesData
{
    public class StoreDataAboutTiles
    {
        private int _health;
        private MineResources _resource = null;
        //private bool _deployable = false;
        //private bool _beaconDeployed = false;

        // If block is Mined
        public static event Action<MineResources> OnMinedBlock;
        private bool once = false; // for safety

        public StoreDataAboutTiles(int _health)
        {
            this._health = _health;
        }

        public StoreDataAboutTiles(int _health, MineResources _resource)
        {
            this._health = _health;
            this._resource = _resource;
        }

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
    }
}