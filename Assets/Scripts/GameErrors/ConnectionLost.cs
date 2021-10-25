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
        _errorGameObject.SetActive(false);      
    }

    private void ConnectionIsLost(object sender, DisconnectedEventArgs e)
    {
        _errorGameObject.SetActive(true);
    }

    public void ReloadApllication()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
}