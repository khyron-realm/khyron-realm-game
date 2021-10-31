using System;
using System.Collections.Generic;
using System.Collections;
using DarkRift;
using UnityEngine;
using Networking.Launcher;
using UnityEngine.SceneManagement;
using Authentification;


public class Reconnect : MonoBehaviour
{
    private List<AsyncOperation> _operations;

    public static event Action<List<AsyncOperation>, bool> OnSceneChanged;

    private void Awake()
    {
        _operations = new List<AsyncOperation>();

        AutomaticLogIn.OnAutomaticLoginAccepted += LoadNextScene;
        AutomaticLogIn.OnAutomaticLoginFailed += LoadNextScene;
    }


    public void ReloadApllication()
    {
        NetworkManager.Client.ConnectToServer();
        StartCoroutine(CheckForAcceptance());
    }


    private IEnumerator CheckForAcceptance()
    {
        yield return new WaitForSeconds(2f);

        if (NetworkManager.Client.ConnectionState == ConnectionState.Connected)
        {
            _operations.Clear();
            AutomaticLogIn.ConnectionEstablished();   
        }
        else
        {
            Debug.Log("Cannot reconnect");
        }
    }


    private void LoadNextScene(AsyncOperation temp1, bool temp2)
    {
        _operations.Add(SceneManager.UnloadSceneAsync((int)ScenesName.RECONNECT_SCENE));
        OnSceneChanged?.Invoke(_operations, true);
    }
    private void LoadNextScene()
    {
        _operations.Add(SceneManager.UnloadSceneAsync((int)ScenesName.RECONNECT_SCENE));
        OnSceneChanged?.Invoke(_operations, true);
    }
}