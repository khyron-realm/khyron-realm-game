using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ProgressBar : MonoBehaviour
{
    #region "InputFields"

    [SerializeField] private int _maxValue;

    [SerializeField] private int _currentValue;

    private void OnValidate()
    {
        if (_maxValue < 0)
        {
            _maxValue = 0;
        }

        _currentValue = Mathf.Clamp(_currentValue, 0, _maxValue);
    }

    [SerializeField] private Image _fillImage;

    [Space(20f)]

    [SerializeField] private bool _hasText;
   
    [SerializeField] private TextMeshProUGUI _text;

    #endregion

    #region "Public Values"

    public int MaxValue
    {
        get
        {
            return _maxValue;
        }
        set
        {
            if(value > 0)
            {
                _maxValue = value;
            }
            else
            {
                _maxValue = 0;
            }
        }
    }

    public int CurrentValue
    {
        get
        {
            return _currentValue;
        }
        set
        {
            _currentValue = Mathf.Clamp(value, 0, _maxValue);
        }
    }

    #endregion

    private void Update()
    {
        SetFillAmount();
        if(_hasText)
        {
            SetText();
        }
    }

    private void SetFillAmount()
    {
        if(_maxValue > 0)
        {
            _fillImage.fillAmount = (float)_currentValue / (float)_maxValue;
        }
    }

    private void SetText()
    {
        _text.text = _currentValue.ToString();
    }

    public void MakeTextLarge(int value)
    {
        _text.fontSize += value;
    }

    public void MakeTextSmaller(int value)
    {
        _text.fontSize -= value;
    }
}