using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Manager.Store
{
    public class StatsOperations : MonoBehaviour
    {
        public static event Action<int> OnLevelUp;
        public static event Action OnXpAdded;

        public void AddXP(int xp)
        {
            StoreDataPlayerStats.currentXp += xp;
            CheckIfLevelUp();    
        }

        private void CheckIfLevelUp()
        {
            if (StoreDataPlayerStats.currentLevel < StoreDataPlayerStats.levelsThresholds.levelsThresholds.Count && StoreDataPlayerStats.currentXp > StoreDataPlayerStats.levelsThresholds.levelsThresholds[StoreDataPlayerStats.currentLevel - 1])
            {
                StoreDataPlayerStats.currentXp -= StoreDataPlayerStats.levelsThresholds.levelsThresholds[StoreDataPlayerStats.currentLevel - 1];
                StoreDataPlayerStats.currentLevel++;

                OnLevelUp?.Invoke(StoreDataPlayerStats.currentLevel);
            }
            else
            {
                OnXpAdded?.Invoke();
            }
        }
    }
}