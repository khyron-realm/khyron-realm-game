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

        private bool once = false;

        public static event Action<AsyncOperation, bool> OnAutomaticLoginAccepted;
        public static event Action OnAutomaticLoginFailed;

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
                LoginManager.Login(_playerData.Username, _playerData.Password, 0);
            }
            else
            {
                FailedLogin(0);
            }
        }

        #region "Handlers"
        private void SuccessfulLogin(byte code)
        {
            if (code == 0)
            {
                _loadingOperation = SceneManager.LoadSceneAsync((int)ScenesName.HEADQUARTERS_SCENE, LoadSceneMode.Additive);
                OnAutomaticLoginAccepted?.Invoke(_loadingOperation, true);
            }
        }
        private void FailedLogin(byte errorId)
        {
            if(once == false)
            {
                SceneManager.LoadSceneAsync((int)ScenesName.AUTHENTICATION_SCENE, LoadSceneMode.Additive);

                once = true;
            }
            
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