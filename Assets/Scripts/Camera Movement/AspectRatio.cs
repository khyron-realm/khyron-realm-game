using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    public float temp;

    void Start()
    {
        GetComponent<Camera>().aspect = temp;
    }
}
