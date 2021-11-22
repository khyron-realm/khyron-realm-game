using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Headquarters;


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
                HeadquartersManager.Player.Energy += item.ValueId;
                HeadquartersManager.UpdateEnergy(HeadquartersManager.Player.Energy);

                if(item != null)
                    HeadquartersManager.RemoveBackgroundTaskRequest(item);
            }           
        }        
    }


    private void OnDestroy()
    {
        HeadquartersManager.OnPlayerDataReceived -= TopUpEnergyMethod;
    }
}