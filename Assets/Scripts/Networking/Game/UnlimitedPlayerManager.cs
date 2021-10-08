using System;
using System.Linq;
using DarkRift;
using DarkRift.Client;
using Networking.GameElements;
using Networking.Tags;
using UnityEngine;

namespace Networking.Game
{
    /// <summary>
    ///     Player manager that handles the game messages
    /// </summary>
    public class UnlimitedPlayerManager : MonoBehaviour
    {
        public static bool ShowDebug = true;
        public delegate void ConversionAcceptedEventHandler(DateTime remainingTime);
        public delegate void ConversionRejectedEventHandler(byte errorId);
        public delegate void ConversionFinishedEventHandler();

        public static event ConversionAcceptedEventHandler OnConversionAccepted;
        public static event ConversionRejectedEventHandler OnConversionRejected;
        public static event ConversionFinishedEventHandler OnConversionFinished;

        public static PlayerData player = null;
        
        private void Awake()
        {
            GameControl.Client.MessageReceived += OnDataHandler;
        }

        /// <summary>
        ///     Message received handler that receives each message and executes the necessary actions
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The client object</param>
        private static void OnDataHandler(object sender, MessageReceivedEventArgs e)
        {
            using (var message = e.GetMessage())
            {
                // Check if message is for this plugin
                if (message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Game + 1)) return;

                switch (message.Tag)
                {
                    case GameTags.PlayerConnected:
                    {
                        PlayerConnected(e);
                        break;
                    }
                    
                    case GameTags.PlayerDisconnected:
                    {
                        PlayerDisconnected(e);
                        break;
                    }
                    
                    case GameTags.PlayerData:
                    {
                        GetPlayerData(e);
                        break;
                    }

                    case GameTags.ConversionAccepted:
                    {
                        ConversionAccepted(message);
                        break;
                    }
                    
                    case GameTags.ConversionRejected:
                    {
                        ConversionRejected(message);
                        break;
                    }

                    case GameTags.ConversionFinished:
                    {
                        ConversionFinished(message);
                        break;
                    }
                }
            }
        }

        #region ReceivedCalls
        
        /// <summary>
        ///     Player connected actions
        /// </summary>
        /// <param name="e">The client object</param>
        private static void PlayerConnected(MessageReceivedEventArgs e)
        {
            if (ShowDebug) Debug.Log("Player connected");
        }

        /// <summary>
        ///     Player disconnected actions
        /// </summary>
        /// <param name="e">The client object</param>
        private static void PlayerDisconnected(MessageReceivedEventArgs e)
        {
            if (ShowDebug) Debug.Log("Player disconnected");
        }

        /// <summary>
        ///     Receive player data from the DarkRift server
        /// </summary>
        /// <param name="e">The client object</param>
        private static void GetPlayerData(MessageReceivedEventArgs e)
        {
            if (ShowDebug) Debug.Log("Received player data");

            using var message = e.GetMessage();
            using var reader = message.GetReader();
            player = reader.ReadSerializable<PlayerData>();

            if (ShowDebug)
            {
                Debug.Log("Received data:");
                Debug.Log("Player id = " + player.Id);
                Debug.Log("Player level = " + player.Level);
                Debug.Log("Player experience = " + player.Experience);
                Debug.Log("Player energy = " + player.Energy);
                foreach(int iterator in Enumerable.Range(0, player.Robots.Length))
                {
                    Debug.Log("Resource = " + player.Resources[iterator].Name);
                }
                foreach(int iterator in Enumerable.Range(0, player.Robots.Length))
                {
                    Debug.Log("Robot = " + player.Robots[iterator].Name);
                }
                DateTime time = DateTime.FromBinary(player.ResourceConversion.EndTime);
                Debug.Log("Conversion end time: " + time);
            }
        }

        private static void ConversionAccepted(Message message)
        {
            using var reader = message.GetReader();
            DateTime finishTime = DateTime.FromBinary(reader.ReadInt64());
                        
            // Compute remaining time 
            DateTime remainingTime = finishTime;

            OnConversionAccepted?.Invoke(remainingTime);
        }

        private static void ConversionRejected(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Conversion rejected error data received");
                return;
            }

            OnConversionRejected?.Invoke(reader.ReadByte());
        }

        private static void ConversionFinished(Message message)
        {
            using var reader = message.GetReader();
            player.Energy = reader.ReadUInt32();

            OnConversionFinished?.Invoke();
        }

        #endregion
        
        #region NetworkCalls
        
        public static void GetPlayerDataRequest()
        {
            using var msg = Message.CreateEmpty(GameTags.PlayerData);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        public static void SendConvertRequest()
        {
            using var msg = Message.CreateEmpty(GameTags.ConvertResources);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            if(ShowDebug) Debug.Log("Converting resources ...");
        }
        
        #endregion
    }
}