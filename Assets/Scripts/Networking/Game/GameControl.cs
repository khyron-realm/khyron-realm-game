using DarkRift.Client.Unity;
using UnityEngine;

namespace Networking.Game
{
    /// <summary>
    ///     Creates a networking client to connect to the DarkRift server
    /// </summary>
    public class GameControl : Singleton<GameControl>
    {
        [SerializeField] [Tooltip("The DarkRift client communication object")]
        public UnityClient networkClient;

        private static GameControl s_instance;

        protected GameControl()
        {
        }

        public static UnityClient Client => Instance?.networkClient;

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            networkClient = GetComponent<UnityClient>();

            if (s_instance != null && s_instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                s_instance = this;
            }
        }
    }
}