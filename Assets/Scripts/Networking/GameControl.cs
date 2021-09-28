using System;
using System.Diagnostics.CodeAnalysis;
using DarkRift.Client.Unity;
using UnityEngine;

namespace Networking
{
    public class GameControl : Singleton<GameControl>
    {
        [SerializeField] [Tooltip("The DarkRift client communication object")]
        public UnityClient NetworkClient;

        [SuppressMessage("ReSharper", "Unity.NoNullPropagation")] 
        public static UnityClient Client => Instance?.NetworkClient;
        
        protected GameControl() {}

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            NetworkClient = GetComponent<UnityClient>();
        }
    }
}
