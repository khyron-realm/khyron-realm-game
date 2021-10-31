using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;
using UnityEngine.UI;
using Networking.Headquarters;


namespace Manager.Convert
{
    public class ConversionInfo : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Text _silliconAmount;
        [SerializeField] private Text _lithiumAmount;
        [SerializeField] private Text _titaniumAmount;

        [SerializeField] private Text _energyRecevied;
        [SerializeField] private Text _timeOfExecution;
        #endregion


        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += DisplayPrices;
        }


        private void DisplayPrices()
        {
            _silliconAmount.text = LevelMethods.ResourceConversionCost(HeadquartersManager.Player.Level)[0].ToString();
            _lithiumAmount.text = LevelMethods.ResourceConversionCost(HeadquartersManager.Player.Level)[1].ToString();
            _titaniumAmount.text = LevelMethods.ResourceConversionCost(HeadquartersManager.Player.Level)[2].ToString();

            _energyRecevied.text = LevelMethods.ResourceConversionGeneration(HeadquartersManager.Player.Level).ToString();
            _timeOfExecution.text = LevelMethods.ResourceConversionTime(HeadquartersManager.Player.Level).ToString();
        }


        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataReceived -= DisplayPrices;
        }
    }
}