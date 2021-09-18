using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Store
{
    public class StoreDataResources : MonoBehaviour
    {
        [SerializeField] private int _maximumLevel;
        public static int maximumLevel;

        // Resources level
        public static int energyLevel;
        public static int lithiumLevel;
        public static int titaniumLevel;
        public static int silliconLevel;

        private void Awake()
        {
            maximumLevel = _maximumLevel;
            InitValuesOfStoredData();
        }

        private static void InitValuesOfStoredData()
        {
            energyLevel = 0;
            lithiumLevel = 0;
            titaniumLevel = 0;
            silliconLevel = 0;
        }  
    }
}