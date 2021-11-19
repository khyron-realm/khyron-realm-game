using System.Collections.Generic;
using DarkRift;
using DarkRift.Client;
using Networking.Headquarters;
using Networking.Launcher;
using Networking.Tags;
using UnityEngine;

namespace Networking.Mines
{
    /// <summary>
    ///     Mine manager that handles the mine messages
    /// </summary>
    public class MineManager : MonoBehaviour
    {
        public static List<Mine> MineList { get; set; }
        public static byte CurrentMine { get; set; }                    // index in the list MineList

        #region Events
        
        public delegate void ReceivedMinesEventHandler();
        public delegate void GetMinesFailedEventHandler(byte errorId);
        public delegate void SaveMineEventHandler();
        public delegate void SaveMineFailedEventHandler(byte errorId);
        public delegate void SaveMapPositionFailedEventHandler(byte errorId);
        public delegate void FinishMineEventHandler();
        public delegate void FinishMineFailedEventHandler(byte errorId);
        public static event ReceivedMinesEventHandler OnReceivedMines;
        public static event GetMinesFailedEventHandler OnFailedGetMines;
        public static event SaveMineEventHandler OnSaveMine;
        public static event SaveMineFailedEventHandler OnSaveMineFailed;
        public static event SaveMapPositionFailedEventHandler OnSaveMapPositionFailed;
        public static event FinishMineEventHandler OnFinishMine;
        public static event FinishMineFailedEventHandler OnFinishMineFailed;

        #endregion

        private void Awake()
        {
            NetworkManager.Client.OnMessageReceived += OnDataHandler;
        }

        private void OnDestroy()
        {
            if (NetworkManager.Client == null)
                return;

            NetworkManager.Client.OnMessageReceived -= OnDataHandler;
        }

        /// <summary>
        ///     Message received handler that receives each message and executes the necessary actions
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The client object</param>
        private void OnDataHandler(object sender, MessageReceivedEventArgs e)
        {
            using var message = e.GetMessage();

            // Check if message is for this plugin
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Mine ||
                message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Mine + 1)) return;

            switch (message.Tag)
            {
                case MineTags.GetMines:
                {
                    GetMines(message);
                    break;
                }
                
                case MineTags.GetMinesFailed:
                {
                    GetMinesFailed(message);
                    break;
                }
                
                case MineTags.SaveMine:
                {
                    SaveMine(message);
                    break;
                }
                
                case MineTags.SaveMineFailed:
                {
                    SaveMineFailed(message);
                    break;
                }

                case MineTags.FinishMine:
                {
                    FinishMine(message);
                    break;
                }
                
                case MineTags.FinishMineFailed:
                {
                    FinishMineFailed(message);
                    break;
                }
                
                case MineTags.SaveMapPositionFailed:
                {
                    SaveMapPositionFailed(message);
                    break;
                }
            }
        }
         
        #region ReceivedCalls
        
        /// <summary>
        ///     Get auction room actions and receive room
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetMines(Message message)
        {
            var mineList = new List<Mine>();
            
            using var reader = message.GetReader();
            while (reader.Position < reader.Length)
            {
                mineList.Add(reader.ReadSerializable<Mine>());
            }
            
            MineList = mineList;
            
            OnReceivedMines?.Invoke();
        }
        
        /// <summary>
        ///     Get mines failed actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetMinesFailed(Message message)
        {
            using var reader = message.GetReader();
            
            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Invalid Get Mines Message Failed Error data received");
#endif
                return;
            }
            
            OnFailedGetMines?.Invoke(reader.ReadByte());
        }

        /// <summary>
        ///     Save mine message received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void SaveMine(Message message)
        {
            OnSaveMine?.Invoke();
        }
        
        /// <summary>
        ///     Failed to save mine message received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void SaveMineFailed(Message message)
        {
            using var reader = message.GetReader();

            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Invalid Save Mine Message Failed Error data received");
#endif
                return;
            }

            OnSaveMineFailed?.Invoke(reader.ReadByte());
        }
        
        /// <summary>
        ///     Failed to save mine position in the map message received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void SaveMapPositionFailed(Message message)
        {
            using var reader = message.GetReader();

            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Invalid Save Map Position Message Failed Error data received");
#endif
                return;
            }

            OnSaveMapPositionFailed?.Invoke(reader.ReadByte());
        }

        /// <summary>
        ///     Finish mine message received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FinishMine(Message message)
        {
            OnFinishMine?.Invoke();
        }
        
        /// <summary>
        ///     Failed to finish mine message received
        /// </summary>
        /// <param name="message"></param>
        private static void FinishMineFailed(Message message)
        {
            using var reader = message.GetReader();

            if (reader.Length != 1)
            {
#if UNITY_EDITOR
                Debug.LogWarning("Invalid Message Failed Error data received");
#endif
                return;
            }
            
            OnFinishMineFailed?.Invoke(reader.ReadByte());
        }

        #endregion

        #region NetworkCalls

        /// <summary>
        ///     Get open auction rooms
        /// </summary>
        public static void GetUserMines()
        {
            using var msg = Message.CreateEmpty(MineTags.GetMines);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Save the state of the mine and update robots and resources
        /// </summary>
        /// <param name="mineId">The id of the mine</param>
        /// <param name="blockValues">The state of the mine blocks</param>
        /// <param name="robots">The new robot values</param>
        /// <param name="resources">The new resource values</param>
        public static void SavePlayerMine(uint mineId, bool[] blockValues, Robot[] robots, Resource[] resources)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(mineId);
            writer.Write(blockValues);
            writer.Write(robots);
            writer.Write(resources);
            using var msg = Message.Create(MineTags.SaveMine, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        /// <summary>
        ///     Save the position of the mine in the player map
        /// </summary>
        /// <param name="mineId">The id of the mine</param>
        /// <param name="mapPosition">The position of the mine in the map</param>
        public static void SaveMapPosition(uint mineId, byte mapPosition)
        {
            foreach (Mine item in MineList)
            {
                if(item.Id == mineId)
                {
                    item.MapPosition = mapPosition;
                }
            }
            
            using var writer = DarkRiftWriter.Create();
            writer.Write(mineId);
            writer.Write(mapPosition);
            using var msg = Message.Create(MineTags.SaveMapPosition, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        /// <summary>
        ///     Save the state of the mine and update robots and resources
        /// </summary>
        /// <param name="mineId">The mine id</param>
        /// <param name="robots">The new robot values</param>
        /// <param name="resources">The new resource values</param>
        public static void FinishPlayerMine(uint mineId, Robot[] robots, Resource[] resources)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(mineId);
            writer.Write(robots);
            writer.Write(resources);
            using var msg = Message.Create(MineTags.FinishMine, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        #endregion
    }
}