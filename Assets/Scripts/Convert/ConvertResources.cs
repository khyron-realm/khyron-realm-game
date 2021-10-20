using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Save;
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
            HeadquartersManager.OnConversionAccepted += ConversionAccepted;
            HeadquartersManager.OnConversionRejected += ConversionRejected;

            HeadquartersManager.OnFinishConversionAccepted += FinishConversionAccepted;

            ManageTasks.OnConvertingWorking += CheckForConversionInProgress;

            _timer.TimeTextState(false);
        }

        public void Convert()
        {
            HeadquartersManager.ConversionRequest(DateTime.UtcNow);
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


        #region "Conversion handlers"
        private void ConversionAccepted()
        {
            ExecuteConversion(GameDataValues.ConversionTime * 60);
        }
        private void ConversionRejected(byte errorId)
        {
            print("Conversion rejected");
        }
        #endregion


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

            HeadquartersManager.FinishConversionRequest();
        }


        private void FinishConversionAccepted()
        {
            _button.enabled = true;
            _timer.TimeTextState(false);
        }


        private void OnDestroy()
        {
            HeadquartersManager.OnFinishConversionAccepted -= FinishConversionAccepted;
            HeadquartersManager.OnConversionAccepted -= ConversionAccepted;

            HeadquartersManager.OnConversionRejected -= ConversionRejected;

            ManageTasks.OnConvertingWorking -= CheckForConversionInProgress;
        }
    }
}