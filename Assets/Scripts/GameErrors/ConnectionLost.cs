using System.Collections;
using System.Collections.Generic;
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

    // HERE
    public void ReloadApllication()
    {
        SceneManager.LoadScene(1);
        NetworkManager.Client.ConnectToServer();
        _errorGameObject.SetActive(false);
    }
}