using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using Networking;

public class UnlimitedPlayerManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The DarkRift client communication")]
    private UnityClient client;

    void Awake()
    {
        client.MessageReceived += MessageReceived;
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
    
    void PlayerDisconnect(object sender, MessageReceivedEventArgs e) {}
}
