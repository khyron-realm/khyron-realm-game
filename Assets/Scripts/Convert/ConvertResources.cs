using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Store;
using CountDown;
using Networking.Game;
using Networking.GameElements;


namespace Manager.Convert
{
    public class ConvertResources : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Timer _timer;
        [SerializeField] private Button _button;
        #endregion

        private void Awake()
        {
            UnlimitedPlayerManager.OnCancelConversionAccepted += CancelConversionAccepted;

            UnlimitedPlayerManager.OnConversionAccepted += ConversionAccepted;
            UnlimitedPlayerManager.OnConversionRejected += ConversionRejected;

            ManageTasks.OnConvertingWorking += CheckForUpgradesInProgress;

            _timer.TimeTextState(false);
        }


        public void CheckForUpgradesInProgress(BuildTask task)
        {
            ConversionAccepted(task.EndTime);    
        }


        public void Convert()
        {
            UnlimitedPlayerManager.ConversionRequest(DateTime.Now);
        }

        private void ConversionAccepted()
        {
            print("Converting accepted");
            
            int timeRemained = 0;                           // Get task time

            timeRemained += 3600;
            
            DateTime now = DateTime.Now;

            int timeRemained = (int)finalTime.Subtract(now).TotalSeconds;

            if (ResourcesOperations.PayResources(10, 10, 10))
            {
                _timer.TimeTextState(true);
                _timer.AddTime(timeRemained);
                _button.enabled = false;

                StartCoroutine(RunConversion());
            }
        }


        private void ConversionRejected(byte errorId)
        {
            print("Conversion rejected");
        }


        private IEnumerator RunConversion()
        {
            int temp = 0;
            while (temp < _timer.TotalTime)
            {
                temp += 1;
                yield return _timer.ActivateTimer();
            }

            UnlimitedPlayerManager.CancelConversionRequest();
        }


        private void CancelConversionAccepted()
        {
            print("Conversion ended");
            _button.enabled = true;
            _timer.TimeTextState(false);
            ResourcesOperations.Add(StoreResourcesAmount.energy, 100);

        }


        private void OnDestroy()
        {
            UnlimitedPlayerManager.OnCancelConversionAccepted -= CancelConversionAccepted;
            UnlimitedPlayerManager.OnConversionAccepted -= ConversionAccepted;

            UnlimitedPlayerManager.OnConversionRejected -= ConversionRejected;
        }
    }
}