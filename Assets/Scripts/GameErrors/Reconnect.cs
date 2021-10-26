using System.Collections;
using System.Collections.Generic;
using DarkRift;
using UnityEngine;
using Networking.Launcher;
using UnityEngine.SceneManagement;


public class Reconnect : MonoBehaviour
{
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
            Debug.Log("Reconnected to the server");
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Cannot reconnect");
        }
    }
}