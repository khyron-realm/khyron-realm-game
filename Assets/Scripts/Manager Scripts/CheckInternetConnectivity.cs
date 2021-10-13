using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class CheckInternetConnectivity : MonoBehaviour
{
    public static event Action OnNoConnection;

    private void FixedUpdate()
    {
        StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected == false)
            {
                OnNoConnection?.Invoke();
            }
        }));
    }

    private IEnumerator checkInternetConnection(Action<bool> action)
    {
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
}