using DarkRift.Client.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking.Game
{
    /// <summary>
    ///     Creates a networking client to connect to the DarkRift server
    /// </summary>
    public class GameControl : Singleton<GameControl>
    {
        [SerializeField] [Tooltip("The DarkRift client communication object")]
        public UnityClient networkClient;

        protected GameControl()
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