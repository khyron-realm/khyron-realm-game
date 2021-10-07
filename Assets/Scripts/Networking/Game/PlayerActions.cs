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
        
        public void ConversionStatus()
        {
            UnlimitedPlayerManager.SendConvertStatusRequest();
        }
        
        #endregion

        #region ProcessServerResponse

        private void ConversionAccepted()
        {
            
        }

        private void ConversionRejected(byte errorId)
        {
            
        }

        private void ConversionFinished()
        {
            
        }

        #endregion
    }
}