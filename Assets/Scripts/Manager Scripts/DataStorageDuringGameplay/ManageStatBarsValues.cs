using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Store
{
    public class ManageStatBarsValues : MonoBehaviour
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


        private void Awake()
        {
            GiveValuesToStatic();
            InitMaximumLevels();

            xpBar.MaxValue = StoreDataPlayerStats.levelsThresholds.levelsThresholds[0];

            ManageStatsOperations.OnXpAdded += HandleBarAnimationForXpBar;
            ManageStatsOperations.OnLevelUp += HandleBarAnimationForLevelUp;
            ManageResourcesOperations.OnResourcesModified += HandleBarAnimationForResources;
        }

        private void InitMaximumLevels()
        {
            energyBar.MaxValue = StoreDataResources.maximumLevel;
            lithiumBar.MaxValue = StoreDataResources.maximumLevel;
            titaniumBar.MaxValue = StoreDataResources.maximumLevel;
            silliconBar.MaxValue = StoreDataResources.maximumLevel;
        }
        private void GiveValuesToStatic()
        {
            xpBar = _xpBar;
            energyBar = _energyBar;
            lithiumBar = _lithiumBar;
            titaniumBar = _titaniumBar;
            silliconBar = _silliconBar;
        }


        private void HandleBarAnimationForResources(string resource)
        {
            switch(resource)
            {
                case "energy":
                    StartCoroutine(BarAnimation(energyBar, StoreDataResources.energyLevel, 0.6f));
                    break;
                case "lithium":
                    StartCoroutine(BarAnimation(lithiumBar, StoreDataResources.lithiumLevel, 0.6f));
                    break;
                case "titanium":
                    StartCoroutine(BarAnimation(titaniumBar, StoreDataResources.titaniumLevel, 0.6f));
                    break;
                case "silicon":
                    StartCoroutine(BarAnimation(silliconBar, StoreDataResources.silliconLevel, 0.6f));
                    break;
            }
        }
        private void HandleBarAnimationForXpBar()
        {
            StartCoroutine(BarAnimation(xpBar, StoreDataPlayerStats.currentXp, 0.6f));
        }
        private void HandleBarAnimationForLevelUp(int level)
        {
            StartCoroutine(BarAnimation(xpBar, StoreDataPlayerStats.currentXp, 2f));
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


        public static IEnumerator BarAnimation(ProgressBar bar, int endGoal, float time)
        {
            float temp = 0f;
            while (temp < time)
            {
                temp += Time.deltaTime;
                bar.CurrentValue = Mathf.RoundToInt(Mathf.Lerp(bar.CurrentValue, endGoal, temp));
                yield return null;
            }
        }
    }
}