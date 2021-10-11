using System;
using UnityEngine;

namespace Networking.Game
{
    public class PlayerActions : MonoBehaviour
    {
        private void Awake()
        {
            UnlimitedPlayerManager.OnPlayerDataUnavailable += PlayerDataUnavailable;
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
            UnlimitedPlayerManager.OnPlayerDataUnavailable -= PlayerDataUnavailable;
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
        
        public void ConvertResources()
        {
            UnlimitedPlayerManager.ConversionRequest();
        }

        public void CancelConvertResources()
        {
            UnlimitedPlayerManager.CancelConversionRequest();
        }
        
        public void UpgradeRobot()
        {
            byte robotId = 0;
            byte robotPart = 0;
            UnlimitedPlayerManager.UpgradingRequest(robotId, robotPart);
        }

        public void CancelUpgradeRobot()
        {
            UnlimitedPlayerManager.CancelUpgradingRequest();
        }

        public void BuildRobot()
        {
            byte robotId = 0;
            UnlimitedPlayerManager.BuildingRequest(robotId);
        }

        public void CancelBuildRobot()
        {
            UnlimitedPlayerManager.CancelBuildingRequest();
        }

        #endregion

        #region ProcessServerResponse

        private void PlayerDataUnavailable()
        {
            Debug.Log("Player data unavailable");
        }

        private void CancelConversionAccepted()
        {
            Debug.Log("Cancel conversion accepted");
        }
        
        private void ConversionAccepted(long time)
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

        private void UpgradingAccepted(long time)
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
        
        private void BuildingAccepted(long time)
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