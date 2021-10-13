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
        public static PlayerData player;

        #region Handlers

        public delegate void PlayerDataReceivedEventHandler();
        public delegate void PlayerDataUnavailableEventHandler();
        public delegate void CancelConversionAcceptedEventHandler();
        public delegate void ConversionAcceptedEventHandler(long time);
        public delegate void ConversionRejectedEventHandler(byte errorId);
        public delegate void CancelUpgradeAcceptedEventHandler();
        public delegate void UpgradingAcceptedEventHandler(long time);
        public delegate void UpgradingRejectedEventHandler(byte errorId);
        public delegate void CancelBuildAcceptedEventHandler();
        public delegate void BuildingAcceptedEventHandler(long time);
        public delegate void BuildingRejectedEventHandler(byte errorId);
        public static event PlayerDataReceivedEventHandler OnPlayerDataReceived;
        public static event PlayerDataUnavailableEventHandler OnPlayerDataUnavailable;
        public static event CancelConversionAcceptedEventHandler OnCancelConversionAccepted;
        public static event ConversionAcceptedEventHandler OnConversionAccepted;
        public static event ConversionRejectedEventHandler OnConversionRejected;
        public static event CancelUpgradeAcceptedEventHandler OnCancelUpgradingAccepted;
        public static event UpgradingAcceptedEventHandler OnUpgradingAccepted;
        public static event UpgradingRejectedEventHandler OnUpgradingRejected;
        public static event CancelBuildAcceptedEventHandler OnCancelBuildingAccepted;
        public static event BuildingAcceptedEventHandler OnBuildingAccepted;
        public static event BuildingRejectedEventHandler OnBuildingRejected;

        #endregion
        
        private void Awake()
        {
            player = null;
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

                case GameTags.PlayerDataUnavailable:
                {
                    PlayerDataUnavailable(message);
                    break;
                }

                case GameTags.CancelConversionAccepted:
                {
                    CancelConversionAccepted(message);
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

                case GameTags.CancelUpgradeAccepted:
                {
                    CancelUpgradeAccepted(message);
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

                case GameTags.CancelBuildAccepted:
                {
                    CancelBuildAccepted(message);
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

        #region ConnectionHandlers

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

        #endregion
        
        #region ReceivedCalls
        
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

                BuildTask[] taskQueue = player.TaskQueue;
                Debug.Log(taskQueue.Length == 0 ? "No tasks in progress" : "Tasks in progress");

                foreach (BuildTask task in taskQueue)
                {
                    Debug.Log(" - task: " + task.Id);
                    DateTime time = DateTime.FromBinary(task.EndTime);
                    Debug.Log(" - time: " + time);
                }
            }
            
            OnPlayerDataReceived?.Invoke();
        }

        /// <summary>
        ///     Receive player data unavailable message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void PlayerDataUnavailable(Message message)
        {
            OnPlayerDataUnavailable?.Invoke();
        }

        /// <summary>
        ///     Receive the cancel conversion accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void CancelConversionAccepted(Message message)
        {
            OnCancelConversionAccepted?.Invoke();
        }
        
        /// <summary>
        ///     Receive the conversion accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void ConversionAccepted(Message message)
        {
            using var reader = message.GetReader();
            var finishTime = reader.ReadInt64();
            
            OnConversionAccepted?.Invoke(finishTime);
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
        ///     Receive the cancel upgrade robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void CancelUpgradeAccepted(Message message)
        {
            OnCancelUpgradingAccepted?.Invoke();
        }
        
        /// <summary>
        ///     Receive the upgrade robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void UpgradeRobotAccepted(Message message)
        {
            using var reader = message.GetReader();
            var finishTime = reader.ReadInt64();
            
            OnUpgradingAccepted?.Invoke(finishTime);
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
        ///     Receive the cancel build robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void CancelBuildAccepted(Message message)
        {
            OnCancelBuildingAccepted?.Invoke();
        }
        
        /// <summary>
        ///     Receive the build robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void BuildRobotAccepted(Message message)
        {
            using var reader = message.GetReader();
            var finishTime = reader.ReadInt64();
            
            OnBuildingAccepted?.Invoke(finishTime);
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
        ///     Request for cancelling the conversion of the resources to energy
        /// </summary>
        public static void CancelConversionRequest()
        {
            using var msg = Message.CreateEmpty(GameTags.CancelConversion);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Cancelling the conversion of resources ...");
        }

        /// <summary>
        ///     Request for upgrading a robot
        /// </summary>
        /// <param name="robotId">The robot type</param>
        /// <param name="robotPart">The robot part</param>
        public static void UpgradingRequest(byte robotId)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(robotId);
            using var msg = Message.Create(GameTags.UpgradeRobot, writer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Trying to upgrade robot ...");
        }
        
        /// <summary>
        ///     Request for cancelling the upgrading of the robot
        /// </summary>
        public static void CancelUpgradingRequest()
        {
            using var msg = Message.CreateEmpty(GameTags.CancelUpgrade);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Cancelling the upgrading of the robot ...");
        }

        /// <summary>
        ///     Request for building a robot
        /// </summary>
        /// <param name="queueNumber"></param>
        /// <param name="robotId">The robot type</param>
        public static void BuildingRequest(byte queueNumber, byte robotId)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(queueNumber);
            writer.Write(robotId);
            using var msg = Message.Create(GameTags.BuildRobot, writer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            if(ShowDebug) Debug.Log("Trying to build robot ...");
        }
        
        /// <summary>
        ///     Request for cancelling the building of the robot
        /// </summary>
        public static void CancelBuildingRequest(byte queueNumber)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(queueNumber);
            using var msg = Message.Create(GameTags.CancelBuild, writer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Cancelling the building of the robot ...");
        }
        
        #endregion
    }
}