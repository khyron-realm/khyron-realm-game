using DarkRift;
using UnityEngine;
using UnityEngine.Serialization;

namespace Networking.Launcher
{
    /// <summary>
    ///     Creates a networking client to connect to the DarkRift server
    /// </summary>
    public class NetworkManager : Singleton<NetworkManager>
    {
#if UNITY_EDITOR
        private static readonly bool ShowDebug = false;
#endif
        
        [SerializeField]
        [Tooltip("The DarkRift client communication object")]
        public DarkriftServerConnection networkClient;

        #region Events

        public delegate void ServerNotAvailableErrorEventHandler();
        public static event ServerNotAvailableErrorEventHandler OnServerNotAvailable;

        public delegate void ServerAvailableEventHandler();
        public static event ServerAvailableEventHandler OnConnectionEstablished;

        #endregion

        protected NetworkManager()
        {
        }

        public static DarkriftServerConnection Client => Instance?.networkClient;

        public void Awake()
        {
            networkClient = GetComponent<DarkriftServerConnection>();
        }
        
        public void Start()
        {
            if (networkClient.ConnectionState == ConnectionState.Connecting)
            {
#if UNITY_EDITOR
                if(ShowDebug) Debug.Log("Client trying to connect ...");
#endif
            }

            if (networkClient.ConnectionState == ConnectionState.Connected)
            {
#if UNITY_EDITOR
                if(ShowDebug) Debug.Log("Starting Login scene");
#endif
                OnConnectionEstablished?.Invoke();
            }
            else 
            {
#if UNITY_EDITOR
                if(ShowDebug) Debug.LogError("Server not available");
#endif
                OnServerNotAvailable?.Invoke();
            }
        }
    }
}