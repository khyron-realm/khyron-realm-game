using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Headquarters;
using Levels;


public class TopUpEnergy : MonoBehaviour
{
    private void Awake()
    {
        HeadquartersManager.OnPlayerDataReceived += TopUpEnergyMethod;
    }


    private void TopUpEnergyMethod()
    {
        if (HeadquartersManager.Player.BackgroundTasks.Length < 1) return;

        foreach (BackgroundTask item in HeadquartersManager.Player.BackgroundTasks)
        {
            if(item.Type == BackgroundTaskType.EnergyTopUp)
            {
                uint temp = HeadquartersManager.Player.Energy + item.ValueId;

                if (temp < LevelMethods.MaxEnergyCount(HeadquartersManager.Player.Level))
                {
                    HeadquartersManager.Player.Energy += item.ValueId;
                    HeadquartersManager.UpdateEnergy(HeadquartersManager.Player.Energy);
                }  
            }

            HeadquartersManager.RemoveBackgroundTaskRequest(item);
        }        
    }


    private void OnDestroy()
    {
        HeadquartersManager.OnPlayerDataReceived -= TopUpEnergyMethod;
    }
}