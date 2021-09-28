using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Confirm : MonoBehaviour
{
    [SerializeField] private Text _textDisplayed;
    [SerializeField] private Button _yesButton;

    private int _amount = 5000;

    public static Action<int> OnBidAccepted;

    public void ShowDetails()
    {
        _textDisplayed.text = "You are bidding " + _amount + " energy for the mine"; 
    }

    public void ActionAccepted()
    {
        OnBidAccepted?.Invoke(_amount);
        _amount += 500;
    }
}