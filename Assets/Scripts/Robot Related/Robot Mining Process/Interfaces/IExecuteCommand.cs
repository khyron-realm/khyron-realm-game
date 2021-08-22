using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExecuteCommand<T>
{
    public void ExecuteCommand(List<T> _allHits);
    public GameObject CommandBlock { set; }
}
