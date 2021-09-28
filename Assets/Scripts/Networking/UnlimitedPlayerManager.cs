using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using Networking;

public class UnlimitedPlayerManager : MonoBehaviour
{
    void Awake()
    {
        GameControl.Client.MessageReceived += MessageReceived;
    }

    void MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        using (Message message = e.GetMessage() as Message)
        {
            if (message.Tag == Tags.PlayerConnectTag)
            {
                PlayerConnect(sender, e);
            }
            else if (message.Tag == Tags.PlayerDisconnectTag)
            {
                PlayerDisconnect(sender, e);
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
