using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Networking.Login;
using Scenes;
using Save;
using AuxiliaryClasses;


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
        #endregion

        #region "Private members" 
        private string _userName;
        private string _password;

        private static bool s_connectionTimeOut = false;
        #endregion

        #region "Awake"
        private void Awake()
        {
            LoginManager.OnSuccessfulLogin += SuccessfulLogin;
            LoginManager.OnFailedLogin += FailedLogin;
        }
        #endregion

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
                s_connectionTimeOut = false;
                LoginManager.Login(_userName, _password, 1);
                StartCoroutine(ConnectionTimeOut());
            }          
            else
            {
                FailedLogin(1, 1);
            }
        }


        #region "LogIn response handlers"
        /// <summary>
        /// Called if login is successful
        /// </summary>
        private void SuccessfulLogin(byte code)
        {
            if (code != 1) return;

            StopCoroutine(ConnectionTimeOut());
            _scene.GoToScene();                    
        }        
        private void FailedLogin(byte errorId, byte id)
        {
            if (id != 1) return;

            switch (errorId)
            {
                case 0:
                    Animations.MesageErrorAnimation(_errorsText, "Server error [Code 0]", Color.red);
                    break;

                case 1:
                    Animations.MesageErrorAnimation(_errorsText, "Login failed. Try again", Color.red);
                    break;

                case 2:
                    Animations.MesageErrorAnimation(_errorsText, "Server error [Code 2]", Color.red);
                    break;

                case 3:
                    Animations.MesageErrorAnimation(_errorsText, "User already in use", Color.red);
                    break;
                
                case 10:
                    Animations.MesageErrorAnimation(_errorsText, "Time out connection", Color.red);
                    break;
            }

            s_connectionTimeOut = true;
        }
        #endregion


        private IEnumerator ConnectionTimeOut()
        {
            yield return new WaitForSeconds(4f);
            
            if(s_connectionTimeOut == false)
            {
                FailedLogin(10, 1);
            }
        }
        

        private void OnDestroy()
        {
            LoginManager.OnSuccessfulLogin -= SuccessfulLogin;
            LoginManager.OnFailedLogin -= FailedLogin;
        }
    }
}