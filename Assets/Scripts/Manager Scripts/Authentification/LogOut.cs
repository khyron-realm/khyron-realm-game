using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Login;
using Scenes;

namespace Authentification
{
    public class LogOut : MonoBehaviour
    {
        [SerializeField] private ChangeScene _scene;

        private void Awake()
        {
            LoginManager.OnSuccessfulLogout += SuccessfulLogout;
        }

        public void  Disconnect()
        {
            LoginManager.Logout();          
        }

        private void SuccessfulLogout(byte logoutType)
        {
            Delete();
            _scene.GoToScene();
        }

        private void Delete()
        {
            try
            {
                File.Delete(Application.persistentDataPath + "/playerCredentials.data");
            }
            catch
            {}
        }


        private void OnDestroy()
        {
            LoginManager.OnSuccessfulLogout -= SuccessfulLogout;
        }
    }
}
