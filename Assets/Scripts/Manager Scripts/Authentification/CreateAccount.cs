using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Login;


namespace Authentification
{
    public class CreateAccount : MonoBehaviour
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
            LoginManager.OnSuccessfulAddUser += SuccessfulAddUser;
            LoginManager.OnFailedAddUser += FailedAddUser;
        }


        public void AddUser()
        {
            _userName = _userNameField.text;
            _password = _passwordField.text;

            LoginManager.AddUser(_userName, _password);
        }


        private void SuccessfulAddUser()
        {
            //...
        }


        private void FailedAddUser(byte errorId)
        {
            //...
        }


        private void OnDestroy()
        {
            LoginManager.OnSuccessfulAddUser += SuccessfulAddUser;
            LoginManager.OnFailedAddUser += FailedAddUser;
        }
    }
}