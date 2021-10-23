using System;
using System.Collections.Generic;
using DarkRift;
using DarkRift.Client;
using Networking.Launcher;
using Networking.Tags;
using UnityEngine;

namespace Networking.Auctions
{
    /// <summary>
    ///     
    /// </summary>
    public class AuctionsManager : MonoBehaviour
    {
        public static bool IsHost { get; private set; }
        public static AuctionRoom CurrentAuctionRoom { get; set; }
        
        #region Events

        public delegate void SuccessfulJoinRoomEventHandler(List<Player> playerList);

        public static event SuccessfulJoinRoomEventHandler onSuccessfulJoinRoom;

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
                    Debug.Log("Invalid CreateRoom data send");
                    break;
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