using System;
using DarkRift;
using DarkRift.Client;
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
#if UNITY_EDITOR
        private static readonly bool ShowDebug = false;
#endif

        public static PlayerData Player;

        private void Awake()
        {
            Player = null;

            NetworkManager.Client.OnMessageReceived += OnDataHandler;
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
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Headquarters ||
                message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Headquarters + 1)) return;

            switch (message.Tag)
            {
                case HeadquartersTags.PlayerConnected:
                {
                    PlayerConnected();
                    break;
                }
                case HeadquartersTags.PlayerDisconnected:
                {
                    PlayerDisconnected();
                    break;
                }

                case HeadquartersTags.PlayerData:
                {
                    GetPlayerData(message);
                    break;
                }
                case HeadquartersTags.PlayerDataUnavailable:
                {
                    PlayerDataUnavailable();
                    break;
                }

                case HeadquartersTags.GameData:
                {
                    GetGameData(message);
                    break;
                }
                case HeadquartersTags.GameDataUnavailable:
                {
                    GameDataUnavailable();
                    break;
                }

                case HeadquartersTags.UpdateLevelError:
                {
                    UpdateLevelError();
                    break;
                }
                case HeadquartersTags.UpdateEnergyError:
                {
                    UpdateEnergyError();
                    break;
                }

                case HeadquartersTags.ConvertResourcesError:
                {
                    ConversionError(message);
                    break;
                }
                case HeadquartersTags.FinishConversionError:
                {
                    FinishConversionError(message);
                    break;
                }

                case HeadquartersTags.UpgradeRobotError:
                {
                    UpgradeRobotError(message);
                    break;
                }
                case HeadquartersTags.FinishUpgradeError:
                {
                    FinishUpgradeError(message);
                    break;
                }

                case HeadquartersTags.BuildRobotError:
                {
                    BuildRobotError(message);
                    break;
                }
                case HeadquartersTags.FinishBuildError:
                {
                    FinishBuildError(message);
                    break;
                }
                case HeadquartersTags.CancelBuildError:
                {
                    CancelBuildError(message);
                    break;
                }
                
                case HeadquartersTags.RemoveBackgroundTaskFailed:
                {
                    RemoveBackgroundTaskFailed(message);
                    break;
                }
            }
        }

        #region Events

        public delegate void PlayerDataReceivedEventHandler();

        public delegate void PlayerDataUnavailableEventHandler();

        public delegate void GameDataReceivedEventHandler();

        public delegate void GameDataUnavailableEventHandler();

        public delegate void UpdateLevelErrorEventHandler();
        public delegate void UpdateEnergyErrorEventHandler();

        public delegate void ConversionErrorEventHandler(byte errorId);

        public delegate void FinishConversionErrorEventHandler(byte errorId);

        public delegate void UpgradingErrorEventHandler(byte errorId);

        public delegate void FinishUpgradeErrorEventHandler(byte errorId);

        public delegate void BuildingErrorEventHandler(byte errorId);

        public delegate void FinishBuildErrorEventHandler(byte errorId);

        public delegate void CancelBuildErrorEventHandler(byte errorId);
        public delegate void RemoveBackgroundTaskFailedEventHandler();

        public static event PlayerDataReceivedEventHandler OnPlayerDataReceived;
        public static event PlayerDataUnavailableEventHandler OnPlayerDataUnavailable;
        public static event GameDataReceivedEventHandler OnGameDataReceived;
        public static event GameDataUnavailableEventHandler OnGameDataUnavailable;
        public static event UpdateLevelErrorEventHandler OnUpdateLevelError;
        public static event UpdateEnergyErrorEventHandler OnUpdateEnergyError;
        public static event ConversionErrorEventHandler OnConversionError;
        public static event FinishConversionErrorEventHandler OnFinishConversionError;
        public static event UpgradingErrorEventHandler OnUpgradingError;
        public static event FinishUpgradeErrorEventHandler OnFinishUpgradingError;
        public static event BuildingErrorEventHandler OnBuildingError;
        public static event FinishBuildErrorEventHandler OnFinishBuildingError;
        public static event CancelBuildErrorEventHandler OnCancelBuildingError;
        public static event RemoveBackgroundTaskFailedEventHandler OnRemoveBackgroundTaskFailed;

        #endregion

        #region ConnectionHandlers

        /// <summary>
        ///     Player connected actions
        /// </summary>
        private static void PlayerConnected()
        {
#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Player connected");
#endif
        }

        /// <summary>
        ///     Player disconnected actions
        /// </summary>
        private static void PlayerDisconnected()
        {
#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Player disconnected");
#endif
        }

        #endregion

        #region ReceivedCalls

        /// <summary>
        ///     Receive player data from the DarkRift server
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetPlayerData(Message message)
        {
#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Received player data");
#endif

            using var reader = message.GetReader();
            Player = reader.ReadSerializable<PlayerData>();

            OnPlayerDataReceived?.Invoke();
        }

        /// <summary>
        ///     Receive player data unavailable message
        /// </summary>
        private static void PlayerDataUnavailable()
        {
            OnPlayerDataUnavailable?.Invoke();
        }

        /// <summary>
        ///     Receive game data from the DarkRift server
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetGameData(Message message)
        {
#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Received game data");
#endif

            using var reader = message.GetReader();

            OnGameDataReceived?.Invoke();
        }

        /// <summary>
        ///     Receive game data unavailable message
        /// </summary>
        private static void GameDataUnavailable()
        {
            OnGameDataUnavailable?.Invoke();
        }

        /// <summary>
        ///     Receive update level error message
        /// </summary>
        private static void UpdateLevelError()
        {
            OnUpdateLevelError?.Invoke();
        }
        
        /// <summary>
        ///     Receive update energy error message
        /// </summary>
        private static void UpdateEnergyError()
        {
            OnUpdateEnergyError?.Invoke();
        }

        /// <summary>
        ///     Receive the conversion error message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void ConversionError(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Conversion error incorrect data received");
#endif
                return;
            }

            OnConversionError?.Invoke(reader.ReadByte());
        }

        /// <summary>
        ///     Receive the finish conversion error message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FinishConversionError(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Finish conversion error incorrect data received");
#endif
                return;
            }

            OnFinishConversionError?.Invoke(reader.ReadByte());
        }

        /// <summary>
        ///     Receive the upgrade robot error message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void UpgradeRobotError(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Upgrade robot error incorrect data received");
#endif
                return;
            }

            OnUpgradingError?.Invoke(reader.ReadByte());
        }

        /// <summary>
        ///     Receive the finish upgrade robot error message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FinishUpgradeError(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Finish upgrade robot error incorrect data received");
#endif
                return;
            }

            OnFinishUpgradingError?.Invoke(reader.ReadByte());
        }

        /// <summary>
        ///     Receive the build robot error message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void BuildRobotError(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Build robot error incorrect data received");
#endif
                return;
            }

            OnBuildingError?.Invoke(reader.ReadByte());
        }

        /// <summary>
        ///     Receive the finish upgrade robot error message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FinishBuildError(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Finish build robot error incorrect data received");
#endif
                return;
            }

            OnFinishBuildingError?.Invoke(reader.ReadByte());
        }

        /// <summary>
        ///     Receive the cancel upgrade robot error message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void CancelBuildError(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Cancel build robot error incorrect data received");
#endif
                return;
            }

            OnCancelBuildingError?.Invoke(reader.ReadByte());
        }

        private static void RemoveBackgroundTaskFailed(Message message)
        {
            OnRemoveBackgroundTaskFailed?.Invoke();
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

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Requesting player data ...");
#endif
        }

        /// <summary>
        ///     Request for getting the game data
        /// </summary>
        /// <param name="version">0 for the latest version and [version > 0] for checking if the version is up to date</param>
        public static void GameDataRequest(ushort version)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(version);
            using var msg = Message.Create(HeadquartersTags.GameData, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Requesting game data ...");
#endif
        }

        /// <summary>
        ///     Request for updating the level
        /// </summary>
        /// <param name="level">The new level</param>
        /// <param name="experience">The new experience value</param>
        public static void UpdateLevel(byte level, uint experience)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(level);
            writer.Write(experience);
            using var msg = Message.Create(HeadquartersTags.UpdateLevel, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Updating level");
#endif
        }

        /// <summary>
        ///     Request for updating the energy
        /// </summary>
        /// <param name="energy">The new energy value</param>
        public static void UpdateEnergy(uint energy)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(energy);
            using var msg = Message.Create(HeadquartersTags.UpdateEnergy, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Updating energy");
#endif
        }
        
        /// <summary>
        ///     Request for converting the resources to energy
        /// </summary>
        /// <param name="startTime">The starting time of the task</param>
        /// <param name="newResources">The new player resources</param>
        public static void ConversionRequest(DateTime startTime, Resource[] newResources)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(startTime.ToBinary());
            writer.Write(newResources);
            using var msg = Message.Create(HeadquartersTags.ConvertResources, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending resource conversion to the server");
#endif
        }

        /// <summary>
        ///     Request for finishing the conversion of the resources to energy
        /// </summary>
        /// <param name="newEnergy">The new energy value</param>
        /// <param name="newExperience">The new experience value</param>
        public static void FinishConversionRequest(uint newEnergy, uint newExperience)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(newEnergy);
            writer.Write(newExperience);
            using var msg = Message.Create(HeadquartersTags.FinishConversion, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending finish resource conversion to the server");
#endif
        }

        /// <summary>
        ///     Request for upgrading a robot
        /// </summary>
        /// <param name="robotId">The robot type</param>
        /// <param name="startTime">The starting time of the task</param>
        /// <param name="newEnergy">The new energy value</param>
        public static void UpgradingRequest(byte robotId, DateTime startTime, uint newEnergy)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(robotId);
            writer.Write(startTime.ToBinary());
            writer.Write(newEnergy);
            using var msg = Message.Create(HeadquartersTags.UpgradeRobot, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending robot upgrade to the server");
#endif
        }

        /// <summary>
        ///     Request for finishing the upgrading of the robot
        /// </summary>
        /// <param name="robotId">The robot type</param>
        /// <param name="newRobot">The new robot values</param>
        /// <param name="newExperience">The new experience value</param>
        public static void FinishUpgradingRequest(byte robotId, Robot newRobot, uint newExperience)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(robotId);
            writer.Write(newRobot);
            writer.Write(newExperience);
            using var msg = Message.Create(HeadquartersTags.FinishUpgrade, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending finish robot upgrade to the server");
#endif
        }

        /// <summary>
        ///     Request for building a robot
        /// </summary>
        /// <param name="queueNumber">The number of the robot in the queue</param>
        /// <param name="robotId">The robot type</param>
        /// <param name="startTime">The starting time of the task</param>
        /// <param name="newEnergy">The new energy value</param>
        public static void BuildingRequest(ushort queueNumber, byte robotId, long startTime, uint newEnergy)
        {
            using var writer = DarkRiftWriter.Create();

            writer.Write(queueNumber);
            writer.Write(robotId);
            writer.Write(startTime);
            writer.Write(newEnergy);
            using var msg = Message.Create(HeadquartersTags.BuildRobot, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending robot build to the server");
#endif
        }

        /// <summary>
        ///     Request for finishing the building of the robot
        /// </summary>
        /// <param name="queueNumber">The number of the robot in the queue</param>
        /// <param name="robotId">The type of the robot</param>
        /// <param name="startTime">The new starting time</param>
        /// <param name="newRobot">The new robot values</param>
        public static void FinishBuildingRequest(ushort queueNumber, byte robotId, DateTime startTime, Robot newRobot)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(queueNumber);
            writer.Write(robotId);
            writer.Write(startTime.ToBinary());
            writer.Write(newRobot);
            using var msg = Message.Create(HeadquartersTags.FinishBuild, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending finish robot build to the server");
#endif
        }

        /// <summary>
        ///     Request for finishing the building of all the robots
        /// </summary>
        /// <param name="queueNumber">The number of the robot in the queue</param>
        /// <param name="robotId">The type of the robot</param>
        /// <param name="startTime">The new starting time</param>
        /// <param name="newRobots">The new values for all robots</param>
        public static void FinishBuildingRequest(ushort queueNumber, byte robotId, DateTime startTime,
            Robot[] newRobots)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(queueNumber);
            writer.Write(robotId);
            writer.Write(startTime.ToBinary());
            writer.Write(newRobots);
            using var msg = Message.Create(HeadquartersTags.FinishBuildMultiple, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending finish robots build to the server");
#endif
        }

        /// <summary>
        ///     Request for cancelling the building of the robot
        /// </summary>
        /// <param name="queueNumber">The number of the robot in the queue</param>
        /// <param name="robotId">The type of the robot</param>
        /// <param name="startTime">The new starting time</param>
        /// <param name="newEnergy">The new energy value</param>
        /// <param name="inProgress">True if the task is in progress or false otherwise</param>
        public static void CancelBuildingRequest(ushort queueNumber, byte robotId, DateTime startTime, uint newEnergy,
            bool inProgress)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(queueNumber);
            writer.Write(robotId);
            writer.Write(startTime.ToBinary());
            writer.Write(newEnergy);
            using var msg =
                Message.Create(inProgress ? HeadquartersTags.CancelInProgressBuild : HeadquartersTags.CancelOnHoldBuild,
                    writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending cancel robot build to the server");
#endif
        }
        
        /// <summary>
        ///     Request for removing a background task
        /// </summary>
        /// <param name="task">The task to be removed</param>
        public static void RemoveBackgroundTaskRequest(BackgroundTask task)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(task);
            using var msg = Message.Create(HeadquartersTags.RemoveBackgroundTask, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);

#if UNITY_EDITOR
            if (ShowDebug) Debug.Log("Sending remove background task to the server");
#endif
        }

        #endregion
    }
}