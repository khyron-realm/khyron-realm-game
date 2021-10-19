using System;
using Networking.GameElements;
using UnityEngine;

namespace Networking.Game
{
    public class PlayerActions : MonoBehaviour
    {
        private void Awake()
        {
            UnlimitedPlayerManager.OnPlayerDataReceived += PlayerDataReceived;
            UnlimitedPlayerManager.OnPlayerDataUnavailable += PlayerDataUnavailable;
            UnlimitedPlayerManager.OnGameDataReceived += GameDataReceived;
            UnlimitedPlayerManager.OnGameDataUnavailable += GameDataUnavailable;
            UnlimitedPlayerManager.OnFinishConversionAccepted += FinishConversionAccepted;
            UnlimitedPlayerManager.OnConversionAccepted += ConversionAccepted;
            UnlimitedPlayerManager.OnConversionRejected += ConversionRejected;
            UnlimitedPlayerManager.OnFinishUpgradingAccepted += FinishUpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingAccepted += UpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingRejected += UpgradingRejected;
            UnlimitedPlayerManager.OnFinishBuildingAccepted += FinishBuildingAccepted;
            UnlimitedPlayerManager.OnBuildingAccepted += BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected += BuildingRejected;
            UnlimitedPlayerManager.OnLevelUpdate += LevelUpdate;
            UnlimitedPlayerManager.OnExperienceUpdate += ExperienceUpdate;
            UnlimitedPlayerManager.OnEnergyUpdate += EnergyUpdate;
            UnlimitedPlayerManager.OnResourcesUpdate += ResourcesUpdate;
            UnlimitedPlayerManager.OnRobotsUpdate += RobotsUpdate;
        }

        private void OnDestroy()
        {
            UnlimitedPlayerManager.OnPlayerDataReceived -= PlayerDataReceived;
            UnlimitedPlayerManager.OnPlayerDataUnavailable -= PlayerDataUnavailable;
            UnlimitedPlayerManager.OnGameDataReceived -= GameDataReceived;
            UnlimitedPlayerManager.OnGameDataUnavailable -= GameDataUnavailable;
            UnlimitedPlayerManager.OnFinishConversionAccepted -= FinishConversionAccepted;
            UnlimitedPlayerManager.OnConversionAccepted -= ConversionAccepted;
            UnlimitedPlayerManager.OnConversionRejected -= ConversionRejected;
            UnlimitedPlayerManager.OnFinishUpgradingAccepted -= FinishUpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingAccepted -= UpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingRejected -= UpgradingRejected;
            UnlimitedPlayerManager.OnFinishBuildingAccepted -= FinishBuildingAccepted;
            UnlimitedPlayerManager.OnBuildingAccepted -= BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected -= BuildingRejected;
            UnlimitedPlayerManager.OnLevelUpdate -= LevelUpdate;
            UnlimitedPlayerManager.OnExperienceUpdate -= ExperienceUpdate;
            UnlimitedPlayerManager.OnEnergyUpdate -= EnergyUpdate;
            UnlimitedPlayerManager.OnResourcesUpdate -= ResourcesUpdate;
            UnlimitedPlayerManager.OnRobotsUpdate -= RobotsUpdate;
        }
        
        #region ServerRequests

        public void GetPlayerData()
        {
            UnlimitedPlayerManager.PlayerDataRequest();
        }

        public void GetGameData()
        {
            UnlimitedPlayerManager.GameDataRequest();
        }
        
        public void ConvertResources()
        {
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.ConversionRequest(startTime);
        }

        public void FinishConvertResources()
        {
            UnlimitedPlayerManager.FinishConversionRequest();
        }
        
        public void UpgradeRobot()
        {
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.UpgradingRequest(robotId, startTime);
        }

        public void FinishUpgradeRobot()
        {
            byte robotId = 0;
            UnlimitedPlayerManager.FinishUpgradingRequest(robotId);
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
            UnlimitedPlayerManager.BuildingRequest(queueNumber, robotId, startTime);
        }

        public void FinishBuildRobot()
        {
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.FinishBuildingRequest(queueNumber, robotId, startTime, true);
        }
        
        public void CancelInProgressBuildRobot() 
        {
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.FinishBuildingRequest(queueNumber, robotId, startTime, false);
        }
        
        public void CancelOnHoldBuildRobot()
        {
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.FinishBuildingRequest(queueNumber, robotId, startTime, false, false);
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