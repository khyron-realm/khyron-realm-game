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
        NetworkManager.Client.OnDisconnected += ConnectionIsLost;
        NetworkManager.OnServerNotAvailable += ConnectionIsLost; 
    }

    // Connection lost methods
    private void ConnectionIsLost(object sender, DisconnectedEventArgs e)
    {
        SceneManager.UnloadSceneAsync(GetCurrentScene());
        SceneManager.LoadSceneAsync((int)ScenesName.RECONNECT_SCENE, LoadSceneMode.Additive);
    }
    private void ConnectionIsLost()
    {
        SceneManager.UnloadSceneAsync(GetCurrentScene());
        SceneManager.LoadSceneAsync((int)ScenesName.RECONNECT_SCENE, LoadSceneMode.Additive);
    }

    private int GetCurrentScene()
    {
        int countLoaded = SceneManager.sceneCount;

        for (int i = 0; i < countLoaded; i++)
        {
            if(SceneManager.GetSceneAt(i).buildIndex != 0)
            {
                return SceneManager.GetSceneAt(i).buildIndex;
            }
        }

        return 0;
    }
}