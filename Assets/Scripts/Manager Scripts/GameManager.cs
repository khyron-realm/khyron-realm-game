using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Networking.Headquarters;

public class GameManager : MonoBehaviour
{
    #region "Input data"
    [SerializeField] private int _frameRate;
    #endregion


    void Start()
    {
        Application.targetFrameRate = _frameRate;
    }


    private void OnApplicationPause(bool pause)
    {
        if(pause) // Users put aplication in background
        {
            Application.Quit();
        }
    }
}