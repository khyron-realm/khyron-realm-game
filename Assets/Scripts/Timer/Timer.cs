using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Panels;


namespace CountDown
{
    public class Timer : MonoBehaviour
    {
        #region "Input data"
        [Header("Text to display time")]
        [SerializeField] private bool _hasTimeText;
        [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(_hasTimeText))]
        [SerializeField] private Text _timeText;
       
        [Space(20f)]

        [Header("Progress Bar for time")]
        [SerializeField] private bool _hasProgressBar;
        [ShowIf(ActionOnConditionFail.DontDraw, ConditionOperator.And, nameof(_hasProgressBar))]
        [SerializeField] private ProgressBar _bar;
        #endregion


        #region "Private Variables"
        private int _currentTime = 0;
        private int _maxTime = 0;

        private WaitForSeconds _standardTime;
        #endregion


        #region "Public Variables"
        public int CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                if (_currentTime > -1)
                {
                    _currentTime = value;
                }
                else
                {
                    _currentTime = 0;
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
                if (value == false)
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
        #endregion


        #region "Awake & Start"
        private void Awake()
        {
            _timeText.text = "";
            _standardTime = new WaitForSeconds(1f);
        }

        private void Start()
        {
            if (_hasProgressBar)
            {
                _bar.CurrentValue = 1;
                _bar.MaxValue = 1;
            }
        }
        #endregion


        #region "Time Operation"
        // Time Operations
        public void AddTime(int time)
        {
            CurrentTime += time;

            if (_hasTimeText)
            {
                DisplayTime();
            }
        }
        public void DecreaseTime(int time)
        {
            CurrentTime -= time;

            if (_hasTimeText)
            {
                DisplayTime();
            }
        }
        public void SetMaxValueForTime(int max)
        {
            _maxTime = max;

            if(_hasProgressBar)
            {
                _bar.MaxValue = max;
            }           
        }
        #endregion


        #region "Show CountDown"
        // Show Time
        public void DisplayTime()
        {
            DisplayTime(_timeText, _currentTime);
        }
        public void DisplayTime(Text text, int time)
        {
            float days = Mathf.FloorToInt(time / (86400));
            float hours = Mathf.FloorToInt((time % 86400) / 3600);
            float minutes = Mathf.FloorToInt((time % 3600) / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            UpdateProgressBar();

            if (days < 1)
            {
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
                    text.text = string.Format("{0}h {1}m", hours, minutes);
                }
            }
            else
            {
                text.text = string.Format("{0}d {1}h", days, hours);
            }
        }
        public void TimeTextState(bool temp)
        {
            _timeText.gameObject.SetActive(temp);
        }
        #endregion


        private void UpdateProgressBar()
        {
            if (_hasProgressBar)
            {
                _bar.CurrentValue = _maxTime - _currentTime;
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
}