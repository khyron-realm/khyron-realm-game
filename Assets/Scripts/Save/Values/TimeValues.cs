using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;

public class TimeValues : MonoBehaviour
{
    [HideInInspector] public int TimeTillFinished;

    private void Awake()
    {
        LoadData();
    }

    public void SaveData()
    {
        SaveSystem.SaveTimeData(this, gameObject);
    }


    //Load data from files
    public void LoadData()
    {
        TimeData temp = SaveSystem.LoadTimeData(gameObject);

        if (temp != null)
        {
            TimeTillFinished = temp.TimeTillFinish;
        }
        else
        {
            SaveData();
        }
    }
}