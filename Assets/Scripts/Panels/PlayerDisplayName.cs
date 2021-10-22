using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Headquarters;


namespace Panels
{
    public class PlayerDisplayName : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Text _name;
        #endregion

        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += UpdateText;
        }

        private void UpdateText()
        {
            _name.text = HeadquartersManager.Player.Id;
        }
    }
}