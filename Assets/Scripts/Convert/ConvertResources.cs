using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unlimited_NetworkingServer_MiningGame.Game;
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
            uint[] resources = LevelMethods.ResourceConversionCost(HeadquartersManager.Player.Level);
            PlayerDataOperations.PayResources(-(int)resources[0], -(int)resources[1], -(int)resources[2], Tag);          
        }
        private void SendConvertRequest(byte tag)
        {
            if(Tag == tag)
            {
                HeadquartersManager.ConversionRequest(DateTime.UtcNow, HeadquartersManager.Player.Resources);
                ExecuteConversion(LevelMethods.ResourceConversionTime(HeadquartersManager.Player.Level) * 60);
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

            ExecuteConversion((LevelMethods.ResourceConversionTime(HeadquartersManager.Player.Level) * 60) - timeRemained, (LevelMethods.ResourceConversionTime(HeadquartersManager.Player.Level) * 60));    
        }


        private void ExecuteConversion(int time, int maxValue = 0)
        {
            _timer.TimeTextState(true);
            _timer.AddTime(time);

            if(maxValue == 0)
            {
                _timer.SetMaxValueForTime(time);
            }
            else
            {
                _timer.SetMaxValueForTime(maxValue);
            }
            
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