using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockResource : MonoBehaviour
{
    private int _minimumDrop;
    private int _maximumDrop;

    public int MinimumDrop
    {
        set
        {
            _minimumDrop = value;
        }
    }
    public int MaximumDrop
    {
        set
        {
            _maximumDrop = value;
        }
    }

    public void Drop()
    {
        int coef = Random.Range(_minimumDrop, _maximumDrop);
    }
}
