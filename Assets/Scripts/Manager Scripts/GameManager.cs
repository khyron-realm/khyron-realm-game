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

    private static bool s_once = true;

    void Start()
    {
        Application.targetFrameRate = _frameRate;
    }

    private void OnApplicationPause(bool pause)
    {
        if(!pause)
        {
            if(s_once)
            {
                s_once = false;
            }
            else
            {
                SceneManager.UnloadSceneAsync(1);
                SceneManager.LoadSceneAsync((int)ScenesName.HEADQUARTERS_SCENE);
            }
        }
    }
}