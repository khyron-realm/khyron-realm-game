using Networking.Login;
using Networking.Game;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    private int x = 0;
    public void onClickLogin()
    {
        //UnlimitedPlayerManager.PlayerDataRequest();
        
        UnlimitedPlayerManager.ConversionRequest();
        //UnlimitedPlayerManager.CancelConversionRequest();
        
        UnlimitedPlayerManager.UpgradingRequest(1);
        //UnlimitedPlayerManager.CancelUpgradingRequest();
        
        UnlimitedPlayerManager.BuildingRequest(0, 0);
        //UnlimitedPlayerManager.CancelBuildingRequest(100);
    }
}