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
        private int _discovered = 0;

        private byte _type;

        private RuleTile _standardBlock;
        private RuleTile _resourceTile = null;

        private MineResources _resource = null;

        // If block is Mined
        public static event Action<MineResources> OnMinedBlock;
        private bool once = false; // for safety

        // constructor with health
        public DataOfTile(int _health, RuleTile _standardBlock, byte _type)
        {
            this._health = _health;
            this._standardBlock = _standardBlock;
            this._type = _type;
        }

        // constructor with health and resources 
        public DataOfTile(int _health, RuleTile _standardBlock, RuleTile _resourceTile, MineResources _resource, byte _type)
        {          
            this._health = _health;
            this._standardBlock = _standardBlock;
            this._resourceTile = _resourceTile;
            this._resource = _resource;
            this._type = _type;
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

        public byte Type
        {
            get
            {
                return _type;
            }
        }

        public RuleTile StandardBlock
        {
            get
            {
                return _standardBlock;
            }
            set
            {
                _standardBlock = value;
            }
        }
        public RuleTile ResourceTile
        {
            get
            {
                return _resourceTile;
            }
            set
            {
                _resourceTile = value;
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

        public int Discovered
        {
            get
            {
                return _discovered;
            }
            set
            {
                _discovered = value;
                if(_discovered < 0)
                {
                    _discovered = 0;
                }
            }
        }
        #endregion
    }
}