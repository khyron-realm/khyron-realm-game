using System;
using System.Collections.Generic;
using UnityEngine;

public interface IMining<T>
{
    public void Mine(List<List<T>> hits, int damage);

    public event Action OnMining;
    public event Action OnFinishedMining;
}