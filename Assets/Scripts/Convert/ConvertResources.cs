using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Store;
using CountDown;
using Networking.Game;


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

            _timer.TimeTextState(false);
        }

        public void Convert()
        {
            UnlimitedPlayerManager.ConversionRequest();
        }


        private void ConversionAccepted(long time)
        {
            print("Converting accepted");

            DateTime finalTime = DateTime.FromBinary(time);
            DateTime now = DateTime.Now;

            int timeRemained = Mathf.Abs(now.Second - finalTime.Second);

            timeRemained += 3600;

            if (ResourcesOperations.PayResources(10, 10, 10))
            {
                _timer.TimeTextState(true);
                _timer.AddTime(timeRemained);
                _button.enabled = false;

                StartCoroutine(RunConversion());
            }
        }


        private void CancelConversionAccepted()
        {

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