using System;
using System.Collections.Generic;
using Networking.Launcher;
using UnityEngine;
using DarkRift;
using DarkRift.Client;

namespace Networking.Chat
{
    /// <summary>
    ///     Chat manager that handles the chat messages
    /// </summary>
    public class ChatManager : MonoBehaviour
    {
        public static List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        public static List<string> SavedChatGroups { get; private set; }

        public static Dictionary<MessageType, Color> ChatColors { get; } = new Dictionary<MessageType, Color>();

        #region Events

        

        #endregion

        private void Start()
        {
            // Set chat colors
            // TO-DO
            
            // Get all saved chat groups
            // TO-DO

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
            if (message.Tag < Tags.Tags.TagsPerPlugin * Tags.Tags.Chat || message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Chat + 1)) return;

            switch (message.Tag)
            {
                
            }
        }

        #region NetworkCalls

        

        #endregion

        #region ReceivedCalls

        

        #endregion

        #region Commands

        

        #endregion

        #region Helpers

        

        #endregion
    }
}