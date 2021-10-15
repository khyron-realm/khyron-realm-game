using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Game;

public class GameManager : MonoBehaviour
{
    #region "Input data"
    [SerializeField] private int _frameRate;
    #endregion

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