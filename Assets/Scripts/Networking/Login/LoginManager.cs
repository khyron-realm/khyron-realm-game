using System.Text;
using DarkRift;
using DarkRift.Client;
using Networking.Game;
using Networking.Tags;
using UnityEngine;

namespace Networking.Login
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
            using var message = e.GetMessage();
            
            // Check if message is for this plugin
            if (message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Login + 1))
            {
                return;
            }
                
            switch (message.Tag)
            {
                case LoginTags.LoginSuccess:
                {
                    IsLoggedIn = true;
                    break;
                }
                    
                case LoginTags.LoginFailed:
                {
                    using var reader = message.GetReader();
                    if (reader.Length != 1)
                    {
                        Debug.LogWarning("Invalid LoginFailed error data received");
                    }

                    break;
                }
                    
                case LoginTags.AddPlayerSuccess:
                {
                    break;
                }

                case LoginTags.AddPlayedFailed:
                {
                    using var reader = message.GetReader();
                    if (reader.Length != 1)
                    {
                        Debug.LogWarning("Invalid LoginFailed error data received");
                    }

                    break;
                }
            }
        }

        public static void Login(string username, string password)
        {
            using DarkRiftWriter writer = DarkRiftWriter.Create();
            writer.Write(username);
            writer.Write(Rsa.Encrypt(Encoding.UTF8.GetBytes(password)));

            using var msg = Message.Create(LoginTags.LoginPlayer, writer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
        }

        public static void Logout()
        {
            IsLoggedIn = false;

            using var msg = Message.CreateEmpty(LoginTags.LogoutPlayer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
        }
    }
}