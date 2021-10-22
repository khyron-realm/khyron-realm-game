using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Networking.Launcher
{
    /// <summary>
    ///     Creates a networking client to connect to the DarkRift server
    /// </summary>
    public class NetworkManager : Singleton<NetworkManager>
    {
        [FormerlySerializedAs("networkServerConnection")] [SerializeField] [Tooltip("The DarkRift client communication object")]
        public DarkriftServerConnection networkClient;

        protected NetworkManager()
        {
        }

        public static DarkriftServerConnection Client => Instance?.networkClient;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            networkClient = GetComponent<DarkriftServerConnection>();

            SceneManager.LoadScene(1);
        }
    }
}