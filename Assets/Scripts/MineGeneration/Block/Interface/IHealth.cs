using System;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    public int Health{ get;}
    public int InitialHealth { get; set;}

    public event Action OnHealthZero;
    public bool DoDamage(int damage);
}
