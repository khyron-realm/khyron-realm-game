using System.Collections.Generic;
using DarkRift;
using DarkRift.Client;
using Networking.Launcher;
using Networking.Tags;
using UnityEngine;

namespace Networking.Auctions
{
    /// <summary>
    ///     Auctions manager that handles the auction and rooms messages
    /// </summary>
    public class AuctionsManager : MonoBehaviour
    {
        public static bool IsHost { get; private set; }
        public static AuctionRoom CurrentAuctionRoom { get; set; }
        
        #region Events

        public delegate void SuccessfulJoinRoomEventHandler(List<Player> playerList);
        public delegate void SuccessfulLeaveRoomEventHandler();
        public delegate void PlayerJoinedEventHandler(Player player);
        public delegate void PlayerLeftEventHandler(uint leftId, uint newHostId);
        public delegate void ReceivedOpenRoomsEventHandler(List<AuctionRoom> roomList);
        public static event SuccessfulJoinRoomEventHandler onSuccessfulJoinRoom;
        public static event SuccessfulLeaveRoomEventHandler onSuccessfulLeaveRoom;
        public static event PlayerJoinedEventHandler onPlayerJoined;
        public static event PlayerLeftEventHandler onPlayerLeft;
        public static event ReceivedOpenRoomsEventHandler onReceivedOpenRooms;
        
        #endregion

        private void Awake()
        {
            NetworkManager.Client.MessageReceived += OnDataHandler;
        }

        private void OnDestroy()
        {
            if (NetworkManager.Client == null)
                return;

            NetworkManager.Client.MessageReceived -= OnDataHandler;
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
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Auctions || message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Auctions + 1)) return;

            switch (message.Tag)
            {
                case AuctionTags.CreateSuccess:
                {
                    CreateSuccess(message);
                    break;
                }

                case AuctionTags.CreateFailed:
                {
                    CreateFailed(message);
                    break;
                }

                case AuctionTags.JoinSuccess:
                {
                    JoinSuccess(message);
                    break;
                }
                
                case AuctionTags.JoinFailed:
                {
                    JoinFailed(message);
                    break;
                }
                
                case AuctionTags.LeaveSuccess:
                {
                    LeaveSuccess(message);
                    break;
                }

                case AuctionTags.PlayerJoined:
                {
                    PlayerJoined(message);
                    break;
                }

                case AuctionTags.PlayerLeft:
                {
                    PlayerLeft(message);
                    break;
                }

                case AuctionTags.GetOpenRooms:
                {
                    GetOpenRooms(message);
                    break;
                }
                
                case AuctionTags.GetOpenRoomsFailed:
                {
                    GetOpenRoomsFailed(message);
                    break;
                }
                
                case AuctionTags.StartAuctionSuccess:
                {
                    StartAuctionSuccess(message);
                    break;
                }
                
                case AuctionTags.StartAuctionFailed:
                {
                    StartAuctionFailed(message);
                    break;
                }
            }
        }

        #region ReceivedCalls

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void CreateSuccess(Message message)
        {
            using var reader = message.GetReader();
            var room = reader.ReadSerializable<AuctionRoom>();
            var player = reader.ReadSerializable<Player>();

            IsHost = player.IsHost;
            CurrentAuctionRoom = room;
            
            onSuccessfulJoinRoom?.Invoke(new List<Player> {player});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void CreateFailed(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid CreateRoomFailed error data received");
                return;
            }

            switch (reader.ReadByte())
            {
                case 0:
                {
                    Debug.Log("Invalid CreateRoom data send");
                    break;
                }
                case 1:
                {
                    Debug.Log("Player not logged in");
                    // TO-DO
                    // go to login
                    break;
                }
                default:
                {
                    Debug.Log("Invalid errorId");
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void JoinSuccess(Message message)
        {
            var playerList = new List<Player>();
            
            using var reader = message.GetReader();
            
            CurrentAuctionRoom = reader.ReadSerializable<AuctionRoom>();
            while (reader.Position < reader.Length)
            {
                var player = reader.ReadSerializable<Player>();
                playerList.Add(player);
            }

            IsHost = playerList.Find(p => p.Id == NetworkManager.Client.ID).IsHost;
            
            onSuccessfulJoinRoom?.Invoke(playerList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void JoinFailed(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid JoinRoomFailed error data received");
            }
            else
            {
                switch (reader.ReadByte())
                {
                    case 0:
                    {
                        Debug.Log("Invalid JoinRoom data sent");
                        break;
                    }
                    case 1:
                    {
                        Debug.Log("Player not logged in");
                        break;
                    }
                    case 2:
                    {
                        Debug.Log("Player already is in a room");
                        break;
                    }
                    case 3:
                    {
                        Debug.Log("Room doesn't exist anymore");
                        break;
                    }
                    default:
                    {
                        Debug.Log("Invalid errorId");
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void LeaveSuccess(Message message)
        {
            CurrentAuctionRoom = null;
            
            onSuccessfulLeaveRoom?.Invoke();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="message"></param>
        private static void PlayerJoined(Message message)
        {
            using var reader = message.GetReader();

            var player = reader.ReadSerializable<Player>();
            
            onPlayerJoined?.Invoke(player);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void PlayerLeft(Message message)
        {
            using var reader = message.GetReader();

            var leftId = reader.ReadUInt32();
            var newHostId = reader.ReadUInt32();
            var leaverName = reader.ReadString();

            if (newHostId == NetworkManager.Client.ID)
            {
                IsHost = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void GetOpenRooms(Message message)
        {
            var roomList = new List<AuctionRoom>();
            
            using var reader = message.GetReader();
            while (reader.Position < reader.Length)
            {
                roomList.Add(reader.ReadSerializable<AuctionRoom>());
            }
            
            onReceivedOpenRooms?.Invoke(roomList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void GetOpenRoomsFailed(Message message)
        {
            Debug.Log("Player not logged in");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void StartAuctionSuccess(Message message)
        {
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private static void StartAuctionFailed(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid StartAuction error data received");
            }

            switch (reader.ReadByte())
            {
                case 0:
                {
                    Debug.Log("Invalid CreateRoom data sent");
                    break;
                }
                case 1:
                {
                    Debug.Log("Player not logged in");
                    break;
                }
                case 2:
                {
                    Debug.Log("You are not the host");
                    break;
                }
                default:
                {
                    Debug.Log("Invalid errorId");
                    break;
                }
            }
        }

        #endregion
        
        #region NetworkCalls

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="isVisible"></param>
        public static void CreateAuctionRoom(string roomName, bool isVisible)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(roomName);
            writer.Write(isVisible);
            using var msg = Message.Create(AuctionTags.Create, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomId"></param>
        public static void JoinAuctionRoom(ushort roomId)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(roomId);
            using var msg = Message.Create(AuctionTags.Join, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void LeaveAuctionRoom()
        {
            using var msg = Message.CreateEmpty(AuctionTags.Leave);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public static void GetOpenAuctionRooms()
        {
            using var msg = Message.CreateEmpty(AuctionTags.GetOpenRooms);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        public static void StartAuction()
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(CurrentAuctionRoom.Id);
            using var msg = Message.Create(AuctionTags.StartAuction, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        #endregion
    }
}