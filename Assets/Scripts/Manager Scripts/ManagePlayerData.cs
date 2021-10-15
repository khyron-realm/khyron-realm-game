using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Game;
using Authentification;
using Scenes;


public class ManagePlayerData : MonoBehaviour
{
    #region "Input data"
    [SerializeField] private LogIn _login;
    [SerializeField] private ChangeScene _scene;
    #endregion

    private void Awake()
    {
        UnlimitedPlayerManager.OnPlayerDataReceived += PlayerDataReceived;
        UnlimitedPlayerManager.OnPlayerDataUnavailable += PlayerDataUnavailable;
        _login.OnCredentialsAreGood += GetPlayerData;
    }


    public void GetPlayerData()
    {
        UnlimitedPlayerManager.PlayerDataRequest();
    }


    private void PlayerDataReceived()
    {
        Debug.Log("Player data received");
        _scene.GoToScene();
    }


    private void PlayerDataUnavailable()
    {
        Debug.Log("Player data unavailable");
    }


    private void OnDestroy()
    {
        UnlimitedPlayerManager.OnPlayerDataReceived -= PlayerDataReceived;
        UnlimitedPlayerManager.OnPlayerDataUnavailable -= PlayerDataUnavailable;
        _login.OnCredentialsAreGood -= GetPlayerData;
    }
}