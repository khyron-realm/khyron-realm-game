using System;
using DarkRift;
using DarkRift.Client;
using Networking.Launcher;
using Networking.Tags;
using UnityEngine;

namespace Networking.Mine
{
    /// <summary>
    ///     Mine manager that handles the mine messages
    /// </summary>
    public class MineManager : MonoBehaviour
    {
        public static MineData Mine;
        
        #region Events
        
        

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
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Mine ||
                message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Mine + 1)) return;

            switch (message.Tag)
            {
                case MineTags.GetMine:
                {
                    
                    break;
                }
                
                case MineTags.GetMineFailed:
                {
                    
                    break;
                }
            }
        }

        #region ReceivedCalls

        

        #endregion

        #region NetworkCalls

        /// <summary>
        ///     Get the mine data
        /// </summary>
        /// <param name="playerName">The name of the player</param>
        public static void GetMine(string playerName)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(playerName);
            using var msg = Message.Create(AuctionTags.Create, writer);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        #endregion
    }
}