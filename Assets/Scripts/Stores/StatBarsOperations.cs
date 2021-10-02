using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panels;
using DG.Tweening;


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

            _xpBar.MaxValue = StorePlayerStats.levelsThresholds.levelsThresholds[0];
            _xpBar.CurrentValue = StorePlayerStats.currentXp;

            StatsOperations.OnXpAdded += HandleBarAnimationForXpBar;
            StatsOperations.OnLevelUp += HandleBarAnimationForLevelUp;
            ResourcesOperations.OnResourcesModified += HandleBarAnimationForResources;
        }


        private void InitCurrentLevels()
        {
            _energyBar.CurrentValue = StoreResourcesAmount.energy.currentValue;
            _lithiumBar.CurrentValue = StoreResourcesAmount.lithium.currentValue;
            _titaniumBar.CurrentValue = StoreResourcesAmount.titanium.currentValue;
            _silliconBar.CurrentValue = StoreResourcesAmount.silicon.currentValue;
        }
        private void InitMaximumLevels()
        {
            _energyBar.MaxValue = StoreResourcesAmount.maximumLevel;
            _lithiumBar.MaxValue = StoreResourcesAmount.maximumLevel;
            _titaniumBar.MaxValue = StoreResourcesAmount.maximumLevel;
            _silliconBar.MaxValue = StoreResourcesAmount.maximumLevel;
        }
        

        private void HandleBarAnimationForResources(string resource)
        {
            switch (resource)
            {
                case "energy":
                    if (energy != null)
                        StopCoroutine(energy);
                    energy = StartCoroutine(BarAnimation(_energyBar, StoreResourcesAmount.energy.currentValue));
                    break;
                case "lithium":
                    if (lithium != null)
                        StopCoroutine(lithium);
                    lithium = StartCoroutine(BarAnimation(_lithiumBar, StoreResourcesAmount.lithium.currentValue));
                    break;
                case "titanium":
                    if (titanium != null)
                        StopCoroutine(titanium);
                    titanium = StartCoroutine(BarAnimation(_titaniumBar, StoreResourcesAmount.titanium.currentValue));
                    break;
                case "silicon":
                    if (silicon != null)
                        StopCoroutine(silicon);
                    silicon = StartCoroutine(BarAnimation(_silliconBar, StoreResourcesAmount.silicon.currentValue));
                    break;
            }
        }
        private void HandleBarAnimationForXpBar()
        {
            StartCoroutine(BarAnimation(_xpBar, StorePlayerStats.currentXp));
        }
        private void HandleBarAnimationForLevelUp(int level)
        {
            StartCoroutine(BarAnimation(_xpBar, StorePlayerStats.currentXp));          
            _xpBar.MaxValue = StorePlayerStats.levelsThresholds.levelsThresholds[StorePlayerStats.currentLevel];
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