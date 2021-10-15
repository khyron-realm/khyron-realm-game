using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Login;
using Scenes;
using Save;


namespace Authentification
{
    public class LogIn : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private InputField _userNameField;
        [SerializeField] private InputField _passwordField;
        [SerializeField] private PlayerValues _playerData;
        #endregion

        #region "Private members" 
        private string _userName = "";
        private string _password = "";
        #endregion

        public event Action OnCredentialsAreGood;

        private void Awake()
        {
            LoginManager.OnSuccessfulLogin += SuccessfulLogin;
            LoginManager.OnFailedLogin += FailedLogin;        
        }

        private void Start()
        {
            AutomaticLogIn();
        }


        /// <summary>
        /// LogIn automatic with the saved credentials
        /// </summary>
        private void AutomaticLogIn()
        {
            LoginManager.Login(_playerData.Username, _playerData.Password);
        }


        /// <summary>
        /// Standard logIn operation that is added to buttons
        /// </summary>
        public void LogInToServer()
        {
            _userName = _userNameField.text;
            _password = _passwordField.text;

            _playerData.Username = _userName;
            _playerData.Password = _password;

            _playerData.SaveData();

            LoginManager.Login(_userName, _password);
        }


        private void SuccessfulLogin()
        {
            OnCredentialsAreGood?.Invoke();
        }


        private void FailedLogin(byte errorId)
        {
            print("Try Again");
        }


        private void OnDestroy()
        {
            LoginManager.OnSuccessfulLogin -= SuccessfulLogin;
            LoginManager.OnFailedLogin -= FailedLogin;
        }
    }
}