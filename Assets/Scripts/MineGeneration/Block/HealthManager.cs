using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour, IHealth
{
    [SerializeField]
    private int _initialHealth;

    private int _health;
    public event Action OnHealthZero;

    public int Health
    {
       get
       {
            return _health;
       }
    }

    public int InitialHealth 
    {
        get
        {
            return _initialHealth;
        }
        set
        {
            _initialHealth = value;
        }
    }

    private void Start()
    {
        _health = _initialHealth;
    }

    public bool DoDamage(int damage)
    {
        _health -= damage;

        if(_health > _initialHealth)
        {
            _health = _initialHealth;
        }

        if(_health < 1)
        {
            _health = 0;
            GetComponent<MeshFilter>().mesh = null;
            OnHealthZero?.Invoke();
            return true;
        }

        return false;
    }
}