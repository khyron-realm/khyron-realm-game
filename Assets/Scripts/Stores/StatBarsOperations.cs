using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panels;
using Networking.Headquarters;
using Networking.Levels;
using PlayerDataUpdate;


namespace Manager.Store
{
    public class StatBarsOperations : MonoBehaviour
    {
        #region "Input Data"
        [SerializeField] private ProgressBar _xpBar;
        [SerializeField] private ProgressBar _energyBar;
        [SerializeField] private ProgressBar _lithiumBar;
        [SerializeField] private ProgressBar _titaniumBar;
        [SerializeField] private ProgressBar _silliconBar;
        #endregion

        #region "Coroutines"
        private Coroutine energy;
        private Coroutine lithium;
        private Coroutine titanium;
        private Coroutine silicon;
        #endregion

        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += InitAll;

            PlayerDataOperations.OnEnergyModified += EnergyUpdate;
            PlayerDataOperations.OnResourcesModified += ResourcesUpdate;
            PlayerDataOperations.OnExperienceUpdated += ExperienceUpdate;
            PlayerDataOperations.OnLevelUpdated += LevelUpdate;


            _xpBar.MaxValue = StorePlayerStats.levelsThresholds.levelsThresholds[0];
            _xpBar.CurrentValue = StorePlayerStats.currentXp;
        }

        #region "Initialisation"
        private void InitAll()
        {
            InitMaximumLevels();
            InitCurrentLevels();
        }
        private void InitCurrentLevels()
        {
            _energyBar.CurrentValue = (int)HeadquartersManager.Player.Energy;
            _silliconBar.CurrentValue = (int)HeadquartersManager.Player.Resources[0].Count;
            _lithiumBar.CurrentValue = (int)HeadquartersManager.Player.Resources[1].Count;
            _titaniumBar.CurrentValue = (int)HeadquartersManager.Player.Resources[2].Count;
        }
        private void InitMaximumLevels()
        {
            _energyBar.MaxValue = (int)LevelMethods.MaxEnergyCount(HeadquartersManager.Player.Level);
            _silliconBar.MaxValue = (int)LevelMethods.MaxResourcesAmount(HeadquartersManager.Player.Level)[0];
            _lithiumBar.MaxValue = (int)LevelMethods.MaxResourcesAmount(HeadquartersManager.Player.Level)[1];
            _titaniumBar.MaxValue = (int)LevelMethods.MaxResourcesAmount(HeadquartersManager.Player.Level)[2];
        }
        #endregion


        #region "Resources Update"
        private void EnergyUpdate(byte tag)
        {
            if (energy != null)
                StopCoroutine(energy);
            energy = StartCoroutine(BarAnimation(_energyBar, (int)HeadquartersManager.Player.Energy));
        }
        private void ResourcesUpdate(byte tag)
        {
            if (lithium != null)
                StopCoroutine(lithium);
            lithium = StartCoroutine(BarAnimation(_lithiumBar, (int)HeadquartersManager.Player.Resources[1].Count));

            if (titanium != null)
                StopCoroutine(titanium);
            titanium = StartCoroutine(BarAnimation(_titaniumBar, (int)HeadquartersManager.Player.Resources[2].Count));

            if (silicon != null)
                StopCoroutine(silicon);
            silicon = StartCoroutine(BarAnimation(_silliconBar, (int)HeadquartersManager.Player.Resources[0].Count));
        }
        #endregion


        #region "Level update"
        private void ExperienceUpdate(byte tag)
        {
            StartCoroutine(BarAnimation(_xpBar, (int)HeadquartersManager.Player.Experience));
        }
        private void LevelUpdate(byte tag)
        {
            StartCoroutine(BarAnimation(_xpBar, (int)HeadquartersManager.Player.Experience));
            _xpBar.MaxValue = StorePlayerStats.levelsThresholds.levelsThresholds[StorePlayerStats.currentLevel];
        }
        #endregion


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
            HeadquartersManager.OnPlayerDataReceived -= InitAll;

            PlayerDataOperations.OnEnergyModified -= EnergyUpdate;
            PlayerDataOperations.OnResourcesModified -= ResourcesUpdate;
            PlayerDataOperations.OnExperienceUpdated -= ExperienceUpdate;
            PlayerDataOperations.OnLevelUpdated -= LevelUpdate;
        }
    }
}