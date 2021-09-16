using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _frameRate;

    void Start()
    {
        Application.targetFrameRate = _frameRate;
    }
}
