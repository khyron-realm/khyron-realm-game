using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Panels
{
    public class Confirm : MonoBehaviour
    {
        #region "Input  data"
        [SerializeField] private Text _textDisplayed;
        [SerializeField] private Button _yesButton;
        #endregion

        private int _amount = 5000;

        public static Action<int> OnAccepted;

        /// <summary>
        /// Display the text
        /// </summary>
        public void ShowDetails()
        {
            _textDisplayed.text = "You are bidding " + _amount + " energy for the mine";
        }

        /// <summary>
        /// Invoke the event [Method added as listener to button]
        /// </summary>
        public void ActionAccepted()
        {
            OnAccepted?.Invoke(_amount);
            _amount += 500;
        }
    }
}