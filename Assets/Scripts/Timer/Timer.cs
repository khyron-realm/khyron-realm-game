using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private bool _hasTimeText;
    [SerializeField] private Text _timeText;

    [SerializeField] private bool _hasProgressBar;
    [SerializeField] private ProgressBar _bar;

    private int _totalTime = 0;
    private int _maxTime = 0;
    private WaitForSeconds _standardTime;


    public int TotalTime
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
    public bool HasTimeText
    {
        get
        {
            return _hasTimeText;
        }
        set
        { 
            if(value == false)
            {
                _timeText.enabled = false;
            }   
            else
            {
                
                _timeText.enabled = true;
                DisplayTime();
            }

            _hasTimeText = value;       
        }
    }

    
    private void Awake()
    {
        _timeText.text = "";     
        _standardTime = new WaitForSeconds(1f);
    }

    private void Start()
    {
        if(_hasProgressBar)
        {
            _bar.CurrentValue = 1;
            _bar.MaxValue = 1;
        }      
    }


    // Time Operations
    public void AddTime(int time)
    {
        _totalTime += time;
        DoStuff();
    }
    public void AddTime(Robot robot)
    {
        _totalTime += robot.buildTime;
        DoStuff();
    }
    public void DecreaseTime(int time)
    {
        _totalTime -= time;

        if(_hasTimeText)
        {
            DisplayTime();
        }
    }
    private void DoStuff()
    {
        if (_hasProgressBar)
        {
            SetProgressBarMaxValue();
        }

        if (_hasTimeText)
        {
            DisplayTime();
        }
    }


    // Show Time
    public void DisplayTime()
    {
        float hours = Mathf.FloorToInt(_totalTime / 3600);
        float minutes = Mathf.FloorToInt((_totalTime % 3600) / 60);
        float seconds = Mathf.FloorToInt(_totalTime % 60);

        UpdateProgressBar();

        if (hours < 1)
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
    public void DisplayTime(Text text, int time)
    {
        float hours = Mathf.FloorToInt(time / 3600);
        float minutes = Mathf.FloorToInt((time % 3600) / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        UpdateProgressBar();

        if (hours < 1)
        {
            if (minutes < 1)
            {
                text.text = string.Format("{0}s", seconds);
            }
            else
            {
                text.text = string.Format("{0}m {1}s", minutes, seconds);
            }
        }
        else
        {
            text.text = string.Format("{0}h {1}m {2}s", hours, minutes, seconds);
        }
    }
    public void TimeTextState(bool temp)
    {
        _timeText.gameObject.SetActive(temp);
    }


    private void UpdateProgressBar()
    {
        if(_hasProgressBar)
        {
            _bar.CurrentValue = _maxTime - _totalTime;
        }    
    }
    private void SetProgressBarMaxValue()
    {
        if (_hasProgressBar)
        {
            _bar.MaxValue = _totalTime;
            _maxTime = _totalTime;
        }
    }


    // Timer 
    public IEnumerator ActivateTimer()
    {
        DecreaseTime(1);
        UpdateProgressBar();
        yield return _standardTime;
    }
}