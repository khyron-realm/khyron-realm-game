using System.Text;
using DarkRift;
using DarkRift.Client;
using Networking.Game;
using Networking.Tags;
using UnityEngine;

namespace Networking.Login
{
    /// <summary>
    ///     Login Manager for handling authentication
    /// </summary>
    public class LoginManager : MonoBehaviour
    {
        [SerializeField]
        public bool ShowDebug = true;
        public static bool IsLoggedIn { get; private set; }

        public delegate void SuccessfulLoginEventHandler();
        public delegate void FailedLoginEventHandler(byte errorId);
        public delegate void SuccessfulAddUserEventHandler();
        public delegate void FailedAddUserEventHandler(byte errorId);
        
        public static event SuccessfulLoginEventHandler onSuccessfulLogin;
        public static event FailedLoginEventHandler onFailedLogin;
        public static event SuccessfulAddUserEventHandler onSuccessfulAddUser;
        public static event FailedAddUserEventHandler onFailedAddUser;

        private void Awake()
        {
            GameControl.Client.MessageReceived += OnDataHandler;
        }

        private void OnDestroy()
        {
            if (GameControl.Client == null) return;

            GameControl.Client.MessageReceived -= OnDataHandler;
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
            if (message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Login + 1)) return;

            switch (message.Tag)
            {
                case LoginTags.LoginSuccess:
                {
                    if (ShowDebug) Debug.Log("Successfully logged in");
                    IsLoggedIn = true;
                    onSuccessfulLogin?.Invoke();
                    break;
                }

                case LoginTags.LoginFailed:
                {
                    if (ShowDebug) Debug.Log("Cannot log in");
                    using var reader = message.GetReader();
                    if (reader.Length != 1)
                    {
                        Debug.LogWarning("Invalid LoginFailed error data received");
                        return;
                    }
                    onFailedLogin?.Invoke(reader.ReadByte());
                    break;
                }

                case LoginTags.AddUserSuccess:
                {
                    if (ShowDebug) Debug.Log("Successfully added user");
                    onSuccessfulAddUser?.Invoke();
                    break;
                }

                case LoginTags.AddUserFailed:
                {
                    if (ShowDebug) Debug.Log("Cannot add a new user");
                    using var reader = message.GetReader();
                    if (reader.Length != 1) Debug.LogWarning("Invalid LoginFailed error data received");
                    onFailedAddUser?.Invoke(reader.ReadByte());
                    break;
                }
            }
        }
        
        #region ReceivedCalls

        #endregion

        #region NetworkCalls

        /// <summary>
        ///     Sends a login message to the server
        /// </summary>
        /// <param name="username">The username string</param>
        /// <param name="password">The user password string</param>
        public static void Login(string username, string password)
        {
            using var writer = DarkRiftWriter.Create();
            writer.Write(username);
            writer.Write(Rsa.Encrypt(Encoding.UTF8.GetBytes(password)));

            using var msg = Message.Create(LoginTags.LoginUser, writer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
            Debug.Log("Logging in");
        }

        /// <summary>
        ///     Sends an add user message to the server
        /// </summary>
        /// <param name="username">The username string</param>
        /// <param name="password">The user password string</param>
        public static void AddUser(string username, string password)
        {
            Debug.Log("Adding user");
            using var writer = DarkRiftWriter.Create();
            writer.Write(username);
            writer.Write(Rsa.Encrypt(Encoding.UTF8.GetBytes(password)));

            using var msg = Message.Create(LoginTags.AddUser, writer);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Sends a logout message to the server
        /// </summary>
        public static void Logout()
        {
            IsLoggedIn = false;

            using var msg = Message.CreateEmpty(LoginTags.LogoutUser);
            GameControl.Client.SendMessage(msg, SendMode.Reliable);
        }

        #endregion
    }
}