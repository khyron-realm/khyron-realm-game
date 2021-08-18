using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeleteCommand<T> 
{
    public void DeleteCommand(List<T> _allHits);
    public GameObject CommandBlock { set; }
}
