using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Authentification;
using Networking.Headquarters;
using Scenes;


namespace Manager
{
    public class ManagePlayerData : MonoBehaviour
    {
        private void Awake()
        {
            HeadquartersManager.OnPlayerDataUnavailable += PlayerDataUnavailable;
        }

        private void PlayerDataUnavailable()
        {
            Debug.Log("----> Player data unavailable <----");
            // Native Errors
        }


        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataUnavailable -= PlayerDataUnavailable;
        }
    }
}