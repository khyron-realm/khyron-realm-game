using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Game;

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
            energy.currentValue = (int)UnlimitedPlayerManager.player.Energy;
            silicon.currentValue = (int)UnlimitedPlayerManager.player.Resources[0].Count;
            lithium.currentValue = (int)UnlimitedPlayerManager.player.Resources[1].Count;
            titanium.currentValue = (int)UnlimitedPlayerManager.player.Resources[2].Count;
        }
        
        private static void InitValuesMax()
        {
            energy.maxValue = maximumLevel;
            silicon.maxValue = maximumLevel;
            lithium.maxValue = maximumLevel;
            titanium.maxValue = maximumLevel;
        }
        
        public static void ChangeMaximumAmountOfResource(GameResources resource, int amount)
        {
            resource.maxValue = amount;
        }
    }
}