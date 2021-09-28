using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEditor.PackageManager;

namespace Networking
{
    public class LoginManager : MonoBehaviour
    {
        public static bool IsLoggedIn { get; private set; }
        
        private void Awake()
        {
            GameControl.Client.MessageReceived += OnDataHandler;
        }

        private void OnDestroy()
        {
            if (GameControl.Client == null)
            {
                return;
            }

            GameControl.Client.MessageReceived -= OnDataHandler;
        }

        private static void OnDataHandler(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                switch (message.Tag)
                {
                    case Tags.LoginSuccess:
                    {
                        IsLoggedIn = true;
                        break;
                    }
                    
                    case Tags.LoginFailed:
                    {
                        using (var reader = message.GetReader())
                        {
                            if (reader.Length != 1)
                            {
                                Debug.LogWarning("Invalid LoginFailed error data received");
                            }
                        }
                        break;
                    }
                    
                    case Tags.AddPlayerSuccess:
                    {
                        break;
                    }

                    case Tags.AddPlayedFailed:
                    {
                        using (var reader = message.GetReader())
                        {
                            if (reader.Length != 1)
                            {
                                Debug.LogWarning("Invalid LoginFailed error data received");
                            }
                        }
                        break;
                    }
                }
            }
        }

        public static void Login(string username, string password)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(username);
                writer.Write(Rsa.Encrypt(Encoding.UTF8.GetBytes(password)));
             
                using (var msg = Message.Create(Tags.LoginPlayer, writer))
                {
                    GameControl.Client.SendMessage(msg, SendMode.Reliable);
                }
            }
        }

        public static void Logout()
        {
            IsLoggedIn = false;
            
            using (var msg = Message.CreateEmpty(Tags.LogoutPlayer))
            {
                GameControl.Client.SendMessage(msg, SendMode.Reliable);
            }
        }
    }
}