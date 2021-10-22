using System;
using DarkRift;
using DarkRift.Client;
using Networking.Game;
using Networking.Launcher;
using Networking.Tags;
using UnityEngine;

namespace Networking.Headquarters
{
    /// <summary>
    ///     Player manager that handles the game messages
    /// </summary>
    public class HeadquartersManager : MonoBehaviour
    {
        public static bool ShowDebug = true;
        public static PlayerData player;
        public static GameData game;

        #region Handlers

        public delegate void PlayerDataReceivedEventHandler();
        public delegate void PlayerDataUnavailableEventHandler();
        public delegate void GameDataReceivedEventHandler();
        public delegate void GameDataUnavailableEventHandler();
        public delegate void FinishConversionAcceptedEventHandler();
        public delegate void ConversionAcceptedEventHandler();
        public delegate void ConversionRejectedEventHandler(byte errorId);
        public delegate void FinishUpgradeAcceptedEventHandler();
        public delegate void UpgradingAcceptedEventHandler();
        public delegate void UpgradingRejectedEventHandler(byte errorId);
        public delegate void FinishBuildAcceptedEventHandler();
        public delegate void CancelBuildAcceptedEventHandler(byte taskType);
        public delegate void BuildingAcceptedEventHandler();
        public delegate void BuildingRejectedEventHandler(byte errorId);
        public delegate void LevelUpdateEventHandler();
        public delegate void ExperienceUpdateEventHandler();
        public delegate void EnergyUpdateEventHandler();
        public delegate void ResourcesUpdateEventHandler();
        public delegate void RobotsUpdateEventHandler();
        public static event PlayerDataReceivedEventHandler OnPlayerDataReceived;
        public static event PlayerDataUnavailableEventHandler OnPlayerDataUnavailable;
        public static event GameDataReceivedEventHandler OnGameDataReceived;
        public static event GameDataUnavailableEventHandler OnGameDataUnavailable;
        public static event FinishConversionAcceptedEventHandler OnFinishConversionAccepted;
        public static event ConversionAcceptedEventHandler OnConversionAccepted;
        public static event ConversionRejectedEventHandler OnConversionRejected;
        public static event FinishUpgradeAcceptedEventHandler OnFinishUpgradingAccepted;
        public static event UpgradingAcceptedEventHandler OnUpgradingAccepted;
        public static event UpgradingRejectedEventHandler OnUpgradingRejected;
        public static event FinishBuildAcceptedEventHandler OnFinishBuildingAccepted;
        public static event CancelBuildAcceptedEventHandler OnCancelBuildingAccepted;
        public static event BuildingAcceptedEventHandler OnBuildingAccepted;
        public static event BuildingRejectedEventHandler OnBuildingRejected;
        public static event LevelUpdateEventHandler OnLevelUpdate;
        public static event ExperienceUpdateEventHandler OnExperienceUpdate;
        public static event EnergyUpdateEventHandler OnEnergyUpdate;
        public static event ResourcesUpdateEventHandler OnResourcesUpdate;
        public static event RobotsUpdateEventHandler OnRobotsUpdate;

        #endregion
        
        private void Awake()
        {
            player = null;
            game = null;
            NetworkManager.Client.MessageReceived += OnDataHandler;
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
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Headquarters || message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Headquarters + 1)) return;

            switch (message.Tag)
            {
                case HeadquartersTags.PlayerConnected:
                {
                    PlayerConnected(message);
                    break;
                }
                    
                case HeadquartersTags.PlayerDisconnected:
                {
                    PlayerDisconnected(message);
                    break;
                }
                    
                case HeadquartersTags.PlayerData:
                {
                    GetPlayerData(message);
                    break;
                }

                case HeadquartersTags.PlayerDataUnavailable:
                {
                    PlayerDataUnavailable(message);
                    break;
                }
                
                case HeadquartersTags.GameData:
                {
                    GetGameData(message);
                    break;
                }

                case HeadquartersTags.GameDataUnavailable:
                {
                    GameDataUnavailable(message);
                    break;
                }

                case HeadquartersTags.FinishConversionAccepted:
                {
                    FinishConversionAccepted(message);
                    break;
                }

                case HeadquartersTags.ConversionAccepted:
                {
                    ConversionAccepted(message);
                    break;
                }
                    
                case HeadquartersTags.ConversionRejected:
                {
                    ConversionRejected(message);
                    break;
                }

                case HeadquartersTags.FinishUpgradeAccepted:
                {
                    FinishUpgradeAccepted(message);
                    break;
                }
                    
                case HeadquartersTags.UpgradeRobotAccepted:
                {
                    UpgradeRobotAccepted(message);
                    break;
                }
                    
                case HeadquartersTags.UpgradeRobotRejected:
                {
                    UpgradeRobotRejected(message);
                    break;
                }

                case HeadquartersTags.FinishBuildAccepted:
                {
                    FinishBuildAccepted(message);
                    break;
                }
                
                case HeadquartersTags.CancelBuildAccepted:
                {
                    CancelBuildAccepted(message);
                    break;
                }

                case HeadquartersTags.BuildRobotAccepted:
                {
                    BuildRobotAccepted(message);
                    break;
                }
                    
                case HeadquartersTags.BuildRobotRejected:
                {
                    BuildRobotRejected(message);
                    break;
                }

                case HeadquartersTags.LevelUpdate:
                {
                    LevelUpdate(message);
                    break;
                }
                
                case HeadquartersTags.ExperienceUpdate:
                {
                    ExperienceUpdate(message);
                    break;
                }
                
                case HeadquartersTags.EnergyUpdate:
                {
                    EnergyUpdate(message);
                    break;
                }
                
                case HeadquartersTags.ResourcesUpdate:
                {
                    ResourcesUpdate(message);
                    break;
                }
                
                case HeadquartersTags.RobotsUpdate:
                {
                    RobotsUpdate(message);
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
                Debug.Log("Name = " + player.Id);
                Debug.Log("Level = " + player.Level);
                Debug.Log("Experience = " + player.Experience);
                Debug.Log("Energy = " + player.Energy);
                foreach(var r in player.Resources)
                {
                    Debug.Log(" - resource = " + r.Name);
                }
                foreach(var r in player.Robots)
                {
                    Debug.Log(" - robot = " + r.Name);
                }

                BuildTask[] taskQueue = player.TaskQueue;
                Debug.Log(taskQueue.Length == 0 ? "No tasks in progress" : "Tasks in progress");

                foreach (BuildTask task in taskQueue)
                {
                    Debug.Log(" - task: " + task.Id);
                    DateTime time = DateTime.FromBinary(task.StartTime);
                    Debug.Log(" - time: " + time);
                }
            }
            
            OnPlayerDataReceived?.Invoke();
        }

        /// <summary>
        ///     Receive game data from the DarkRift server
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetGameData(Message message)
        {
            if (ShowDebug) Debug.Log("Received game data");
            
            using var reader = message.GetReader();
            game = reader.ReadSerializable<Game.GameData>();

            if (ShowDebug)
            {
                Debug.Log("Game parameters version " + game.Version);
            }
            
            OnGameDataReceived?.Invoke();
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
        ///     Receive game data unavailable message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GameDataUnavailable(Message message)
        {
            OnGameDataUnavailable?.Invoke();
        }

        /// <summary>
        ///     Receive the cancel conversion accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FinishConversionAccepted(Message message)
        {
            OnFinishConversionAccepted?.Invoke();
        }
        
        /// <summary>
        ///     Receive the conversion accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void ConversionAccepted(Message message)
        {
            using var reader = message.GetReader();

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
        ///     Receive the cancel upgrade robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FinishUpgradeAccepted(Message message)
        {
            OnFinishUpgradingAccepted?.Invoke();
        }
        
        /// <summary>
        ///     Receive the upgrade robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void UpgradeRobotAccepted(Message message)
        {
            using var reader = message.GetReader();

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
        ///     Receive the finish build robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FinishBuildAccepted(Message message)
        {
            OnFinishBuildingAccepted?.Invoke();
        }
        
        /// <summary>
        ///     Receive the cancel build robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void CancelBuildAccepted(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Building cancelled error data received");
                return;
            }

            OnCancelBuildingAccepted?.Invoke(reader.ReadByte());
        }
        
        /// <summary>
        ///     Receive the build robot accepted confirmation
        /// </summary>
        /// <param name="message">The message received</param>
        private static void BuildRobotAccepted(Message message)
        {
            using var reader = message.GetReader();

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

        /// <summary>
        ///     Receive a level update with the new value
        /// </summary>
        /// <param name="message">The message received</param>
        public static void LevelUpdate(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Level update error data received");
                return;
            }

            player.Level = reader.ReadByte();
            
            OnLevelUpdate?.Invoke();
        }
        
        /// <summary>
        ///     Receive an experience update with the new value
        /// </summary>
        /// <param name="message">The message received</param>
        public static void ExperienceUpdate(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 2)
            {
                Debug.LogWarning("Experience update error data received");
                return;
            }

            player.Experience = reader.ReadUInt16();
            
            
            OnExperienceUpdate?.Invoke();
        }
        
        /// <summary>
        ///     Receive an energy update with the new value
        /// </summary>
        /// <param name="message">The message received</param>
        public static void EnergyUpdate(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 4)
            {
                Debug.LogWarning("Energy update error data received");
                return;
            }

            player.Energy = reader.ReadUInt32();
            
            OnEnergyUpdate?.Invoke();
        }
        
        /// <summary>
        ///     Receive a resources update with the new value
        /// </summary>
        /// <param name="message">The message received</param>
        public static void ResourcesUpdate(Message message)
        {
            using var reader = message.GetReader();
            /*
            if (reader.Length != 1)
            {
                Debug.LogWarning("Resources update error data received");
                return;
            }
            */

            player.Resources = reader.ReadSerializables<Resource>();
            
            OnResourcesUpdate?.Invoke();
        }
        
        /// <summary>
        ///     Receive a robots update with the new value
        /// </summary>
        /// <param name="message">The message received</param>
        public static void RobotsUpdate(Message message)
        {
            using var reader = message.GetReader();
            /*
            if (reader.Length != 1)
            {
                Debug.LogWarning("Level update error data received");
                return;
            }
            */

            player.Robots = reader.ReadSerializables<Robot>();
            
            OnRobotsUpdate?.Invoke();
        }

        #endregion
        
        #region NetworkCalls
        
        /// <summary>
        ///     Request for getting the player data
        /// </summary>
        public static void PlayerDataRequest()
        {
            using var msg = Message.CreateEmpty(HeadquartersTags.PlayerData);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Requesting player data ...");
        }

        /// <summary>
        ///     Request for getting the game data
        /// </summary>
        /// <param name="version">0 for the latest version and [version > 0] for checking if the version is up to date</param>
        public static void GameDataRequest(ushort version)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(version);
            using var msg = Message.CreateEmpty(HeadquartersTags.GameData);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Requesting game data ...");
        }

        /// <summary>
        ///     Request for converting the resources to energy
        /// </summary>
        /// <param name="startTime">The starting time of the task</param>
        public static void ConversionRequest(DateTime startTime)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(startTime.ToBinary());
            using var msg = Message.Create(HeadquartersTags.ConvertResources, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Trying to convert resources ...");
        }
        
        /// <summary>
        ///     Request for finishing the conversion of the resources to energy
        /// </summary>
        public static void FinishConversionRequest()
        {
            using var msg = Message.CreateEmpty(HeadquartersTags.FinishConversion);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Finishing the conversion of resources ...");
        }

        /// <summary>
        ///     Request for upgrading a robot
        /// </summary>
        /// <param name="robotId">The robot type</param>
        /// <param name="startTime">The starting time of the task</param>
        public static void UpgradingRequest(byte robotId, DateTime startTime)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(robotId);
            writer.Write(startTime.ToBinary());
            using var msg = Message.Create(HeadquartersTags.UpgradeRobot, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Trying to upgrade robot ...");
        }
        
        /// <summary>
        ///     Request for finishing the upgrading of the robot
        /// </summary>
        public static void FinishUpgradingRequest(byte robotId)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(robotId);
            using var msg = Message.Create(HeadquartersTags.FinishUpgrade, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Finishing the upgrading of the robot ...");
        }

        /// <summary>
        ///     Request for building a robot
        /// </summary>
        /// <param name="queueNumber">The number of the robot in the queue</param>
        /// <param name="robotId">The robot type</param>
        /// <param name="startTime">The starting time of the task</param>
        public static void BuildingRequest(ushort queueNumber, byte robotId, DateTime startTime)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(queueNumber);
            writer.Write(robotId);
            writer.Write(startTime.ToBinary());
            using var msg = Message.Create(HeadquartersTags.BuildRobot, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
            if(ShowDebug) Debug.Log("Trying to build robot ...");
        }

        /// <summary>
        ///     Request for finishing the building of the robot
        /// </summary>
        /// <param name="queueNumber">The number of the robot in the queue</param>
        /// <param name="robotId">The type of the robot</param>
        /// <param name="startTime">The starting task time</param>
        /// <param name="isFinished">True if the task is finished or false otherwise</param>
        /// <param name="inProgress">True if the task is in progress or false otherwise</param>
        public static void FinishBuildingRequest(ushort queueNumber, byte robotId, DateTime startTime, bool isFinished, bool inProgress = true)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(queueNumber);
            writer.Write(robotId);
            writer.Write(startTime.ToBinary());
            using var msg =
                Message.Create(
                    isFinished
                        ? HeadquartersTags.FinishBuild
                        : inProgress ? HeadquartersTags.CancelInProgressBuild : HeadquartersTags.CancelOnHoldBuild, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
            
            if(ShowDebug) Debug.Log("Finishing the building of the robot ...");
        }

        #endregion
    }
}