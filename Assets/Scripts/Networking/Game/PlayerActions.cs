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
            UnlimitedPlayerManager.OnConversionFinished += ConversionFinished;
        }

        private void OnDestroy()
        {
            UnlimitedPlayerManager.OnConversionAccepted += ConversionAccepted;
            UnlimitedPlayerManager.OnConversionRejected += ConversionRejected;
            UnlimitedPlayerManager.OnConversionFinished += ConversionFinished;
        }
        
        #region ServerRequests

        public void ConvertResources()
        {
            UnlimitedPlayerManager.SendConvertRequest();
        }

        #endregion

        #region ProcessServerResponse

        private void ConversionAccepted(DateTime remainingTime)
        {
            Debug.Log("Conversion accepted");
        }

        private void ConversionRejected(byte errorId)
        {
            Debug.Log("Conversion rejected");
        }

        private void ConversionFinished()
        {
            Debug.Log("Conversion finished");
        }

        #endregion
    }
}