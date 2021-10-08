using System;
using UnityEngine;

namespace Networking.Login
{
    public class LoginActions : MonoBehaviour
    {
        private void Awake()
        {
            LoginManager.OnSuccessfulLogin += SuccessfulLogin;
            LoginManager.OnFailedLogin += FailedLogin;
            LoginManager.OnSuccessfulAddUser += SuccessfulAddUser;
            LoginManager.OnFailedAddUser += FailedAddUser;
        }

        private void OnDestroy()
        {
            LoginManager.OnSuccessfulLogin -= SuccessfulLogin;
            LoginManager.OnFailedLogin -= FailedLogin;
            LoginManager.OnSuccessfulAddUser -= SuccessfulAddUser;
            LoginManager.OnFailedAddUser -= FailedAddUser;
        }

        #region ServerRequests

        /// <summary>
        /// Sends a login request to the server
        /// </summary>
        public void LoginUser()
        {
            string username = "gigel";
            string password = "12345";
            LoginManager.Login(username, password);
        }
        
        /// <summary>
        /// Sends a logout request to the server
        /// </summary>
        public void LogoutUser()
        {
            LoginManager.Logout();
        }
        
        /// <summary>
        /// Send a register request to the server
        /// </summary>
        public void AddUser()
        {
            string username = "gigel";
            string password = "12345";
            LoginManager.AddUser(username, password);
        }

        #endregion

        #region ProcessServerResponse

        /// <summary>
        /// Process the server response for a successful login
        /// </summary>
        private void SuccessfulLogin()
        {
            
        }

        /// <summary>
        /// Process the server response for a failed login
        /// </summary>
        /// <param name="errorId"></param>
        private void FailedLogin(byte errorId)
        {
            
        }
        
        /// <summary>
        /// Process the server response for a successful register
        /// </summary>
        private void SuccessfulAddUser()
        {
            
        }
        
        /// <summary>
        /// Process the server response for a failed register
        /// </summary>
        /// <param name="errorId"></param>
        private void FailedAddUser(byte errorId)
        {
            
        }

        #endregion
    }
}