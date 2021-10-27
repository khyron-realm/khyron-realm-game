using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Networking.Login;
using Save;
using Networking.Launcher;


namespace Authentification
{
    public class AutomaticLogIn : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private PlayerValues _playerData;
        [SerializeField] private bool _enableAutomaticLogin;
        #endregion

        private AsyncOperation _loadingOperation;

        public static event Action<AsyncOperation> OnLoginAccepted;
        
        private void Awake()
        {
            LoginManager.OnSuccessfulLogin += SuccessfulLogin;
            LoginManager.OnFailedLogin += FailedLogin;

            NetworkManager.OnConnectionEstablished += ConnectionEstablished;
        }

        /// <summary>
        /// Called when connection has been established
        /// </summary>
        private void ConnectionEstablished()
        {
            if(_enableAutomaticLogin)
            {
                AutomaticLogInMethod();
            }
            else
            {
                FailedLogin(0);
            }            
        }

        private void AutomaticLogInMethod()
        {
            int uLen = _playerData.Username.ToCharArray().Length;
            int pLen = _playerData.Username.ToCharArray().Length;

            if (_playerData != null && uLen > 5 && pLen > 5)
            {
                LoginManager.Login(_playerData.Username, _playerData.Password);
            }
        }

        #region "Handlers"
        private void SuccessfulLogin()
        {
            _loadingOperation = SceneManager.LoadSceneAsync((int)ScenesName.HEADQUARTERS_SCENE, LoadSceneMode.Additive);
            OnLoginAccepted?.Invoke(_loadingOperation);
        }
        private void FailedLogin(byte errorId)
        {
            SceneManager.LoadSceneAsync((int)ScenesName.AUTHENTICATION_SCENE, LoadSceneMode.Additive);
        }
        #endregion

        private void OnDestroy()
        {
            LoginManager.OnSuccessfulLogin -= SuccessfulLogin;
            LoginManager.OnFailedLogin -= FailedLogin;

            NetworkManager.OnConnectionEstablished -= ConnectionEstablished;
        }
    }
}