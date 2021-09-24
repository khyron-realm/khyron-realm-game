using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Store
{
    public class StatBarsOperations : MonoBehaviour
    {
        [SerializeField] private ProgressBar _xpBar;
        [SerializeField] private ProgressBar _energyBar;
        [SerializeField] private ProgressBar _lithiumBar;
        [SerializeField] private ProgressBar _titaniumBar;
        [SerializeField] private ProgressBar _silliconBar;


        public static ProgressBar xpBar;
        public static ProgressBar energyBar;
        public static ProgressBar lithiumBar;
        public static ProgressBar titaniumBar;
        public static ProgressBar silliconBar;

        
        private Coroutine energy;
        private Coroutine lithium;
        private Coroutine titanium;
        private Coroutine silicon;


        private void Awake()
        {
            GiveValuesToStatic();
            InitMaximumLevels();
            InitCurrentLevels();

            xpBar.MaxValue = StoreDataPlayerStats.levelsThresholds.levelsThresholds[0];
            xpBar.CurrentValue = StoreDataPlayerStats.currentXp;

            StatsOperations.OnXpAdded += HandleBarAnimationForXpBar;
            StatsOperations.OnLevelUp += HandleBarAnimationForLevelUp;
            ResourcesOperations.OnResourcesModified += HandleBarAnimationForResources;
        }


        private void GiveValuesToStatic()
        {
            xpBar = _xpBar;
            energyBar = _energyBar;
            lithiumBar = _lithiumBar;
            titaniumBar = _titaniumBar;
            silliconBar = _silliconBar;
        }
        private void InitCurrentLevels()
        {
            energyBar.CurrentValue = StoreDataResources.energy.currentValue;
            lithiumBar.CurrentValue = StoreDataResources.lithium.currentValue;
            titaniumBar.CurrentValue = StoreDataResources.titanium.currentValue;
            silliconBar.CurrentValue = StoreDataResources.silicon.currentValue;
        }
        private void InitMaximumLevels()
        {
            energyBar.MaxValue = StoreDataResources.maximumLevel;
            lithiumBar.MaxValue = StoreDataResources.maximumLevel;
            titaniumBar.MaxValue = StoreDataResources.maximumLevel;
            silliconBar.MaxValue = StoreDataResources.maximumLevel;
        }
        

        private void HandleBarAnimationForResources(string resource)
        {
            switch (resource)
            {
                case "energy":
                    if (energy != null)
                        StopCoroutine(energy);
                    energy = StartCoroutine(BarAnimation(energyBar, StoreDataResources.energy.currentValue));
                    break;
                case "lithium":
                    if (lithium != null)
                        StopCoroutine(lithium);
                    lithium = StartCoroutine(BarAnimation(lithiumBar, StoreDataResources.lithium.currentValue));
                    break;
                case "titanium":
                    if (titanium != null)
                        StopCoroutine(titanium);
                    titanium = StartCoroutine(BarAnimation(titaniumBar, StoreDataResources.titanium.currentValue));
                    break;
                case "silicon":
                    if (silicon != null)
                        StopCoroutine(silicon);
                    silicon = StartCoroutine(BarAnimation(silliconBar, StoreDataResources.silicon.currentValue));
                    break;
            }
        }
        private void HandleBarAnimationForXpBar()
        {
            StartCoroutine(BarAnimation(xpBar, StoreDataPlayerStats.currentXp));
        }
        private void HandleBarAnimationForLevelUp(int level)
        {
            StartCoroutine(BarAnimation(xpBar, StoreDataPlayerStats.currentXp));
            xpBar.MaxValue = StoreDataPlayerStats.levelsThresholds.levelsThresholds[StoreDataPlayerStats.currentLevel];
        }


        public static ProgressBar WhatBarToUpdate(string resource)
        {
            switch (resource)
            {
                case "energy":
                    return energyBar;
                case "lithium":
                    return lithiumBar;
                case "titanium":
                    return titaniumBar;
                case "silicon":
                    return silliconBar;
                default:
                    return null;
            }
        }
        public static IEnumerator BarAnimation(ProgressBar bar, int endGoal)
        {
            float temp = 0f;
            while (temp < 1)
            {
                temp += Time.deltaTime / 3;             
                bar.CurrentValue = Mathf.RoundToInt(Mathf.Lerp(bar.CurrentValue, endGoal, temp));
                yield return null;
            }
        }
    }
}