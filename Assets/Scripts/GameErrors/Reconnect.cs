using System;
using System.Collections.Generic;
using System.Collections;
using DarkRift;
using UnityEngine;
using Networking.Launcher;
using UnityEngine.SceneManagement;


public class Reconnect : MonoBehaviour
{
    private List<AsyncOperation> _operations;

    public static event Action<List<AsyncOperation>, bool> OnSceneChanged;

    private void Awake()
    {
        _operations = new List<AsyncOperation>();
    }

    public void ReloadApllication()
    {
        NetworkManager.Client.ConnectToServer();
        StartCoroutine("CheckForAcceptance");
    }

    private IEnumerator CheckForAcceptance()
    {
        yield return new WaitForSeconds(2f);

        if (NetworkManager.Client.ConnectionState == ConnectionState.Connected)
        {
            _operations.Clear();
            _operations.Add(SceneManager.UnloadSceneAsync((int)ScenesName.RECONNECT_SCENE));
            _operations.Add(SceneManager.LoadSceneAsync((int)ScenesName.HEADQUARTERS_SCENE, LoadSceneMode.Additive));
            OnSceneChanged?.Invoke(_operations, true);
        }
        else
        {
            Debug.Log("Cannot reconnect");
        }
    }
}
