using System;
using System.Collections.Generic;
using System.Linq;
using Networking.Launcher;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using Networking.Auctions;
using Networking.Headquarters;
using Networking.Login;
using Networking.Tags;

namespace Networking.Chat
{
    /// <summary>
    ///     Chat manager that handles the chat messages
    /// </summary>
    public class ChatManager : MonoBehaviour
    {
        public static List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        public static List<string> SavedChatGroups { get; private set; }

        public static Dictionary<byte, Color> ChatColors { get; } = new Dictionary<byte, Color>();

        #region Events

        public delegate void PrivateMessageEventHandler(ChatMessage message);
        public delegate void RoomMessageEventHandler(ChatMessage message);
        public delegate void GroupMessageEventHandler(ChatMessage message);
        public delegate void ServerMessageEventHandler(ChatMessage message);
        public delegate void SuccessfulJoinGroupEventHandler(string groupName);
        public delegate void SuccessfulLeaveGroupEventHandler(string groupName);
        public static event PrivateMessageEventHandler OnPrivateMessage;
        public static event RoomMessageEventHandler OnRoomMessage;
        public static event GroupMessageEventHandler OnGroupMessage;
        public static event ServerMessageEventHandler OnServerMessage;
        public static event SuccessfulJoinGroupEventHandler OnSuccessfulJoinGroup;
        public static event SuccessfulLeaveGroupEventHandler OnSuccessfulLeaveGroup;

        #endregion

        private void Start()
        {
            // Set chat colors
            ChatColors[MessageType.ChatGroup] = Color.green;
            ChatColors[MessageType.Error] = Color.red;
            ChatColors[MessageType.Info] = Color.blue;
            ChatColors[MessageType.Room] = new Color(1, 0.4f, 0);
            ChatColors[MessageType.Private] = Color.magenta;
            ChatColors[MessageType.All] = Color.black;

            // Get all saved chat groups
            if (PlayerPrefs.GetInt("SetChatGroups") == 0)
            {
                SavedChatGroups = new List<string> {"General"};
                ArrayPrefs.SetStringArray("ChatGroups", SavedChatGroups.ToArray());
                PlayerPrefs.SetInt("SetChatGroups", 1);
            }
            else
            {
                SavedChatGroups = ArrayPrefs.GetStringArray("ChatGroups").ToList();
            }

            NetworkManager.Client.MessageReceived += OnDataHandler;
        }

        private void OnDestroy()
        {
            if(NetworkManager.Client == null)
                return;

            NetworkManager.Client.MessageReceived -= OnDataHandler;
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
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Chat ||
                message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Chat + 1)) return;

            switch (message.Tag)
            {
                case ChatTags.PrivateMessage:
                {
                    PrivateMessage(message);
                    break;
                }

                case ChatTags.SuccessfulPrivateMessage:
                {
                    SuccessfulPrivateMessage(message);
                    break;
                }
                
                case ChatTags.RoomMessage:
                {
                    RoomMessage(message);
                    break;
                }

                case ChatTags.GroupMessage:
                {
                    GroupMessage(message);
                    break;
                }

                case ChatTags.MessageFailed:
                {
                    MessageFailed(message);
                    break;
                }

                case ChatTags.JoinGroup:
                {
                    JoinGroup(message);
                    break;
                }

                case ChatTags.JoinGroupFailed:
                {
                    JoinGroupFailed(message);
                    break;
                }

                case ChatTags.LeaveGroup:
                {
                    LeaveGroup(message);
                    break;
                }

                case ChatTags.LeaveGroupFailed:
                {
                    LeaveGroupFailed(message);
                    break;
                }

                case ChatTags.GetActiveGroups:
                {
                    GetActiveGroups(message);
                    break;
                }

                case ChatTags.GetActiveGroupsFailed:
                {
                    GetActiveGroupsFailed(message);
                    break;
                }
            }
        }
        
        #region ReceivedCalls

        /// <summary>
        ///     Receive a private message from an user
        /// </summary>
        /// <param name="message">The message received</param>
        private static void PrivateMessage(Message message)
        {
            using var reader = message.GetReader();
            var senderName = reader.ReadString();
            var content = reader.ReadString();
            var chatMessage = new ChatMessage(senderName, content, MessageType.Private, senderName);
            
            Messages.Add(chatMessage);
            
            OnPrivateMessage?.Invoke(chatMessage);
        }
        
        /// <summary>
        ///     Private message sent successfully
        /// </summary>
        /// <param name="message">The message received</param>
        private static void SuccessfulPrivateMessage(Message message)
        {
            using var reader = message.GetReader();
            var senderName = reader.ReadString();
            var receiver = reader.ReadString();
            var content = reader.ReadString();
            var chatMessage = new ChatMessage(senderName, content, MessageType.Private, receiver, true);
            
            Messages.Add(chatMessage);
            
            OnPrivateMessage?.Invoke(chatMessage);
        }
        
        /// <summary>
        ///     Receive a room message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void RoomMessage(Message message)
        {
            using var reader = message.GetReader();
            var senderName = reader.ReadString();
            var content = reader.ReadString();
            var chatMessage = new ChatMessage(senderName, content, MessageType.Room, "Room");
            
            Messages.Add(chatMessage);
            
            OnRoomMessage?.Invoke(chatMessage);
        }
        
        /// <summary>
        ///     Receive a group message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GroupMessage(Message message)
        {
            using var reader = message.GetReader();
            var groupName = reader.ReadString();
            var senderName = reader.ReadString();
            var content = reader.ReadString();
            var chatMessage = new ChatMessage(senderName, content, MessageType.ChatGroup, groupName);
            
            Messages.Add(chatMessage);
            
            OnGroupMessage?.Invoke(chatMessage);
        }
        
        /// <summary>
        ///     Receive a failed message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void MessageFailed(Message message)
        {
            var content = "Failed to send a message";
            using var reader = message.GetReader();

            if (reader.Length != 1)
            {
                #if UNITY_EDITOR
                Debug.LogWarning("Invalid Message Failed Error data received");
                #endif
            }
            else
            {
                switch (reader.ReadByte())
                {
                    case 0:
                        Debug.Log("Invalid message data sent");
                        break;
                    case 1:
                        Debug.Log("Not logged in");
                        break;
                    case 2:
                        Debug.Log("Not part of this group");
                        content = "Not connected to this chat channel. Try leaving and rejoining!";
                        break;
                    case 3:
                        Debug.Log("Failed to send message. Player is offline");
                        content = "Player is offline";
                        break;
                    default:
                        Debug.Log("Invalid errorId");
                        break;
                }
            }
            
            ServerMessage(content, MessageType.Error);
        }
        
        /// <summary>
        ///     Receive a join group message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void JoinGroup(Message message)
        {
            using var reader = message.GetReader();
            var group = reader.ReadSerializable<ChatGroup>();
            ServerMessage("You joined the channel: " + group.Name, MessageType.ChatGroup);
            
            if (!SavedChatGroups.Contains(group.Name))
            {
                SavedChatGroups.Add(group.Name);
                ArrayPrefs.SetStringArray("ChatGroups", SavedChatGroups.ToArray());
            }
            
            OnSuccessfulJoinGroup?.Invoke(group.Name);
        }
        
        /// <summary>
        ///     Receive a join group failed message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void JoinGroupFailed(Message message)
        {
            var content = "Failed to join chat group";
            using var reader = message.GetReader();

            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid Join Group Failed Error data received");
            }
            else
            {
                switch (reader.ReadByte())
                {
                    case 0:
                        Debug.Log("Invalid message data sent");
                        break;
                    case 1:
                        Debug.Log("Not logged in");
                        break;
                    case 2:
                        Debug.Log("Already this chat group");
                        content = "You are already in this chat group";
                        break;
                    default:
                        Debug.Log("Invalid errorId");
                        break;
                }
            }
            
            ServerMessage(content, MessageType.Error);
        }
        
        /// <summary>
        ///     Receive a leave group message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void LeaveGroup(Message message)
        {
            using var reader = message.GetReader();
            var groupName = reader.ReadString();
            ServerMessage("You left the channel " + groupName, MessageType.ChatGroup);

            if (SavedChatGroups.Remove(groupName))
            {
                ArrayPrefs.SetStringArray("ChatGroups", SavedChatGroups.ToArray());
            }
            
            OnSuccessfulLeaveGroup?.Invoke(groupName);
        }
        
        /// <summary>
        ///     Receive a leave group failed message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void LeaveGroupFailed(Message message)
        {
            var content = "Failed to leave chat group";
            using var reader = message.GetReader();

            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid Leave Group Failed Error data received");
            }
            else
            {
                switch (reader.ReadByte())
                {
                    case 0:
                        Debug.Log("Invalid Leave Group data sent");
                        break;
                    case 1:
                        Debug.Log("Not logged in");
                        break;
                    case 2:
                        Debug.Log("No such chat group");
                        content = "There is no chat group with this name";
                        break;
                    default:
                        Debug.Log("Invalid errorId");
                        break;
                }
            }
            
            ServerMessage(content, MessageType.Error);
        }
        
        /// <summary>
        ///     Receive get active groups message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetActiveGroups(Message message)
        {
            using var reader = message.GetReader();
            var groupList = reader.ReadStrings().ToList();
            groupList.Sort(string.CompareOrdinal);
            foreach (var group in groupList)
            {
                ServerMessage(group, MessageType.All);
                Debug.LogWarning("received group: " + group);
            }
        }
      
        /// <summary>
        ///     Receive failed get active groups message
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetActiveGroupsFailed(Message message)
        {
            var content = "Failed to get the list of chat groups";
            using var reader = message.GetReader();

            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid Get Active Groups Failed Error data received");
            }
            else
            {
                switch (reader.ReadByte())
                {
                    case 1:
                        Debug.Log("Not logged in");
                        break;
                    default:
                        Debug.Log("Invalid errorId");
                        break;
                }
            }
            
            ServerMessage(content, MessageType.Error);
        }
        
        #endregion

        #region NetworkCalls

        /// <summary>
        ///     Send a private message to a user
        /// </summary>
        /// <param name="receiver">The name of the receiver</param>
        /// <param name="message">The message to be sent</param>
        public static void SendPrivateMessage(string receiver, string message)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(receiver);
            writer.Write(message);
            using var msg = Message.Create(ChatTags.PrivateMessage, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Send a message to a room
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public static void SendRoomMessage(string message)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(AuctionsManager.CurrentAuctionRoom.Id);
            writer.Write(message);
            using var msg = Message.Create(ChatTags.RoomMessage, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Send a message to a group
        /// </summary>
        /// <param name="groupName">The name of the group</param>
        /// <param name="message">The message to be sent</param>
        public static void SendGroupMessage(string groupName, string message)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(AuctionsManager.CurrentAuctionRoom.Id);
            writer.Write(message);
            using var msg = Message.Create(ChatTags.GroupMessage, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Adds a server message
        /// </summary>
        /// <param name="content">The content of the message</param>
        /// <param name="messageType">The type of the message</param>
        public static void ServerMessage(string content, byte messageType)
        {
            var message = new ChatMessage("", content, messageType, "", isServerMessage: true);
            Messages.Add(message);
            
            OnServerMessage?.Invoke(message);
        }
        
        /// <summary>
        ///     Joins a chat group
        /// </summary>
        /// <param name="groupName">The name of the group</param>
        public static void JoinChatGroup(string groupName)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(groupName);
            using var msg = Message.Create(ChatTags.JoinGroup, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        /// <summary>
        ///     Leave a chat group
        /// </summary>
        /// <param name="groupName">The name of the group</param>
        public static void LeaveChatGroup(string groupName)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(groupName);
            using var msg = Message.Create(ChatTags.LeaveGroup, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Request for getting the active groups
        /// </summary>
        public static void GetActiveGroups()
        {
            using var msg = Message.CreateEmpty(ChatTags.GetActiveGroups);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        #endregion

        #region Commands

        public static void Command(string message)
        {
            var split = message.Split(' ');
            var command = split[0];
            var parameter = message.Length == command.Length ? " " : message.Substring(command.Length + 1);

            switch (command)
            {
                case "/join":
                {
                    if (string.IsNullOrWhiteSpace(parameter))
                    {
                        ServerMessage("You have to enter a channel name.", MessageType.Error);
                    }
                    else if (parameter.Length > 10)
                    {
                        ServerMessage("Channel name can't have more than 10 characters!", MessageType.Error);    
                    }
                    else
                    {
                        JoinChatGroup(parameter);
                    }
                    break;
                }
                case "/leave":
                {
                    if (string.IsNullOrWhiteSpace(parameter))
                    {
                        ServerMessage("You have to enter a channel name.", MessageType.Error);
                    }
                    else if (parameter.Length > 10)
                    {
                        ServerMessage("Channel name can't have more than 10 characters!", MessageType.Error);    
                    }
                    else
                    {
                        LeaveChatGroup(parameter);
                    }
                    break;
                }
                case "/list":
                {
                    GetActiveGroups();
                    break;
                }
                case "/logout":
                {
                    LoginManager.Logout();
                    break;
                }
                default:
                {
                    ServerMessage("Unknown command", MessageType.Error);
                    break;
                }
            }
        }

        #endregion

        #region Helpers

        

        #endregion
    }
}