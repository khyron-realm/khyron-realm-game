using System;
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
            UnlimitedPlayerManager.OnCancelConversionAccepted += CancelConversionAccepted;
            UnlimitedPlayerManager.OnConversionAccepted += ConversionAccepted;
            UnlimitedPlayerManager.OnConversionRejected += ConversionRejected;
            UnlimitedPlayerManager.OnCancelUpgradingAccepted += CancelUpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingAccepted += UpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingRejected += UpgradingRejected;
            UnlimitedPlayerManager.OnCancelBuildingAccepted += CancelBuildingAccepted;
            UnlimitedPlayerManager.OnBuildingAccepted += BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected += BuildingRejected;
        }

        private void OnDestroy()
        {
            UnlimitedPlayerManager.OnPlayerDataReceived -= PlayerDataReceived;
            UnlimitedPlayerManager.OnPlayerDataUnavailable -= PlayerDataUnavailable;
            UnlimitedPlayerManager.OnGameDataReceived -= GameDataReceived;
            UnlimitedPlayerManager.OnGameDataUnavailable -= GameDataUnavailable;
            UnlimitedPlayerManager.OnCancelConversionAccepted -= CancelConversionAccepted;
            UnlimitedPlayerManager.OnConversionAccepted -= ConversionAccepted;
            UnlimitedPlayerManager.OnConversionRejected -= ConversionRejected;
            UnlimitedPlayerManager.OnCancelUpgradingAccepted -= CancelUpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingAccepted -= UpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingRejected -= UpgradingRejected;
            UnlimitedPlayerManager.OnCancelBuildingAccepted -= CancelBuildingAccepted;
            UnlimitedPlayerManager.OnBuildingAccepted -= BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected -= BuildingRejected;
        }
        
        #region ServerRequests

        public void GetPlayerData()
        {
            UnlimitedPlayerManager.PlayerDataRequest();
        }

        public void GetGameData()
        {
            UnlimitedPlayerManager.GetGameParameters();
        }
        
        public void ConvertResources()
        {
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.ConversionRequest(startTime);
        }

        public void CancelConvertResources()
        {
            UnlimitedPlayerManager.FinishConversionRequest();
        }
        
        public void UpgradeRobot()
        {
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.UpgradingRequest(robotId, startTime);
        }

        public void CancelUpgradeRobot()
        {
            UnlimitedPlayerManager.FinishUpgradingRequest();
        }

        public void BuildRobot()
        {
            byte queueNumber = 0;
            byte robotId = 0;
            DateTime startTime = DateTime.Now;
            UnlimitedPlayerManager.BuildingRequest(queueNumber, robotId, startTime);
        }

        public void CancelBuildRobot()
        {
            byte robotNumber = 0;
            UnlimitedPlayerManager.FinishBuildingRequest(robotNumber);
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

        private void CancelConversionAccepted()
        {
            Debug.Log("Cancel conversion accepted");
        }
        
        private void ConversionAccepted()
        {
            Debug.Log("Conversion accepted");
        }

        private void ConversionRejected(byte errorId)
        {
            Debug.Log("Conversion rejected");
        }

        private void CancelUpgradingAccepted()
        {
            Debug.Log("Cancel upgrading accepted");
        }

        private void UpgradingAccepted()
        {
            Debug.Log("Upgrading accepted");
        }

        private void UpgradingRejected(byte errorId)
        {
            Debug.Log("Upgrading rejected");
        }

        private void CancelBuildingAccepted()
        {
            Debug.Log("Cancel building accepted");
        }
        
        private void BuildingAccepted()
        {
            Debug.Log("Building accepted");
        }

        private void BuildingRejected(byte errorId)
        {
            Debug.Log("Building rejected");
        }
        
        #endregion
    }
}