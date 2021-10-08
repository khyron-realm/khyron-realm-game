using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Login;


namespace Authentification
{
    public class LogIn : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private InputField _userNameField;
        [SerializeField] private InputField _passwordField;
        #endregion

        #region "Private members" 
        private string _userName = "";
        private string _password = "";
        #endregion

        private void Awake()
        {
            LoginManager.OnSuccessfulLogin += SuccessfulLogin;
            LoginManager.OnFailedLogin += FailedLogin;
        }


        public void LogInToServer()
        {
            _userName = _userNameField.text;
            _password = _passwordField.text;

            LoginManager.Login(_userName, _password);
        }


        private void SuccessfulLogin()
        {
            //...
        }


        private void FailedLogin(byte errorId)
        {
            //...
        }


        private void OnDestroy()
        {
            LoginManager.OnSuccessfulLogin -= SuccessfulLogin;
            LoginManager.OnFailedLogin -= FailedLogin;
        }
    }
}