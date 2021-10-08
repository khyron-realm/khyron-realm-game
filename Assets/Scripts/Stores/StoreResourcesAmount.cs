using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Store
{
    public class StoreResourcesAmount : MonoBehaviour
    {
        [SerializeField] private int _maximumLevel;
        public static int maximumLevel;

        [SerializeField] private GameResources _energy;
        [SerializeField] private GameResources _lithium;
        [SerializeField] private GameResources _titanium;
        [SerializeField] private GameResources _silicon;

        // Resources level
        public static GameResources energy;
        public static GameResources lithium;
        public static GameResources titanium;
        public static GameResources silicon;
        
        private void Awake()
        {
            energy = _energy;
            lithium = _lithium;
            titanium = _titanium;
            silicon = _silicon;

            maximumLevel = _maximumLevel;
            InitValuesOfStoredData();
            InitValuesMax();
        }
        
        private static void InitValuesOfStoredData()
        {
            energy.currentValue = 400;
            lithium.currentValue = 400;
            titanium.currentValue = 400;
            silicon.currentValue = 400;
        }
        
        private static void InitValuesMax()
        {
            energy.maxValue = 400;
            lithium.maxValue = 400;
            titanium.maxValue = 400;
            silicon.maxValue = 400;
        }
        
        public static void ChangeMaximumAmountOfResource(GameResources resource, int amount)
        {
            resource.maxValue = amount;
        }
    }
}