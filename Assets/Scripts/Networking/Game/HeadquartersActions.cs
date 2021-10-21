using System;
using Networking.GameElements;
using UnityEngine;

namespace Networking.Game
{
    public class HeadquartersActions : MonoBehaviour
    {
        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += PlayerDataReceived;
            HeadquartersManager.OnPlayerDataUnavailable += PlayerDataUnavailable;
            HeadquartersManager.OnGameDataReceived += GameDataReceived;
            HeadquartersManager.OnGameDataUnavailable += GameDataUnavailable;
            HeadquartersManager.OnFinishConversionAccepted += FinishConversionAccepted;
            HeadquartersManager.OnConversionAccepted += ConversionAccepted;
            HeadquartersManager.OnConversionRejected += ConversionRejected;
            HeadquartersManager.OnFinishUpgradingAccepted += FinishUpgradingAccepted;
            HeadquartersManager.OnUpgradingAccepted += UpgradingAccepted;
            HeadquartersManager.OnUpgradingRejected += UpgradingRejected;
            HeadquartersManager.OnFinishBuildingAccepted += FinishBuildingAccepted;
            HeadquartersManager.OnCancelBuildingAccepted += CancelBuildingAccepted;
            HeadquartersManager.OnBuildingAccepted += BuildingAccepted;
            HeadquartersManager.OnBuildingRejected += BuildingRejected;
            HeadquartersManager.OnLevelUpdate += LevelUpdate;
            HeadquartersManager.OnExperienceUpdate += ExperienceUpdate;
            HeadquartersManager.OnEnergyUpdate += EnergyUpdate;
            HeadquartersManager.OnResourcesUpdate += ResourcesUpdate;
            HeadquartersManager.OnRobotsUpdate += RobotsUpdate;
        }

        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataReceived -= PlayerDataReceived;
            HeadquartersManager.OnPlayerDataUnavailable -= PlayerDataUnavailable;
            HeadquartersManager.OnGameDataReceived -= GameDataReceived;
            HeadquartersManager.OnGameDataUnavailable -= GameDataUnavailable;
            HeadquartersManager.OnFinishConversionAccepted -= FinishConversionAccepted;
            HeadquartersManager.OnConversionAccepted -= ConversionAccepted;
            HeadquartersManager.OnConversionRejected -= ConversionRejected;
            HeadquartersManager.OnFinishUpgradingAccepted -= FinishUpgradingAccepted;
            HeadquartersManager.OnUpgradingAccepted -= UpgradingAccepted;
            HeadquartersManager.OnUpgradingRejected -= UpgradingRejected;
            HeadquartersManager.OnFinishBuildingAccepted -= FinishBuildingAccepted;
            HeadquartersManager.OnBuildingAccepted -= BuildingAccepted;
            HeadquartersManager.OnBuildingRejected -= BuildingRejected;
            HeadquartersManager.OnLevelUpdate -= LevelUpdate;
            HeadquartersManager.OnExperienceUpdate -= ExperienceUpdate;
            HeadquartersManager.OnEnergyUpdate -= EnergyUpdate;
            HeadquartersManager.OnResourcesUpdate -= ResourcesUpdate;
            HeadquartersManager.OnRobotsUpdate -= RobotsUpdate;
        }
        
        #region ServerRequests

        public void GetPlayerData()
        {
            HeadquartersManager.PlayerDataRequest();
        }

        public void GetGameData()
        {
            HeadquartersManager.GameDataRequest();
        }
        
        public void ConvertResources()
        {
            DateTime startTime = DateTime.Now;
            HeadquartersManager.ConversionRequest(startTime);
        }

        public void FinishConvertResources()
        {
            HeadquartersManager.FinishConversionRequest();
        }
        
        public void UpgradeRobot()
        {
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            HeadquartersManager.UpgradingRequest(robotId, startTime);
        }

        public void FinishUpgradeRobot()
        {
            byte robotId = 0;
            HeadquartersManager.FinishUpgradingRequest(robotId);
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
            HeadquartersManager.BuildingRequest(queueNumber, robotId, startTime);
        }

        public void FinishBuildRobot()
        {
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            HeadquartersManager.FinishBuildingRequest(queueNumber, robotId, startTime, true);
        }
        
        public void CancelInProgressBuildRobot()
        {
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            HeadquartersManager.FinishBuildingRequest(queueNumber, robotId, startTime, false);
        }
        
        public void CancelOnHoldBuildRobot()
        {
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            HeadquartersManager.FinishBuildingRequest(queueNumber, robotId, startTime, false, false);
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

        private void FinishConversionAccepted()
        {
            Debug.Log("Cancel conversion accepted");
        }
        
        private void ConversionAccepted()
        {
            Debug.Log("Conversion accepted");
        }
        
        private void ConversionRejected(byte errorId)
        {
            // 1 - task already exists (conversion already in progress)
            // 2 - not enough resources
            Debug.Log("Conversion rejected");
        }

        private void FinishUpgradingAccepted()
        {
            Debug.Log("Cancel upgrading accepted");
        }

        private void UpgradingAccepted()
        {
            Debug.Log("Upgrading accepted");
        }

        private void UpgradingRejected(byte errorId)
        {
            // 1 - task already exists (upgrade already in progress)
            // 2 - not enough energy
            Debug.Log("Upgrading rejected");
        }

        private void FinishBuildingAccepted()
        {
            Debug.Log("Cancel building accepted");
        }
        
        private void CancelBuildingAccepted(byte taskType)
        {
            Debug.Log("Cancel building accepted");
        }
        
        private void BuildingAccepted()
        {
            Debug.Log("Building accepted");
        }

        private void BuildingRejected(byte errorId)
        {
            // 1 - task already exists (build of robotId and queueNumber already in progress)
            // 2 - not enough energy
            Debug.Log("Building rejected");
        }
        
        private void LevelUpdate()
        {
            Debug.Log("Level updated");
        }
        
        private void ExperienceUpdate()
        {
            Debug.Log("Experience updated");
        }
        
        private void EnergyUpdate()
        {
            Debug.Log("Energy updated");
        }
        
        private void ResourcesUpdate()
        {
            Debug.Log("Resources updated");
        }
        
        private void RobotsUpdate()
        {
            Debug.Log("Robots updated");
        }
        
        #endregion
    }
}