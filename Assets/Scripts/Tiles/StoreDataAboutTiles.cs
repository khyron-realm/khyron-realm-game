using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StoreDataAboutTiles
{
    private int _health;
    private MineResources _resource = null;
    //private bool _deployable = false;
    //private bool _beaconDeployed = false;

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