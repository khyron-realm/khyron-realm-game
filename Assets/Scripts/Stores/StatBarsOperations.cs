using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;
using Panels;
using Networking.Headquarters;
using PlayerDataUpdate;
using TMPro;


namespace Manager.Store
{
    public class StatBarsOperations : MonoBehaviour
    {
        #region "Input Data"
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private ProgressBar _xpBar;
        [SerializeField] private ProgressBar _energyBar;
        [SerializeField] private ProgressBar _silliconBar;
        [SerializeField] private ProgressBar _lithiumBar;
        [SerializeField] private ProgressBar _titaniumBar;
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
        }


        #region "Initialisation"
        private void InitAll()
        {
            InitPlayerLevelAndExperience();

            InitMaximumLevels();
            InitCurrentLevels();
        }

        private void InitPlayerLevelAndExperience()
        {
            if(_level != null)
                _level.text = HeadquartersManager.Player.Level.ToString();
            if(_xpBar != null)
            {
                _xpBar.MaxValue = (int)LevelMethods.Experience(HeadquartersManager.Player.Level);
                _xpBar.CurrentValue = (int)HeadquartersManager.Player.Experience;
            }           
        }
        private void InitCurrentLevels()
        {
            if(_energyBar != null)
                _energyBar.CurrentValue = (int)HeadquartersManager.Player.Energy;
            if(_silliconBar != null)
                _silliconBar.CurrentValue = (int)HeadquartersManager.Player.Resources[0].Count;
            if(_lithiumBar != null)
                _lithiumBar.CurrentValue = (int)HeadquartersManager.Player.Resources[1].Count;
            if(_titaniumBar != null)
                _titaniumBar.CurrentValue = (int)HeadquartersManager.Player.Resources[2].Count;
        }
        private void InitMaximumLevels()
        {
            if (_energyBar != null)
                _energyBar.MaxValue = (int)LevelMethods.MaxEnergyCount(HeadquartersManager.Player.Level);
            if (_silliconBar != null)
                _silliconBar.MaxValue = (int)LevelMethods.MaxResourcesAmount(HeadquartersManager.Player.Level)[0];
            if (_lithiumBar != null)
                _lithiumBar.MaxValue = (int)LevelMethods.MaxResourcesAmount(HeadquartersManager.Player.Level)[1];
            if (_titaniumBar != null)
                _titaniumBar.MaxValue = (int)LevelMethods.MaxResourcesAmount(HeadquartersManager.Player.Level)[2];
        }
        #endregion


        #region "Resources Update"
        private void EnergyUpdate(byte tag)
        {
            if (energy != null)
                StopCoroutine(energy);
            if (_energyBar != null)
                energy = StartCoroutine(BarAnimation(_energyBar, (int)HeadquartersManager.Player.Energy));
        }
        private void ResourcesUpdate(byte tag)
        {
            if (lithium != null)
                StopCoroutine(lithium);
            if (_lithiumBar != null)
                lithium = StartCoroutine(BarAnimation(_lithiumBar, (int)HeadquartersManager.Player.Resources[1].Count));

            if (titanium != null)
                StopCoroutine(titanium);
            if (_titaniumBar != null)
                titanium = StartCoroutine(BarAnimation(_titaniumBar, (int)HeadquartersManager.Player.Resources[2].Count));

            if (silicon != null)
                StopCoroutine(silicon);
            if (_silliconBar != null)
                silicon = StartCoroutine(BarAnimation(_silliconBar, (int)HeadquartersManager.Player.Resources[0].Count));
        }
        #endregion


        #region "Level update"
        private void ExperienceUpdate(byte tag)
        {
            if (_xpBar != null)
                StartCoroutine(BarAnimation(_xpBar, (int)HeadquartersManager.Player.Experience));
        }
        private void LevelUpdate(byte tag)
        {
            if (_level != null)
                _level.text = HeadquartersManager.Player.Level.ToString();

            if (_xpBar != null)
            {
                StartCoroutine(BarAnimation(_xpBar, (int)HeadquartersManager.Player.Experience));
                _xpBar.MaxValue = (int)LevelMethods.Experience(HeadquartersManager.Player.Level);
            }           
        }
        #endregion


        public static IEnumerator BarAnimation(ProgressBar bar, int endGoal)
        {
            float temp = 0f;
            while (temp < 1)
            {
                temp += Time.deltaTime * 0.3f;             
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