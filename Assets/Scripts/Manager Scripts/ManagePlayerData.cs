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
            HeadquartersManager.PlayerDataRequest();
        }

        private void PlayerDataUnavailable()
        {
            Debug.Log("----> Player data unavailable <----");
            // Native Erorrs
        }


        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataUnavailable -= PlayerDataUnavailable;
        }
    }
}