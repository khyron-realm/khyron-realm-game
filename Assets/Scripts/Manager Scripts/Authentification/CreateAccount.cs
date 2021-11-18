using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Login;
using Panels;
using AuxiliaryClasses;


namespace Authentification
{
    public class CreateAccount : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private InputField _userNameField;
        [SerializeField] private InputField _passwordField;

        [SerializeField] private ChangeScreen _screenToChange;
        [SerializeField] private Text _errorsText;
        #endregion

        #region "Private members" 
        private string _userName = "";
        private string _password = "";
        #endregion

        #region "Awake"
        private void Awake()
        {
            LoginManager.OnSuccessfulAddUser += SuccessfulAddUser;
            LoginManager.OnFailedAddUser += FailedAddUser;
        }
        #endregion


        /// <summary>
        /// Add user to game
        /// </summary>
        public void AddUser()
        {
            int userLenght = _userNameField.text.ToCharArray().Length;
            int userPassword = _passwordField.text.ToCharArray().Length;

            if(userLenght > 6 && userPassword > 6)
            {
                _userName = _userNameField.text;
                _password = _passwordField.text;

                LoginManager.AddUser(_userName, _password);
            }
            else
            {
                FailedAddUser(1);
            }
        }


        #region "Authentification handlers"
        private void SuccessfulAddUser()
        {
            _screenToChange.ScreenChange();
            Animations.MesageErrorAnimation(_errorsText, "Account successfully created", Color.green);
        }
        private void FailedAddUser(byte errorId)
        {
            switch(errorId)
            {
                case 0:
                    Animations.MesageErrorAnimation(_errorsText, "Server error [Code 0]", Color.red);
                    break;

                case 1:
                    Animations.MesageErrorAnimation(_errorsText, "Incorrect User Added. Username and password must have more than 6 characters", Color.red);
                    break;

                case 2:
                    Animations.MesageErrorAnimation(_errorsText, "Server error [Code 2]", Color.red);
                    break;
            }          
        }
        #endregion


        private void OnDestroy()
        {
            LoginManager.OnSuccessfulAddUser -= SuccessfulAddUser;
            LoginManager.OnFailedAddUser -= FailedAddUser;
        }
    }
}