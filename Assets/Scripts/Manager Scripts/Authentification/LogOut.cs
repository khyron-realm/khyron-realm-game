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
        
        public void  Disconnect()
        {
            LoginManager.Logout();
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
    }
}
