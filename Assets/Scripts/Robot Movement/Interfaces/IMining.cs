using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMining
{
    public void Mine(List<List<Collider2D>> hits, int damage);

    public event Action OnMining;
    public event Action OnFinishedMining;
}