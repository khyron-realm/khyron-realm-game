using DarkRift;
using DarkRift.Client;
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
        public delegate void ConversionAcceptedEventHandler();
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
                        if (ShowDebug) Debug.Log("Conversion accepted");
                        OnConversionAccepted?.Invoke();
                        break;
                    }
                    
                    case GameTags.ConversionStatus:
                    {
                        if (ShowDebug) Debug.Log("Conversion status");
                        
                        OnConversionAccepted?.Invoke();
                        break;
                    }
                    
                    case GameTags.ConversionRejected:
                    {
                        if (ShowDebug) Debug.Log("Conversion rejected");
                        using (var reader = message.GetReader())
                        {
                            if (reader.Length != 1)
                            {
                                Debug.LogWarning("Conversion rejected error data received");
                                return;
                            }

                            OnConversionRejected?.Invoke(reader.ReadByte());
                        }
                        
                        break;
                    }

                    case GameTags.ConversionFinished:
                    {
                        ConversionFinished(e);
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
                Debug.Log("Silicon = " + player.Silicon.Name);
                Debug.Log("Lithium = " + player.Lithium.Name);
                Debug.Log("Titanium = " + player.Titanium.Name);
                Debug.Log("Worker = " + player.Worker.Name);
                Debug.Log("Probe = " + player.Probe.Name);
                Debug.Log("Crusher = " + player.Crusher.Name);
            }
        }

        private static void ConversionFinished(MessageReceivedEventArgs e)
        {
            if (ShowDebug) Debug.Log("Conversion finished");

            using var message = e.GetMessage();
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
        
        public static void SendConvertStatusRequest()
        {
            using var msg = Message.CreateEmpty(GameTags.ConversionStatus);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            if(ShowDebug) Debug.Log("Asking for conversion status resources ...");
        }
        
        #endregion
    }
}