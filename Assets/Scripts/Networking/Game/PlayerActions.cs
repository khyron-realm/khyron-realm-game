using System;
using UnityEngine;

namespace Networking.Game
{
    public class PlayerActions : MonoBehaviour
    {
        private void Awake()
        {
            UnlimitedPlayerManager.OnConversionAccepted += ConversionAccepted;
            UnlimitedPlayerManager.OnConversionRejected += ConversionRejected;
            UnlimitedPlayerManager.OnUpgradingAccepted += UpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingRejected += UpgradingRejected;
            UnlimitedPlayerManager.OnBuildingAccepted += BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected += BuildingRejected;
        }

        private void OnDestroy()
        {
            UnlimitedPlayerManager.OnConversionAccepted -= ConversionAccepted;
            UnlimitedPlayerManager.OnConversionRejected -= ConversionRejected;
            UnlimitedPlayerManager.OnUpgradingAccepted -= UpgradingAccepted;
            UnlimitedPlayerManager.OnUpgradingRejected -= UpgradingRejected;
            UnlimitedPlayerManager.OnBuildingAccepted -= BuildingAccepted;
            UnlimitedPlayerManager.OnBuildingRejected -= BuildingRejected;
        }
        
        #region ServerRequests

        public void ConvertResources()
        {
            UnlimitedPlayerManager.ConversionRequest();
        }
        
        public void UpgradeRobot()
        {
            byte robotId = 0;
            UnlimitedPlayerManager.UpgradingRequest(robotId);
        }

        public void BuildRobot()
        {
            byte robotId = 0;
            UnlimitedPlayerManager.BuildingRequest(robotId);
        }

        #endregion

        #region ProcessServerResponse

        private void ConversionAccepted()
        {
            Debug.Log("Conversion accepted");
        }

        private void ConversionRejected(byte errorId)
        {
            Debug.Log("Conversion rejected");
        }

        private void UpgradingAccepted()
        {
            Debug.Log("Upgrading accepted");
        }

        private void UpgradingRejected(byte errorId)
        {
            Debug.Log("Upgrading rejected");
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