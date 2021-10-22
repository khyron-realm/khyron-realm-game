using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Save;
using CountDown;
using Networking.Headquarters;
using PlayerDataUpdate;


namespace Manager.Convert
{
    public class ConvertResources : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Timer _timer;
        [SerializeField] private Button _button;
        #endregion

        private static byte Tag = 0;

        private void Awake()
        {
            HeadquartersManager.OnConversionError += ConversionError;
            PlayerDataOperations.OnResourcesModified += SendConvertRequest;
            PlayerDataOperations.OnEnergyModified += ConvertEnded;
            ManageTasks.OnConvertingWorking += CheckForConversionInProgress;

            _timer.TimeTextState(false);
        }


        public void Convert()
        {
            PlayerDataOperations.PayResources(-GameDataValues.Resources[0].ConversionRate, -GameDataValues.Resources[1].ConversionRate, -GameDataValues.Resources[2].ConversionRate, Tag);          
        }
        private void SendConvertRequest(byte tag)
        {
            if(Tag == tag)
            {
                HeadquartersManager.ConversionRequest(DateTime.UtcNow, HeadquartersManager.Player.Resources);
                ExecuteConversion(GameDataValues.ConversionTime * 60);
            }          
        }


        /// <summary>
        /// Executes if conversion is in progress 
        /// </summary>
        /// <param name="task"> the conversion task</param>
        private void CheckForConversionInProgress(BuildTask task)
        {          
            DateTime startTime = DateTime.FromBinary(task.StartTime);
            DateTime now = DateTime.UtcNow;

            int timeRemained = (int)now.Subtract(startTime).TotalSeconds;

            ExecuteConversion((GameDataValues.ConversionTime * 60) - timeRemained);    
        }


        private void ExecuteConversion(int time)
        {
            _timer.TimeTextState(true);
            _timer.AddTime(time);
            _button.enabled = false;

            StartCoroutine(RunConversion(time));
        }
        private IEnumerator RunConversion(int time)
        {
            int temp = 0;
            while (temp < time)
            {
                temp += 1;
                yield return _timer.ActivateTimer();
            }

            PlayerDataOperations.PayEnergy(10000, Tag);         
        }


        private void ConvertEnded(byte tag)
        {
            if(Tag == tag)
            {
                HeadquartersManager.FinishConversionRequest(HeadquartersManager.Player.Energy);
                _button.enabled = true;
                _timer.TimeTextState(false);
            }           
        }


        private void ConversionError(byte errorId)
        {
            print("Conversion rejected");
        }

        private void OnDestroy()
        {
            HeadquartersManager.OnConversionError -= ConversionError;
            PlayerDataOperations.OnResourcesModified -= SendConvertRequest;
            ManageTasks.OnConvertingWorking -= CheckForConversionInProgress;
        }
    }
}