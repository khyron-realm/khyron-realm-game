using System;
using System.Collections.Generic;
using System.Collections;
using DarkRift;
using UnityEngine;
using Networking.Launcher;
using UnityEngine.SceneManagement;
using Authentification;
using AuxiliaryClasses;
using TMPro;

public class Reconnect : MonoBehaviour
{
    [SerializeField] private GameObject _wheel;
    [SerializeField] private TextMeshProUGUI _text;

    private List<AsyncOperation> _operations;

    public static event Action<List<AsyncOperation>, bool> OnSceneChanged;

    private void Awake()
    {
        _text.text = "";

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
        Animations.Recconect(_wheel);

        yield return new WaitForSeconds(2f);

        if (NetworkManager.Client.ConnectionState == ConnectionState.Connected)
        {
            _operations.Clear();
            AutomaticLogIn.ConnectionEstablished();
            _text.text = "";
        }
        else
        {
            _text.text = "Cannot Recconect. Try Again";
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