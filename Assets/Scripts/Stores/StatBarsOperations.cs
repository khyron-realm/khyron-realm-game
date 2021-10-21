using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panels;
using Save;
using Networking.Headquarters;


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

            HeadquartersManager.OnEnergyUpdate += EnergyUpdate;
            HeadquartersManager.OnResourcesUpdate += ResourcesUpdate;
            HeadquartersManager.OnExperienceUpdate += ExperienceUpdate;
            HeadquartersManager.OnLevelUpdate += LevelUpdate;



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
            _energyBar.CurrentValue = (int)HeadquartersManager.player.Energy;
            _silliconBar.CurrentValue = (int)HeadquartersManager.player.Resources[0].Count;
            _lithiumBar.CurrentValue = (int)HeadquartersManager.player.Resources[1].Count;
            _titaniumBar.CurrentValue = (int)HeadquartersManager.player.Resources[2].Count;
        }
        private void InitMaximumLevels()
        {
            _energyBar.MaxValue = (int)GameDataValues.MaxEnergy;
            _silliconBar.MaxValue = (int)GameDataValues.Resources[0].MaxCount;
            _lithiumBar.MaxValue = (int)GameDataValues.Resources[1].MaxCount;
            _titaniumBar.MaxValue = (int)GameDataValues.Resources[2].MaxCount;           
        }
        #endregion


        #region "Resources Update"
        private void EnergyUpdate()
        {
            if (energy != null)
                StopCoroutine(energy);
            energy = StartCoroutine(BarAnimation(_energyBar, (int)HeadquartersManager.player.Energy));
        }
        private void ResourcesUpdate()
        {
            if (lithium != null)
                StopCoroutine(lithium);
            lithium = StartCoroutine(BarAnimation(_lithiumBar, (int)HeadquartersManager.player.Resources[1].Count));

            if (titanium != null)
                StopCoroutine(titanium);
            titanium = StartCoroutine(BarAnimation(_titaniumBar, (int)HeadquartersManager.player.Resources[2].Count));

            if (silicon != null)
                StopCoroutine(silicon);
            silicon = StartCoroutine(BarAnimation(_silliconBar, (int)HeadquartersManager.player.Resources[0].Count));
        }
        #endregion


        #region "Level update"
        private void ExperienceUpdate()
        {
            StartCoroutine(BarAnimation(_xpBar, (int)HeadquartersManager.player.Experience));
        }
        private void LevelUpdate()
        {
            StartCoroutine(BarAnimation(_xpBar, (int)HeadquartersManager.player.Experience));
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

            HeadquartersManager.OnEnergyUpdate -= EnergyUpdate;
            HeadquartersManager.OnResourcesUpdate -= ResourcesUpdate;
            HeadquartersManager.OnExperienceUpdate -= ExperienceUpdate;
            HeadquartersManager.OnLevelUpdate -= LevelUpdate;
        }
    }
}