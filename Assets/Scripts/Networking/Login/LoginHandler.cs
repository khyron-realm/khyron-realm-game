using Networking.Login;
using Networking.Game;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    private int x = 0;
    public void onClickLogin()
    {
        //LoginManager.Login("gigel", "12345");
        
        //LoginManager.AddUser("c", "parola");

        //x = 3;
        
        if (x == 0)
        {
            LoginManager.Login("c", "parola");
            x = 1;
        }
        else if (x == 1)
        {
            //UnlimitedPlayerManager.PlayerDataRequest();
            
            //UnlimitedPlayerManager.ConversionRequest();
            //UnlimitedPlayerManager.CancelConversionRequest();
            
            //UnlimitedPlayerManager.UpgradingRequest(1);
            //UnlimitedPlayerManager.CancelUpgradingRequest();
            
            //UnlimitedPlayerManager.BuildingRequest(1, 2);
            UnlimitedPlayerManager.CancelBuildingRequest(1);
            
            //x = 0;
        }
    }
}