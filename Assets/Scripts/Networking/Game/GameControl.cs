using System;
using System.Diagnostics.CodeAnalysis;
using DarkRift.Client.Unity;
using UnityEngine;

namespace Networking.Game
{
    public class GameControl : Singleton<GameControl>
    {
        [SerializeField] [Tooltip("The DarkRift client communication object")]
        public UnityClient networkClient;

        [SuppressMessage("ReSharper", "Unity.NoNullPropagation")] 
        public static UnityClient Client => Instance?.networkClient;
        
        protected GameControl() {}

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            networkClient = GetComponent<UnityClient>();
        }
    }
}
