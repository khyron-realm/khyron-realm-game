using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timeText;

    private int _totalTime = 0;
    private WaitForSeconds _standardTime;

    public int totalTime
    {
        get
        {
            return _totalTime;
        }
        set
        {
            if(_totalTime > -1)
            {
                _totalTime = value;
            } 
            else
            {
                _totalTime = 0;
            }
        }
    }

    private void Awake()
    {
        _standardTime = new WaitForSeconds(1f);
    }


    // Time Operations
    public void AddTime(int time)
    {
        _totalTime += time;
        DisplayTime();
    }
    public void AddTime(Robot robot)
    {
        _totalTime += robot.buildTime;
        DisplayTime();
    }
    public void DecreaseTime(int time)
    {
        _totalTime -= time;
        DisplayTime();
    }


    // Show Time
    private void DisplayTime()
    {
        float hours = Mathf.FloorToInt(_totalTime / 3600);
        float minutes = Mathf.FloorToInt((_totalTime % 3600) / 60);
        float seconds = Mathf.FloorToInt(_totalTime % 60);

        if(hours < 1)
        {
            if(minutes < 1)
            {
                _timeText.text = string.Format("{0}s", seconds);
            }  
            else
            {
                _timeText.text = string.Format("{0}m {1}s", minutes, seconds);
            }
        }
        else
        {
            _timeText.text = string.Format("{0}h {1}m {2}s", hours, minutes, seconds);
        }        
    }
    public void TimeTextState(bool temp)
    {
        _timeText.gameObject.SetActive(temp);
    }


    // Timer 
    public IEnumerator ActivateTimer()
    {
        DecreaseTime(1);
        yield return _standardTime;
    }
}