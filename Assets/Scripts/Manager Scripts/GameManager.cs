using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region "Input data"
    [SerializeField] private int _frameRate;
    #endregion


    void Start()
    {
        Application.targetFrameRate = _frameRate;
    }
}