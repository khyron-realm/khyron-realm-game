using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CountDown;
using Levels;
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

        public static event Action OnConversionStarted;
        public static event Action OnConversionEnded;

        private void Awake()
        {
            HeadquartersManager.OnConversionError += ConversionError;

            PlayerDataOperations.OnResourcesModified += SendConvertRequest;
            PlayerDataOperations.OnEnergyModified += ConversionEnded;

            ManageTasks.OnConvertingWorking += CheckForConversionInProgress;

            _timer.TimeTextState(false);
        }


        /// <summary>
        /// Executes if conversion is in progress 
        /// </summary>
        /// <param name="task"> the conversion task</param>
        private void CheckForConversionInProgress(BuildTask task)
        {
            DateTime startTime = DateTime.FromBinary(task.StartTime);
            DateTime now = DateTime.UtcNow;

            int timePassed = (int)now.Subtract(startTime).TotalSeconds;

            if (((LevelMethods.ResourceConversionTime(HeadquartersManager.Player.Level) * 60) - timePassed) < 1)
            {
                PlayerDataOperations.PayEnergy((int)LevelMethods.ResourceConversionGeneration(HeadquartersManager.Player.Level), OperationsTags.CONVERTING_RESOURCES);
                ConversionEnded(OperationsTags.CONVERTING_RESOURCES);
            }
            else
            {
                ExecuteConversion((LevelMethods.ResourceConversionTime(HeadquartersManager.Player.Level) * 60) - timePassed, (LevelMethods.ResourceConversionTime(HeadquartersManager.Player.Level) * 60));
            }           
        }


        public void Convert()
        {
            uint[] resources = LevelMethods.ResourceConversionCost(HeadquartersManager.Player.Level);
            PlayerDataOperations.PayResources(-(int)resources[0], -(int)resources[1], -(int)resources[2], OperationsTags.CONVERTING_RESOURCES);          
        }
        private void SendConvertRequest(byte tag)
        {
            if (OperationsTags.CONVERTING_RESOURCES != tag) return;
            
            HeadquartersManager.ConversionRequest(DateTime.UtcNow, HeadquartersManager.Player.Resources);
            ExecuteConversion(LevelMethods.ResourceConversionTime(HeadquartersManager.Player.Level) * 60);                      
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
            OnConversionStarted?.Invoke();

            int temp = 0;
            while (temp < time)
            {
                temp += 1;
                yield return _timer.ActivateTimer();
            }

            PlayerDataOperations.PayEnergy((int)LevelMethods.ResourceConversionGeneration(HeadquartersManager.Player.Level), OperationsTags.CONVERTING_RESOURCES);         
        }


        private void ConversionEnded(byte tag)
        {
            if (OperationsTags.CONVERTING_RESOURCES != tag) return;
            
            PlayerDataOperations.ExperienceUpdate(10, 0);
            HeadquartersManager.FinishConversionRequest(HeadquartersManager.Player.Energy, HeadquartersManager.Player.Experience);
                
            _button.enabled = true;

            _timer.SetMaxValueForTime(1);
            _timer.TimeTextState(false);

            OnConversionEnded?.Invoke();                     
        }


        private void ConversionError(byte errorId)
        {
            print("Conversion rejected");
        }


        private void OnDestroy()
        {
            HeadquartersManager.OnConversionError -= ConversionError;

            PlayerDataOperations.OnResourcesModified -= SendConvertRequest;
            PlayerDataOperations.OnEnergyModified -= ConversionEnded;

            ManageTasks.OnConvertingWorking -= CheckForConversionInProgress;
        }
    }
}