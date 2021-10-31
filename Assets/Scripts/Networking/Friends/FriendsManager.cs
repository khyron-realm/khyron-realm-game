using Networking.Launcher;
using UnityEngine;
using DarkRift;
using DarkRift.Client;
using Networking.Chat;
using Networking.Tags;

namespace Networking.Friends
{
    public class FriendsManager : MonoBehaviour
    {
        #region Events

        public delegate void SuccessfulFriendRequestEventHandler(string friendName);
        public delegate void NewFriendRequestEventHandler(string friendName);
        public delegate void SuccessfulDeclineRequestEventHandler(string friendName);
        public delegate void SuccessfulAcceptRequestEventHandler(string friendName, bool online);
        public delegate void SuccessfulRemoveFriendEventHandler(string friendName);
        public delegate void SuccessfulGetAllFriendsEventHandler(string[] onlineFriends, string[] offlineFriends,
            string[] openRequests, string[] unansweredRequests);
        public delegate void FriendLoginEventHandler(string friendName);
        public delegate void FriendLogoutEventHandler(string friendName);
        public static event SuccessfulFriendRequestEventHandler OnSuccessfulFriendRequest;
        public static event NewFriendRequestEventHandler OnNewFriendRequest;
        public static event SuccessfulDeclineRequestEventHandler OnSuccessfulDeclineRequest;
        public static event SuccessfulAcceptRequestEventHandler OnSuccessfulAcceptRequest;
        public static event SuccessfulRemoveFriendEventHandler OnSuccessfulRemoveFriend;
        public static event SuccessfulGetAllFriendsEventHandler OnSuccessfulGetAllFriends;
        public static event FriendLoginEventHandler OnFriendLogin;
        public static event FriendLogoutEventHandler OnFriendLogout;

        #endregion
        
        private void Start()
        {
            NetworkManager.Client.MessageReceived += OnDataHandler;
        }

        private void OnDestroy()
        {
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
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Friends ||
                message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Friends + 1)) return;

            switch (message.Tag)
            {
                case FriendsTags.FriendRequest:
                {
                    FriendRequest(message);
                    break;
                }
                
                case FriendsTags.RequestSuccess:
                {
                    RequestSuccess(message);
                    break;
                }
                
                case FriendsTags.RequestFailed:
                {
                    RequestFailed(message);
                    break;
                }
                
                case FriendsTags.DeclineRequestSuccess:
                {
                    DeclineRequestSuccess(message);
                    break;
                }
                
                case FriendsTags.DeclineRequestFailed:
                {
                    DeclineRequestFailed(message);
                    break;
                }
                
                case FriendsTags.AcceptRequestSuccess:
                {
                    AcceptRequestSuccess(message);
                    break;
                }
                
                case FriendsTags.AcceptRequestFailed:
                {
                    AcceptRequestFailed(message);
                    break;
                }
                
                case FriendsTags.RemoveFriendSuccess:
                {
                    RemoveFriendSuccess(message);
                    break;
                }
                
                case FriendsTags.RemoveFriendFailed:
                {
                    RemoveFriendFailed(message);
                    break;
                }
                
                case FriendsTags.GetAllFriends:
                {
                    GetAllFriends(message);
                    break;
                }
                
                case FriendsTags.GetAllFriendsFailed:
                {
                    GetAllFriendsFailed(message);
                    break;
                }
                
                case FriendsTags.FriendLoggedIn:
                {
                    FriendLoggedIn(message);
                    break;
                }
                
                case FriendsTags.FriendLoggedOut:
                {
                    FriendLoggedOut(message);
                    break;
                }
            }
        }

        #region ReceivedCalls

        /// <summary>
        ///     New friend request received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FriendRequest(Message message)
        {
            using var reader = message.GetReader();
            var friendName = reader.ReadString();
            ChatManager.ServerMessage(friendName + " wants to add you as a friend", MessageType.Info);
            
            OnNewFriendRequest?.Invoke(friendName);
        }

        /// <summary>
        ///     Friend request successfully
        /// </summary>
        /// <param name="message">The message received</param>
        private static void RequestSuccess(Message message)
        {
            using var reader = message.GetReader();
            var friendName = reader.ReadString();
            ChatManager.ServerMessage("Friend request sent", MessageType.Info);
            
            OnSuccessfulFriendRequest?.Invoke(friendName);
        }
        
        /// <summary>
        ///     Request failed message received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void RequestFailed(Message message)
        {
            var content = "Failed to send a friend request";
            using var reader = message.GetReader();

            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid RequestFailed Error data received");
            }
            else
            {
                switch (reader.ReadByte())
                {
                    case 0:
                        Debug.Log("Invalid Friend Request data sent!");
                        break;
                    case 1:
                        Debug.Log("Not logged in!");
                        break;
                    case 2:
                        Debug.Log("Database Error");
                        break;
                    case 3:
                        Debug.Log("No user with that name found!");
                        content = "Username doesn't exist.";
                        break;
                    case 4:
                        Debug.Log("Friend request failed. You are already friends or have an open request");
                        content = "You are already friends or have an open request with this player.";
                        break;
                    default:
                        Debug.Log("Invalid errorId!");
                        break;
                }
            }
            
            ChatManager.ServerMessage(content, MessageType.Error);
        }
        
        /// <summary>
        ///     Successfully declined the friend request
        /// </summary>
        /// <param name="message">The message received</param>
        private static void DeclineRequestSuccess(Message message)
        {
            using var reader = message.GetReader();
            var friendName = reader.ReadString();
            var isSender = reader.ReadBoolean();
            var content = isSender
                ? "Declined " + friendName + "'s friend request."
                : friendName + " declined your friend request.";
            ChatManager.ServerMessage(content, MessageType.Error);

            OnSuccessfulDeclineRequest?.Invoke(friendName);
        }
        
        /// <summary>
        ///     Decline request failed message received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void DeclineRequestFailed(Message message)
        {
            ChatManager.ServerMessage("Failed to decline request.", MessageType.Error);

            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid DeclineRequestFailed Error data received.");
                return;
            }

            switch (reader.ReadByte())
            {
                case 0:
                    Debug.Log("Invalid Decline Request data sent!");
                    break;
                case 1:
                    Debug.Log("Not logged in!");
                    break;
                case 2:
                    Debug.Log("Database Error");
                    break;
                default:
                    Debug.Log("Invalid errorId!");
                    break;
            }
        }
        
        /// <summary>
        ///     Request accepted by the friend 
        /// </summary>
        /// <param name="message">The message received</param>
        private static void AcceptRequestSuccess(Message message)
        {
            using var reader = message.GetReader();
            var friendName = reader.ReadString();
            var isSender = reader.ReadBoolean();
            ChatManager.ServerMessage("Added " + friendName + " to your friend list.", MessageType.Info);

            OnSuccessfulAcceptRequest?.Invoke(friendName, isSender);
        }
        
        /// <summary>
        ///     Failed to accept the friend request message received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void AcceptRequestFailed(Message message)
        {
            ChatManager.ServerMessage("Failed to accept request.", MessageType.Error);
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid DeclineRequestFailed Error data received.");
                return;
            }

            switch (reader.ReadByte())
            {
                case 0:
                    Debug.Log("Invalid Accept Request data sent!");
                    break;
                case 1:
                    Debug.Log("Not logged in!");
                    break;
                case 2:
                    Debug.Log("Database Error");
                    break;
                default:
                    Debug.Log("Invalid errorId!");
                    break;
            }
        }
        
        /// <summary>
        ///     Successfully removed a friend
        /// </summary>
        /// <param name="message">The message received</param>
        private static void RemoveFriendSuccess(Message message)
        {
            using var reader = message.GetReader();
            var friendName = reader.ReadString();
            var isSender = reader.ReadBoolean();
            var content = isSender
                ? "Removed " + friendName + " from your friend list."
                : friendName + " removed you from his friend list.";
            ChatManager.ServerMessage(content, MessageType.Error);

            OnSuccessfulRemoveFriend?.Invoke(friendName);
        }
        
        /// <summary>
        ///     Remove friend failed message request
        /// </summary>
        /// <param name="message">The message received</param>
        private static void RemoveFriendFailed(Message message)
        {
            ChatManager.ServerMessage("Failed to remove friend.", MessageType.Error);
            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid RemoveFriend Error data received.");
                return;
            }

            switch (reader.ReadByte())
            {
                case 0:
                    Debug.Log("Invalid Remove Friend data sent!");
                    break;
                case 1:
                    Debug.Log("Not logged in!");
                    break;
                case 2:
                    Debug.Log("Database Error");
                    break;
                default:
                    Debug.Log("Invalid errorId!");
                    break;
            }
        }
        
        /// <summary>
        ///     Friend list received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetAllFriends(Message message)
        {
            using var reader = message.GetReader();
            var onlineFriends = reader.ReadStrings();
            var offlineFriends = reader.ReadStrings();
            var openRequests = reader.ReadStrings();
            var unansweredRequests = reader.ReadStrings();
            foreach (var friend in onlineFriends)
            {
                ChatManager.ServerMessage(friend + " is online.", MessageType.Info);
            }
            OnSuccessfulGetAllFriends?.Invoke(onlineFriends, offlineFriends, openRequests, unansweredRequests);
        }
        
        /// <summary>
        ///     Get friend list failed message received
        /// </summary>
        /// <param name="message">The message received</param>
        private static void GetAllFriendsFailed(Message message)
        {
            ChatManager.ServerMessage("Failed to load the Friend list!", MessageType.Error);

            using var reader = message.GetReader();
            if (reader.Length != 1)
            {
                Debug.LogWarning("Invalid RemoveFriend Error data received.");
                return;
            }

            switch (reader.ReadByte())
            {
                case 1:
                    Debug.Log("Not logged in!");
                    break;
                case 2:
                    Debug.Log("Database Error");
                    break;
                default:
                    Debug.Log("Invalid errorId!");
                    break;
            }
        }
        
        /// <summary>
        ///     A friend has logged in
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FriendLoggedIn(Message message)
        {
            using var reader = message.GetReader();
            var friendName = reader.ReadString();
            ChatManager.ServerMessage(friendName + " is online.", MessageType.Info);

            OnFriendLogin?.Invoke(friendName);
        }
        
        /// <summary>
        ///     A friend has logged out
        /// </summary>
        /// <param name="message">The message received</param>
        private static void FriendLoggedOut(Message message)
        {
            using var reader = message.GetReader();
            var friendName = reader.ReadString();
            ChatManager.ServerMessage(friendName + " is offline.", MessageType.Info);

            OnFriendLogout?.Invoke(friendName);
        }

        #endregion

        #region NetworkCalls

        /// <summary>
        ///     Send a friend request
        /// </summary>
        /// <param name="friendName">The name of the friend</param>
        public static void SendFriendRequest(string friendName)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(friendName);
            using var msg = Message.Create(FriendsTags.FriendRequest, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        /// <summary>
        ///     Decline a friend request
        /// </summary>
        /// <param name="friendName">The name of the friend</param>
        public static void DeclineFriendRequest(string friendName)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(friendName);
            using var msg = Message.Create(FriendsTags.DeclineRequest, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        /// <summary>
        ///     Accept a friend request
        /// </summary>
        /// <param name="friendName">The name of the friend</param>
        public static void AcceptFriendRequest(string friendName)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(friendName);
            using var msg = Message.Create(FriendsTags.AcceptRequest, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }
        
        /// <summary>
        ///     Remove a friend
        /// </summary>
        /// <param name="friendName">The name of the friend</param>
        public static void RemoveFriend(string friendName)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(friendName);
            using var msg = Message.Create(FriendsTags.RemoveFriend, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Send a friend request
        /// </summary>
        /// <param name="friendName">The name of the friend</param>
        public static void GetAllFriends(string friendName)
        {
            using var msg = Message.CreateEmpty(FriendsTags.GetAllFriends);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        #endregion
    }
}