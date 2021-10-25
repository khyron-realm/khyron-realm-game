using System.Collections;
using System.Collections.Generic;
using DarkRift;
using UnityEngine;
using Networking.Launcher;
using UnityEngine.SceneManagement;
using DarkRift.Client;


public class ConnectionLost : MonoBehaviour
{
    [SerializeField] private GameObject _errorGameObject;

    private void Awake()
    {
        NetworkManager.Client.Disconnected += ConnectionIsLost;
        NetworkManager.OnServerNotAvailable += ConnectionIsLost;

        _errorGameObject.SetActive(false);      
    }

    // Connection lost methods
    private void ConnectionIsLost(object sender, DisconnectedEventArgs e)
    {
        _errorGameObject.SetActive(true);
    }
    private void ConnectionIsLost()
    {
        _errorGameObject.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ReloadApllication()
    {
        NetworkManager.Client.ConnectToServer();
        if (NetworkManager.Client.ConnectionState == ConnectionState.Connected)
        {
            Debug.Log("Reconnected to the server");
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Cannot reconnect");
        }
        
        _errorGameObject.SetActive(false);
    }
}