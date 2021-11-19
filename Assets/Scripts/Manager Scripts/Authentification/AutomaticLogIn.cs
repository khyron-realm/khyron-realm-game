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


        #region "Private vars"
        private AsyncOperation _loadingOperation;

        private static PlayerValues s_playerData;
        private static bool s_enableAutomaticLogin;

        private static bool s_once = false;
        #endregion


        #region "Events"
        public static event Action<AsyncOperation, bool> OnAutomaticLoginAccepted;
        public static event Action OnAutomaticLoginFailed;
        #endregion


        private void Awake()
        {
            s_once = false;

            s_playerData = _playerData;
            s_enableAutomaticLogin = _enableAutomaticLogin;

            LoginManager.OnSuccessfulLogin += SuccessfulLogin;
            LoginManager.OnFailedLogin += FailedLogin;

            NetworkManager.OnConnectionEstablished += ConnectionEstablished;
        }

        /// <summary>
        /// Called when connection has been established
        /// </summary>
        public static void ConnectionEstablished()
        {
            if(s_enableAutomaticLogin)
            {
                AutomaticLogInMethod();
            }
            else
            {
                FailedLogin(0);
            }            
        }

        private static void AutomaticLogInMethod()
        {
            int uLen = s_playerData.Username.ToCharArray().Length;
            int pLen = s_playerData.Username.ToCharArray().Length;

            if (s_playerData != null && uLen > 5 && pLen > 5)
            {
                LoginManager.Login(s_playerData.Username, s_playerData.Password, 0);
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
        private static void FailedLogin(byte errorId)
        {
            if(s_once == false)
            {
                SceneManager.LoadSceneAsync((int)ScenesName.AUTHENTICATION_SCENE, LoadSceneMode.Additive);
                OnAutomaticLoginFailed?.Invoke();

                s_once = true;
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