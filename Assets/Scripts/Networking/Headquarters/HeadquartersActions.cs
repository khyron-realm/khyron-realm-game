using System;
using UnityEngine;

namespace Networking.Headquarters
{
    public class HeadquartersActions : MonoBehaviour
    {
        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += PlayerDataReceived;
            HeadquartersManager.OnPlayerDataUnavailable += PlayerDataUnavailable;
            HeadquartersManager.OnGameDataReceived += GameDataReceived;
            HeadquartersManager.OnGameDataUnavailable += GameDataUnavailable;
            HeadquartersManager.OnConversionError += ConversionError;
            HeadquartersManager.OnFinishConversionError += FinishConversionError;
            HeadquartersManager.OnUpgradingError += UpgradingError;
            HeadquartersManager.OnFinishUpgradingError += FinishUpgradingError;
            HeadquartersManager.OnBuildingError += BuildingError;
            HeadquartersManager.OnFinishBuildingError += FinishBuildingError;
            HeadquartersManager.OnCancelBuildingError += CancelBuildingError;
        }

        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataReceived -= PlayerDataReceived;
            HeadquartersManager.OnPlayerDataUnavailable -= PlayerDataUnavailable;
            HeadquartersManager.OnGameDataReceived -= GameDataReceived;
            HeadquartersManager.OnGameDataUnavailable -= GameDataUnavailable;
            HeadquartersManager.OnConversionError -= ConversionError;
            HeadquartersManager.OnFinishConversionError -= FinishConversionError;
            HeadquartersManager.OnUpgradingError -= UpgradingError;
            HeadquartersManager.OnFinishUpgradingError -= FinishUpgradingError;
            HeadquartersManager.OnBuildingError -= BuildingError;
            HeadquartersManager.OnFinishUpgradingError -= FinishUpgradingError;
            HeadquartersManager.OnFinishBuildingError -= FinishBuildingError;
        }
        
        #region ServerRequests

        public void GetPlayerData()
        {
            HeadquartersManager.PlayerDataRequest();
        }

        public void GetGameData()
        {
            // 0 for the latest version and [version > 0] for checking if the version is up to date
            ushort version = 0;
            HeadquartersManager.GameDataRequest(version);
        }
        
        public void ConvertResources()
        {
            DateTime startTime = DateTime.Now;
            Resource[] newResources = new Resource[] { };
            HeadquartersManager.ConversionRequest(startTime, newResources);
        }

        public void FinishConvertResources()
        {
            uint newEnergy = 0;
            HeadquartersManager.FinishConversionRequest(newEnergy);
        }
        
        public void UpgradeRobot()
        {
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            uint newEnergy = 0;
            HeadquartersManager.UpgradingRequest(robotId, startTime, newEnergy);
        }

        public void FinishUpgradeRobot()
        {
            byte robotId = 0;
            Robot newRobot = new Robot();
            HeadquartersManager.FinishUpgradingRequest(robotId, newRobot);
        }

        public void BuildRobot()
        {
            // Generate queueNumber
            // if no build tasks in progress -> queue number = 0
            // else -> the highest task number + 1
            ushort queueNumber = 0;
            byte robotId = 0;
            // Generate startTime
            // if build task in progress -> starting time of the task
            // else -> 0
            DateTime startTime = DateTime.UtcNow;
            uint newEnergy = 0;
            HeadquartersManager.BuildingRequest(queueNumber, robotId, startTime.ToBinary(), newEnergy);
        }

        public void FinishBuildRobot()
        {
            // if (first time entry or from another screen) -> send only the last finished task
            // else -> send the current finished task
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.UtcNow;
            Robot newRobot = new Robot();
            HeadquartersManager.FinishBuildingRequest(queueNumber, robotId, startTime, newRobot);
        }
        
        public void CancelBuildRobot(bool inProgress)
        {
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.UtcNow;
            uint newEnergy = 0;
            HeadquartersManager.CancelBuildingRequest(queueNumber, robotId, startTime, newEnergy, inProgress);
        }

        #endregion

        #region ProcessServerResponse

        private void PlayerDataReceived()
        {
            Debug.Log("Player data received");
        }
        
        private void PlayerDataUnavailable()
        {
            Debug.Log("Player data unavailable");
        }
        
        private void GameDataReceived()
        {
            Debug.Log("Game data received");
        }
        
        private void GameDataUnavailable()
        {
            Debug.Log("Game data unavailable");
        }

        private void ConversionError(byte errorId)
        {
            // 0 - add task failed 
            // 1 - set new data failed
            Debug.Log("Resource conversion error");
        }
        
        private void FinishConversionError(byte errorId)
        {
            // 0 - remove task failed 
            // 1 - set new data failed
            Debug.Log("Finish conversion error");
        }

        private void UpgradingError(byte errorId)
        {
            // 0 - add task failed 
            // 1 - set new data failed
            Debug.Log("Robot upgrade error");
        }

        private void FinishUpgradingError(byte errorId)
        {
            // 0 - remove task failed
            // 1 - set new data failed
            Debug.Log("Finish robot upgrade error");
        }
        
        private void BuildingError(byte errorId)
        {
            // 0 - add task failed 
            // 1 - set new data failed
            Debug.Log("Robot build error");
        }

        private void FinishBuildingError(byte errorId)
        {
            // 0 - remove task failed
            // 1 - update next task failed
            // 2 - set new data failed
            Debug.Log("Finish robot build error");
        }
        
        private void CancelBuildingError(byte errorId)
        {
            // 0 - remove task failed
            // 1 - update next task failed
            // 2 - set new data failed
            Debug.Log("Cancel robot build error");
        }

        #endregion
    }
}