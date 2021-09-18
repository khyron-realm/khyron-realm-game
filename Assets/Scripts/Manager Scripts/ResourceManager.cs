using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Resources
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private List<GameResources> _resources;
        public static List<GameResources> resources;

        private void Awake()
        {
            resources = new List<GameResources>(_resources);
        }
    }
}