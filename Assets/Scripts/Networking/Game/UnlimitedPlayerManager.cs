using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using Networking;
using Networking.Login;
using Networking.Tags;

namespace Networking.Game
{
    public class UnlimitedPlayerManager : MonoBehaviour
    {
        void Awake()
        {
            GameControl.Client.MessageReceived += OnDataHandler;
        }

        void OnDataHandler(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                // Check if message is for this plugin
                if (message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Game + 1))
                {
                    return;
                }

                switch (message.Tag)
                {
                    case GameTags.PlayerConnectTag:
                    {
                        PlayerConnect(sender, e);
                        break;
                    }
                    case GameTags.PlayerDisconnectTag:
                    {
                        PlayerDisconnect(sender, e);
                        break;
                    }
                }
            }
        }

        void PlayerConnect(object sender, MessageReceivedEventArgs e)
        {
            Debug.Log("Player connected");
            using (Message message = e.GetMessage()) {
                using (DarkRiftReader reader = message.GetReader())
                {
                    string id = reader.ReadString();
                    string name = reader.ReadString();
                    ushort level = reader.ReadUInt16();
                    ushort experience = reader.ReadUInt16();
                    ushort energy = reader.ReadUInt16();
                    
                    Debug.Log("Player id = " + id);
                }
            }
        }

        // Method for user log in - to be called from the authentication screen
        void Login(string username, string password)
        {
            LoginManager.Login(username, password);
        }
        
        void PlayerDisconnect(object sender, MessageReceivedEventArgs e) {}
    }
}