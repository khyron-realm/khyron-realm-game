using DarkRift.Client.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking.Launcher
{
    /// <summary>
    ///     Creates a networking client to connect to the DarkRift server
    /// </summary>
    public class NetworkManager : Singleton<NetworkManager>
    {
        [SerializeField] [Tooltip("The DarkRift client communication object")]
        public UnityClient networkClient;

        protected NetworkManager()
        {
        }

        public static UnityClient Client => Instance?.networkClient;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            networkClient = GetComponent<UnityClient>();

            SceneManager.LoadScene(1);
        }
    }
}