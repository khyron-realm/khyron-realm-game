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

        
        private Coroutine energy;
        private Coroutine lithium;
        private Coroutine titanium;
        private Coroutine silicon;


        private void Awake()
        {
            InitMaximumLevels();
            InitCurrentLevels();

            _xpBar.MaxValue = StoreDataPlayerStats.levelsThresholds.levelsThresholds[0];
            _xpBar.CurrentValue = StoreDataPlayerStats.currentXp;

            StatsOperations.OnXpAdded += HandleBarAnimationForXpBar;
            StatsOperations.OnLevelUp += HandleBarAnimationForLevelUp;
            ResourcesOperations.OnResourcesModified += HandleBarAnimationForResources;
        }


        private void InitCurrentLevels()
        {
            _energyBar.CurrentValue = StoreDataResources.energy.currentValue;
            _lithiumBar.CurrentValue = StoreDataResources.lithium.currentValue;
            _titaniumBar.CurrentValue = StoreDataResources.titanium.currentValue;
            _silliconBar.CurrentValue = StoreDataResources.silicon.currentValue;
        }
        private void InitMaximumLevels()
        {
            _energyBar.MaxValue = StoreDataResources.maximumLevel;
            _lithiumBar.MaxValue = StoreDataResources.maximumLevel;
            _titaniumBar.MaxValue = StoreDataResources.maximumLevel;
            _silliconBar.MaxValue = StoreDataResources.maximumLevel;
        }
        

        private void HandleBarAnimationForResources(string resource)
        {
            switch (resource)
            {
                case "energy":
                    if (energy != null)
                        StopCoroutine(energy);
                    energy = StartCoroutine(BarAnimation(_energyBar, StoreDataResources.energy.currentValue));
                    break;
                case "lithium":
                    if (lithium != null)
                        StopCoroutine(lithium);
                    lithium = StartCoroutine(BarAnimation(_lithiumBar, StoreDataResources.lithium.currentValue));
                    break;
                case "titanium":
                    if (titanium != null)
                        StopCoroutine(titanium);
                    titanium = StartCoroutine(BarAnimation(_titaniumBar, StoreDataResources.titanium.currentValue));
                    break;
                case "silicon":
                    if (silicon != null)
                        StopCoroutine(silicon);
                    silicon = StartCoroutine(BarAnimation(_silliconBar, StoreDataResources.silicon.currentValue));
                    break;
            }
        }
        private void HandleBarAnimationForXpBar()
        {
            StartCoroutine(BarAnimation(_xpBar, StoreDataPlayerStats.currentXp));
        }
        private void HandleBarAnimationForLevelUp(int level)
        {
            StartCoroutine(BarAnimation(_xpBar, StoreDataPlayerStats.currentXp));
            _xpBar.MaxValue = StoreDataPlayerStats.levelsThresholds.levelsThresholds[StoreDataPlayerStats.currentLevel];
        }


        public ProgressBar WhatBarToUpdate(string resource)
        {
            switch (resource)
            {
                case "energy":
                    return _energyBar;
                case "lithium":
                    return _lithiumBar;
                case "titanium":
                    return _titaniumBar;
                case "silicon":
                    return _silliconBar;
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


        private void OnDestroy()
        {
            StatsOperations.OnXpAdded -= HandleBarAnimationForXpBar;
            StatsOperations.OnLevelUp -= HandleBarAnimationForLevelUp;
            ResourcesOperations.OnResourcesModified -= HandleBarAnimationForResources;
        }
    }
}