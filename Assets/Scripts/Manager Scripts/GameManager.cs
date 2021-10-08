using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _frameRate;

    private static GameManager s_instance = null;

    void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        Application.targetFrameRate = _frameRate;
    }
}