using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Login;
using Scenes;
using Save;
using AuxiliaryClasses;
using Networking.Headquarters;


namespace Authentification
{
    public class LogIn : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private InputField _userNameField;
        [SerializeField] private InputField _passwordField;
        [SerializeField] private PlayerValues _playerData;

        [SerializeField] private ChangeScene _scene;
        [SerializeField] private Text _errorsText;

        [Space(20f)]
        [SerializeField] private bool _enableAutomaticLogin;
        #endregion

        #region "Private members" 
        private string _userName;
        private string _password;
        #endregion

        #region "Awake and Start"
        private void Awake()
        {
            LoginManager.OnSuccessfulLogin += SuccessfulLogin;
            LoginManager.OnFailedLogin += FailedLogin;

            HeadquartersManager.OnGameDataReceived += GameDataReceived;
            HeadquartersManager.OnGameDataUnavailable += GameDataUnavailable;
        }

        private void Start()
        {
            if(_enableAutomaticLogin)
            {
                AutomaticLogIn();
            }         
        }
        #endregion

        /// <summary>
        /// LogIn automatic with the saved credentials
        /// </summary>
        private void AutomaticLogIn()
        {
            int uLen = _playerData.Username.ToCharArray().Length;
            int pLen = _playerData.Username.ToCharArray().Length;

            if (_playerData != null && uLen > 5 && pLen > 5)
            {
                LoginManager.Login(_playerData.Username, _playerData.Password);
            }           
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

            int uLen = _userName.ToCharArray().Length;
            int pLen = _password.ToCharArray().Length;

            if (uLen > 5 && pLen > 5)
            {
                _playerData.SaveData();
                LoginManager.Login(_userName, _password);
            }            
        }


        #region "LogIn response handlers"
        /// <summary>
        /// Called if login is successful
        /// </summary>
        private void SuccessfulLogin()
        {
            // TO-DO
            // Send version 0 first time and version number on subsequent checks
            ushort version = 0;
            HeadquartersManager.GameDataRequest(version);            
        }
        private void FailedLogin(byte errorId)
        {
            switch (errorId)
            {
                case 0:
                    Animations.MesageErrorAnimation(_errorsText, "Server error [Code 0]");
                    break;

                case 1:
                    Animations.MesageErrorAnimation(_errorsText, "Login failed. Try again");
                    break;

                case 2:
                    Animations.MesageErrorAnimation(_errorsText, "Server error [Code 2]");
                    break;
            }            
        }      
        #endregion

        #region "GameData response handlers"
        /// <summary>
        /// Called if game data is succesfully received
        /// </summary>
        private void GameDataReceived()
        {
            GameDataValues.SaveDuringGamePlayPlayerData(HeadquartersManager.Game);
            _scene.GoToScene();
        }
        private void GameDataUnavailable()
        {
            Animations.MesageErrorAnimation(_errorsText, "GameData Unavailable");
        }
        #endregion

        
        private void OnDestroy()
        {
            LoginManager.OnSuccessfulLogin -= SuccessfulLogin;
            LoginManager.OnFailedLogin -= FailedLogin;

            HeadquartersManager.OnGameDataReceived -= GameDataReceived;
            HeadquartersManager.OnGameDataUnavailable -= GameDataUnavailable;
        }
    }
}