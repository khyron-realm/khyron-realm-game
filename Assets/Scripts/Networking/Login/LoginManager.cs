using System.Text;
using DarkRift;
using DarkRift.Client;
using Networking.Headquarters;
using Networking.Launcher;
using Networking.Tags;
using UnityEngine;

namespace Networking.Login
{
    /// <summary>
    ///     Login Manager for handling authentication
    /// </summary>
    public class LoginManager : MonoBehaviour
    {
        public static bool ShowDebug = true;
        public static bool IsLoggedIn { get; private set; }

        public delegate void SuccessfulLoginEventHandler();
        public delegate void FailedLoginEventHandler(byte errorId);
        public delegate void SuccessfulLogoutEventHandler();
        public delegate void SuccessfulAddUserEventHandler();
        public delegate void FailedAddUserEventHandler(byte errorId);
        
        public static event SuccessfulLoginEventHandler OnSuccessfulLogin;
        public static event FailedLoginEventHandler OnFailedLogin;
        public static event SuccessfulLogoutEventHandler OnSuccessfulLogout;
        public static event SuccessfulAddUserEventHandler OnSuccessfulAddUser;
        public static event FailedAddUserEventHandler OnFailedAddUser;

        private void Awake()
        {
            NetworkManager.Client.MessageReceived += OnDataHandler;
        }

        private void OnDestroy()
        {
            if (NetworkManager.Client == null) return;

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
            if (message.Tag >= Tags.Tags.TagsPerPlugin * (Tags.Tags.Login + 1)) return;

            switch (message.Tag)
            {
                case LoginTags.LoginSuccess:
                {
                    if (ShowDebug) Debug.Log("Successfully logged in");
                    IsLoggedIn = true;
                    OnSuccessfulLogin?.Invoke();
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
                    OnFailedLogin?.Invoke(reader.ReadByte());
                    break;
                }

                case LoginTags.LogoutSuccess:
                {
                    if (ShowDebug) Debug.Log("Successful logout");
                    IsLoggedIn = false;
                    OnSuccessfulLogout?.Invoke();
                    break;
                }

                case LoginTags.AddUserSuccess:
                {
                    if (ShowDebug) Debug.Log("Successfully added user");
                    OnSuccessfulAddUser?.Invoke();
                    break;
                }

                case LoginTags.AddUserFailed:
                {
                    if (ShowDebug) Debug.Log("Cannot add a new user");
                    using var reader = message.GetReader();
                    if (reader.Length != 1) Debug.LogWarning("Invalid LoginFailed error data received");
                    OnFailedAddUser?.Invoke(reader.ReadByte());
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
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
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
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        /// <summary>
        ///     Sends a logout message to the server
        /// </summary>
        public static void Logout()
        {
            IsLoggedIn = false;

            using var msg = Message.CreateEmpty(LoginTags.LogoutUser);
            NetworkManager.Client.SendMessage(msg, SendMode.Reliable);
        }

        #endregion
    }
}