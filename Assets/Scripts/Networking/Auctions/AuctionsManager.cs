using System.Collections.Generic;
using DarkRift;
using DarkRift.Client;
using Networking.Launcher;
using Networking.Mines;
using Networking.Tags;
using UnityEngine;

namespace Networking.Auctions
{
    /// <summary>
    ///     Auctions manager that handles the auction messages
    /// </summary>
    public class AuctionsManager : MonoBehaviour
    {
        public static bool IsHost { get; private set; }
        public static AuctionRoom CurrentAuctionRoom { get; set; }
        public static List<Bid> Bids { get; set; } = new List<Bid>();
        public static List<Player> Players { get; set; } = new List<Player>();
        
        #region Events

        public delegate void SuccessfulJoinRoomEventHandler();
        public delegate void SuccessfulLeaveRoomEventHandler();
        public delegate void PlayerJoinedEventHandler(Player player);
        public delegate void PlayerLeftEventHandler(string playerName);
        public delegate void ReceivedOpenRoomsEventHandler();
        public delegate void GetOpenRoomsFailedEventHandler(byte errorId);
        public delegate void AuctionFinishedEventHandler(uint roomId, uint winner);
        public delegate void AddBidEventHandler();
        public delegate void OverbidEventHandler();
        public delegate void SuccessfulAddBidEventHandler();
        public delegate void FailedAddBidEventHandler();
        public delegate void FailedAddScanEventHandler();
        public static event SuccessfulJoinRoomEventHandler OnSuccessfulJoinRoom;
        public static event SuccessfulLeaveRoomEventHandler OnSuccessfulLeaveRoom;
        public static event PlayerJoinedEventHandler OnPlayerJoined;
        public static event PlayerLeftEventHandler OnPlayerLeft;
        public static event ReceivedOpenRoomsEventHandler OnReceivedOpenRooms;
        public static event GetOpenRoomsFailedEventHandler OnFailedGetOpenRooms;
        public static event AuctionFinishedEventHandler OnAuctionFinished;
        public static event AddBidEventHandler OnAddBid;
        public static event OverbidEventHandler OnOverbid;
        public static event SuccessfulAddBidEventHandler OnSuccessfulAddBid;
        public static event FailedAddBidEventHandler OnFailedAddBid;
        public static event FailedAddScanEventHandler OnFailedAddScan;
        
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
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Auctions ||
                message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Auctions + 1)) return;

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
                
                case AuctionTags.GetRoom:
                {
                    GetRoom(message);
                    break;
                }
                
                case AuctionTags.GetRoomFailed:
                {
                    GetRoomFailed(message);
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

                case AuctionTags.AuctionFinished:
                {
                    AuctionFinished(message);
                    break;
                }
                
                case AuctionTags.AddBid:
                {
                    AddBid(message);
                    break;
                }
                
                case AuctionTags.Overbid:
                {
                    Overbid(message);
                    break;
                }

                case AuctionTags.AddBidSuccessful:
                {
                    AddBidSuccess(message);
                    break;
                }

                case AuctionTags.AddBidFailed:
                {
                    AddBidFailed(message);
                    break;
                }

                case AuctionTags.AddScanFailed:
                {
                    AddScanFailed(message);
                    break;
                }

                case AuctionTags.AddFriendToAuctionSuccessful:
                {
                    // TO-DO
                    break;
                }

                case AuctionTags.AddFriendToAuctionFailed:
                {
                    // TO-DO
                    break;
                }
            }
        }

        #region ReceivedCalls

        /// <summary>
        ///     Room created successfully actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void CreateSuccess(Message message)
        {
            using var reader = message.GetReader();
            var room = reader.ReadSerializable<AuctionRoom>();
            var player = reader.ReadSerializable<Player>();

            IsHost = player.IsHost;
            CurrentAuctionRoom = room;
            Players = new List<Player> {player};
            
            OnSuccessfulJoinRoom?.Invoke();
        }

        /// <summary>
        ///     Failed to create room actions
        /// </summary>
        /// <param name="message">The message received</param>
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
        ///     Join success actions and receive auction room
        /// </summary>
        /// <param name="message">The message received</param>
        private static void JoinSuccess(Message message)
        {
            var playerList = new List<Player>();
            
            using var reader = message.GetReader();
            
            CurrentAuctionRoom = reader.ReadSerializable<AuctionRoom>();
            CurrentAuctionRoom.Scans = reader.ReadSerializables<MineScan>();
            while (reader.Position < reader.Length)
            {
                var player = reader.ReadSerializable<Player>();
                playerList.Add(player);
            }

            Players = playerList;
            
            OnSuccessfulJoinRoom?.Invoke();
        }

        /// <summary>
        ///     Join room failed actions
        /// </summary>
        /// <param name="message">The message received</param>
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
        ///     Leave auction success actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void LeaveSuccess(Message message)
        {
            CurrentAuctionRoom = null;
            
            OnSuccessfulLeaveRoom?.Invoke();
        }

        /// <summary>
        ///     Player joined in the auction room
        /// </summary>
        /// <param name="message">The message received</param>
        private static void PlayerJoined(Message message)
        {
            using var reader = message.GetReader();

            var player = reader.ReadSerializable<Player>();

            Players.Add(player);
            OnPlayerJoined?.Invoke(player);
        }

        /// <summary>
        ///     Player left the auction room
        /// </summary>
        /// <param name="message">The message received</param>
        private static void PlayerLeft(Message message)
        {
            using var reader = message.GetReader();

            var playerName = reader.ReadString();

            Players.RemoveAll(p => p.Name == playerName);
            OnPlayerLeft?.Invoke(playerName);
        }

        /// <summary>
        ///     Get an auction room
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetRoom(Message message)
        {
            using var reader = message.GetReader();
            CurrentAuctionRoom = reader.ReadSerializable<AuctionRoom>();

            OnReceivedOpenRooms?.Invoke();
        }

        /// <summary>
        ///     Get room failed actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetRoomFailed(Message message)
        {
            OnFailedGetOpenRooms?.Invoke(0);
        }
        
        /// <summary>
        ///     Successfully start auction actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void StartAuctionSuccess(Message message)
        {
            Debug.Log("Start auction success");
        }
        
        /// <summary>
        ///     Failed to start the auction
        /// </summary>
        /// <param name="message">The message received</param>
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
        
        /// <summary>
        ///     Auction is finished
        /// </summary>
        /// <param name="message">The message received</param>
        private static void AuctionFinished(Message message)
        {
            using var reader = message.GetReader();
            if (reader.Length != 6)
            {
                Debug.LogWarning("Invalid StartAuction error data received");
            }

            var roomId = reader.ReadUInt32();
            var winner = reader.ReadUInt32();

            OnAuctionFinished?.Invoke(roomId, winner);
        }
        
        /// <summary>
        ///     Successfully received another bid actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void AddBid(Message message)
        {
            using var reader = message.GetReader();
            var bid = reader.ReadSerializable<Bid>();
            
            Bids.Add(bid);

            OnAddBid?.Invoke();
        }
                
        /// <summary>
        ///     Successfully received another bid actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void Overbid(Message message)
        {
            using var reader = message.GetReader();
            var bid = reader.ReadSerializable<Bid>();
            
            Bids.Add(bid);

            OnOverbid?.Invoke();
        }

        /// <summary>
        ///     Successfully add personal bid actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void AddBidSuccess(Message message)
        {
            using var reader = message.GetReader();
            var bid = reader.ReadSerializable<Bid>();
            
            Bids.Add(bid);
            
            OnSuccessfulAddBid?.Invoke();
        }
        
        /// <summary>
        ///     Failed to add a new personal bid actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void AddBidFailed(Message message)
        {
            OnFailedAddBid?.Invoke();
        }
        
        /// <summary>
        ///     Failed to add a new scan actions
        /// </summary>
        /// <param name="message">The message received</param>
        private static void AddScanFailed(Message message)
        {
            OnFailedAddScan?.Invoke();
        }

        #endregion
        
        #region NetworkCalls

        /// <summary>
        ///     Create a new auction room
        /// </summary>
        /// <param name="roomName">The name of the room</param>
        /// <param name="isVisible">True if is public or false if private</param>
        public static void CreateAuctionRoom(string roomName, bool isVisible)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(roomName);
            writer.Write(isVisible);
            using var msg = Message.Create(AuctionTags.Create, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        /// <summary>
        ///     Invite a friend into the auction room
        /// </summary>
        /// <param name="roomId">The id of the room</param>
        /// <param name="friendName">The name of the friend</param>
        public static void InviteFriendIntoAuction(uint roomId, string friendName)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(roomId);
            writer.Write(friendName);
            using var msg = Message.Create(AuctionTags.AddFriendToAuction, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Join an auction room
        /// </summary>
        /// <param name="roomId"></param>
        public static void JoinAuctionRoom(uint roomId)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(roomId);
            using var msg = Message.Create(AuctionTags.Join, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Leave an auction room
        /// </summary>
        public static void LeaveAuctionRoom()
        {
            using var msg = Message.CreateEmpty(AuctionTags.Leave);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Get an auction room
        /// </summary>
        public static void GetAuctionRoom()
        {
            using var msg = Message.CreateEmpty(AuctionTags.GetRoom);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Start an auction room
        /// </summary>
        public static void StartAuction()
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(CurrentAuctionRoom.Id);
            using var msg = Message.Create(AuctionTags.StartAuction, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Adds a bid to the auction and waits for confirmation
        /// </summary>
        /// <param name="bidValue"></param>
        public static void AddBid(uint bidValue)
        {
            Debug.LogWarning("Current Room" + CurrentAuctionRoom.Id);
            using var writer = DarkRiftWriter.Create();
            writer.Write(CurrentAuctionRoom.Id);
            writer.Write(bidValue);
            using var msg = Message.Create(AuctionTags.AddBid, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Adds a scan to the server
        /// </summary>
        /// <param name="scan">The scan</param>
        public static void AddScan(MineScan scan)
        {
            CurrentAuctionRoom.AddScan(scan);
            using var writer = DarkRiftWriter.Create();
            writer.Write(CurrentAuctionRoom.Id);
            writer.Write(scan);
            using var msg = Message.Create(AuctionTags.AddScan, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        #endregion
    }
}