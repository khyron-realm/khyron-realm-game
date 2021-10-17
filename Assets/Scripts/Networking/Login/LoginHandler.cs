using System;
using Networking.Login;
using Networking.Game;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    private static ushort x = 0;
    private static long time = DateTime.Now.ToBinary();
    public void onClickLogin()
    {
        Debug.Log("Starting action ..................... ");
        
        //UnlimitedPlayerManager.PlayerDataRequest();
        UnlimitedPlayerManager.GameDataRequest();
        //UnlimitedPlayerManager.ConversionRequest(DateTime.Now);
        //UnlimitedPlayerManager.FinishConversionRequest();
        
        //UnlimitedPlayerManager.UpgradingRequest(1);
        //UnlimitedPlayerManager.FinishUpgradingRequest();
        
        //Debug.Log("Task 3");
        //UnlimitedPlayerManager.BuildingRequest(x, 2, DateTime.FromBinary(time));
        time = 0;
        x++;
        //UnlimitedPlayerManager.FinishBuildingRequest(0, 0, DateTime.Now, false, false);
    }
}