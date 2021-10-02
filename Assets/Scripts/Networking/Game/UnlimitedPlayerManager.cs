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
                    case GameTags.PlayerConnectTag:
                    {
                        Debug.Log("Player connected");
                        //PlayerConnect(e);
                        break;
                    }
                    
                    case GameTags.PlayerDisconnectTag:
                    {
                        Debug.Log("Player disconnected");
                        //PlayerDisconnect(e);
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     Player connected actions
        /// </summary>
        /// <param name="e">The client object</param>
        private void PlayerConnect(MessageReceivedEventArgs e)
        {
            using (var message = e.GetMessage())
            {
                using (var reader = message.GetReader())
                {
                    var id = reader.ReadString();
                    var name = reader.ReadString();
                    var level = reader.ReadUInt16();
                    var experience = reader.ReadUInt16();
                    var energy = reader.ReadUInt16();

                    Debug.Log("Player id = " + id);
                }
            }
        }

        /// <summary>
        ///     Player disconnected actions
        /// </summary>
        /// <param name="e">The client object</param>
        private void PlayerDisconnect(MessageReceivedEventArgs e)
        {
        }

        #region Test methods

        private void Login(string username, string password)
        {
            LoginManager.Login(username, password);
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