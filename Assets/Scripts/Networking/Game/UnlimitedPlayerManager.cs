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

        #region Handlers

        public delegate void ConversionAcceptedEventHandler();
        public delegate void ConversionRejectedEventHandler(byte errorId);
        public delegate void UpgradingAcceptedEventHandler();
        public delegate void UpgradingRejectedEventHandler(byte errorId);
        public delegate void BuildingAcceptedEventHandler();
        public delegate void BuildingRejectedEventHandler(byte errorId);
        public static event ConversionAcceptedEventHandler OnConversionAccepted;
        public static event ConversionRejectedEventHandler OnConversionRejected;
        public static event UpgradingAcceptedEventHandler OnUpgradingAccepted;
        public static event UpgradingRejectedEventHandler OnUpgradingRejected;
        public static event BuildingAcceptedEventHandler OnBuildingAccepted;
        public static event BuildingRejectedEventHandler OnBuildingRejected;

        #endregion

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
            using var message = e.GetMessage();
            
            // Check if message is for this plugin
            if (message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Game + 1)) return;

            switch (message.Tag)
            {
                case GameTags.PlayerConnected:
                {
                    PlayerConnected(message);
                    break;
                }
                    
                case GameTags.PlayerDisconnected:
                {
                    PlayerDisconnected(message);
                    break;
                }
                    
                case GameTags.PlayerData:
                {
                    GetPlayerData(message);
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
                    
                case GameTags.UpgradeRobotAccepted:
                {
                    UpgradeRobotAccepted(message);
                    break;
                }
                    
                case GameTags.UpgradeRobotRejected:
                {
                    UpgradeRobotRejected(message);
                    break;
                }

                case GameTags.BuildRobotAccepted:
                {
                    BuildRobotAccepted(message);
                    break;
                }
                    
                case GameTags.BuildRobotRejected:
                {
                    BuildRobotRejected(message);
                    break;
                }
            }
        }

        #region ReceivedCalls

        /// <summary>
        ///     Player connected actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void PlayerConnected(Message message)
        {
            if (ShowDebug) Debug.Log("Player connected");
        }

        /// <summary>
        ///     Player disconnected actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void PlayerDisconnected(Message message)
        {
            if (ShowDebug) Debug.Log("Player disconnected");
        }

        /// <summary>
        ///     Receive player data from the DarkRift server
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetPlayerData(Message message)
        {
            if (ShowDebug) Debug.Log("Received player data");
            
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

        /// <summary>
        ///     Receive the conversion accepted confirmation
        /// </summary>
        /// <param name="message"></param>
        private static void ConversionAccepted(Message message)
        {
            using var reader = message.GetReader();
            DateTime finishTime = DateTime.FromBinary(reader.ReadInt64());
            
            // Compute remaining time
            DateTime remainingTime = finishTime;

            OnConversionAccepted?.Invoke();
        }

        /// <summary>
        ///     Receive the conversion rejected confirmation
        /// </summary>
        /// <param name="message">The message received</param>
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
        
        /// <summary>
        ///     Receive the upgrade robot accepted confirmation
        /// </summary>
        /// <param name="message"></param>
        private static void UpgradeRobotAccepted(Message message)
        {
            using var reader = message.GetReader();
            DateTime finishTime = DateTime.FromBinary(reader.ReadInt64());
            
            // Compute remaining time 
            DateTime remainingTime = finishTime;

            OnUpgradingAccepted?.Invoke();
        }

        /// <summary>
        ///     Receive the upgrade robot rejected confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void UpgradeRobotRejected(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Upgrading rejected error data received");
                return;
            }

            OnUpgradingRejected?.Invoke(reader.ReadByte());
        }
        
        /// <summary>
        ///     Receive the build robot accepted confirmation
        /// </summary>
        /// <param name="message"></param>
        private static void BuildRobotAccepted(Message message)
        {
            using var reader = message.GetReader();
            DateTime finishTime = DateTime.FromBinary(reader.ReadInt64());
            
            // Compute remaining time 
            DateTime remainingTime = finishTime;

            OnBuildingAccepted?.Invoke();
        }

        /// <summary>
        ///     Receive the build robot rejected confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void BuildRobotRejected(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Building rejected error data received");
                return;
            }

            OnBuildingRejected?.Invoke(reader.ReadByte());
        }

        #endregion
        
        #region NetworkCalls
        
        /// <summary>
        ///     Request for getting the player data 
        /// </summary>
        public static void PlayerDataRequest()
        {
            using var msg = Message.CreateEmpty(GameTags.PlayerData);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Requesting player data ...");
        }
        
        /// <summary>
        ///     Request for converting the resources to energy
        /// </summary>
        public static void ConversionRequest()
        {
            using var msg = Message.CreateEmpty(GameTags.ConvertResources);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Trying to convert resources ...");
        }
        
        /// <summary>
        ///     Request for upgrading a robot
        /// </summary>
        /// <param name="robotId">The robot type</param>
        public static void UpgradingRequest(byte robotId)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(robotId);
            using var msg = Message.Create(GameTags.UpgradeRobot, writer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Trying to upgrade robot ...");
        }
        
        /// <summary>
        ///     Request for building a robot
        /// </summary>
        /// <param name="robotId">The robot type</param>
        public static void BuildingRequest(byte robotId)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(robotId);
            using var msg = Message.Create(GameTags.BuildRobot, writer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            if(ShowDebug) Debug.Log("Trying to build robot ...");
        }
        
        #endregion
    }
}