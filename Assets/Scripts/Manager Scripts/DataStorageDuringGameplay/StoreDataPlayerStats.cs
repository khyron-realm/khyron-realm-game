using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Store
{
    public class StoreDataPlayerStats : MonoBehaviour
    {
        [SerializeField] private LevelsThresholds _levelsThresholds;

        public static int currentLevel;
        public static int currentXp;
        public static LevelsThresholds levelsThresholds;

        private void Awake()
        {
            levelsThresholds = _levelsThresholds;
            InitValuesOfStoredData();
        }

        private static void InitValuesOfStoredData()
        {
            currentLevel = 1;
            currentXp = 0;
        }
    }
}