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
            StorePlayerStats.currentXp += xp;
            CheckIfLevelUp();    
        }

        private void CheckIfLevelUp()
        {
            if (StorePlayerStats.currentLevel < StorePlayerStats.levelsThresholds.levelsThresholds.Count && StorePlayerStats.currentXp > StorePlayerStats.levelsThresholds.levelsThresholds[StorePlayerStats.currentLevel - 1])
            {
                StorePlayerStats.currentXp -= StorePlayerStats.levelsThresholds.levelsThresholds[StorePlayerStats.currentLevel - 1];
                StorePlayerStats.currentLevel++;

                OnLevelUp?.Invoke(StorePlayerStats.currentLevel);
            }
            else
            {
                OnXpAdded?.Invoke();
            }
        }
    }
}