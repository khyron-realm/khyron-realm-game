using System.Collections;
using System.Collections.Generic;
using DarkRift;
using UnityEngine;
using Networking.Launcher;
using UnityEngine.SceneManagement;
using DarkRift.Client;


public class ConnectionLost : MonoBehaviour
{
    private void Awake()
    {
        NetworkManager.Client.Disconnected += ConnectionIsLost;
        NetworkManager.OnServerNotAvailable += ConnectionIsLost; 
    }

    // Connection lost methods
    private void ConnectionIsLost(object sender, DisconnectedEventArgs e)
    {
        SceneManager.LoadScene(5);
    }
    private void ConnectionIsLost()
    {
        SceneManager.LoadScene(5);
    }
}