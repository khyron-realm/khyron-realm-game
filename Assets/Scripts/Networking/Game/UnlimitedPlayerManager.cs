using DarkRift;
using DarkRift.Client;
using Networking.Login;
using Networking.Tags;
using UnityEngine;

namespace Networking.Game
{
    /// <summary>
    ///     Player manager that handles the game messages
    /// </summary>
    public class UnlimitedPlayerManager : MonoBehaviour
    {
        [SerializeField]
        public bool ShowDebug = true;
        
        private void Awake()
        {
            GameControl.Client.MessageReceived += OnDataHandler;
        }

        /// <summary>
        ///     Message received handler that receives each message and executes the necessary actions
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The client object</param>
        private void OnDataHandler(object sender, MessageReceivedEventArgs e)
        {
            using (var message = e.GetMessage())
            {
                // Check if message is for this plugin
                if (message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Game + 1)) return;

                switch (message.Tag)
                {
                    case GameTags.PlayerConnected:
                    {
                        PlayerConnected(e);
                        break;
                    }
                    
                    case GameTags.PlayerDisconnected:
                    {
                        PlayerDisconnected(e);
                        break;
                    }

                    case GameTags.PlayerData:
                    {
                        GetPlayerData(e);
                        break;
                    }
                }
            }
        }

        #region ReceivedCalls
        
        /// <summary>
        ///     Player connected actions
        /// </summary>
        /// <param name="e">The client object</param>
        private void PlayerConnected(MessageReceivedEventArgs e)
        {
            if (ShowDebug) Debug.Log("Player connected");
        }

        /// <summary>
        ///     Player disconnected actions
        /// </summary>
        /// <param name="e">The client object</param>
        private void PlayerDisconnected(MessageReceivedEventArgs e)
        {
            if (ShowDebug) Debug.Log("Player disconnected");
        }

        /// <summary>
        ///     Receive player data from the DarkRift server
        /// </summary>
        /// <param name="e">The client object</param>
        private void GetPlayerData(MessageReceivedEventArgs e)
        {
            if (ShowDebug) Debug.Log("Received player data");

            using var message = e.GetMessage();
            using var reader = message.GetReader();
            var player = reader.ReadSerializable<PlayerData>();

            if (ShowDebug)
            {
                Debug.Log("Received data:");
                Debug.Log("Player id = " + player.Id);
                Debug.Log("Player level = " + player.Level);
                Debug.Log("Player experience = " + player.Experience);
                Debug.Log("Player energy = " + player.Energy);
                Debug.Log("Silicon = " + player.Silicon.Name);
                Debug.Log("Lithium = " + player.Lithium.Name);
                Debug.Log("Titanium = " + player.Titanium.Name);
            }
        }
        
        #endregion
        
        #region NetworkCalls
        
        #endregion

        #region Test methods
        
        public static void GetPlayerData()
        {
            using var msg = Message.CreateEmpty(GameTags.PlayerData);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
        }

        private void AddUser(string username, string password)
        {
            Debug.Log("Adding user");
            LoginManager.AddUser(username, password);
            Debug.Log("User added");
        }

        #endregion
    }
}