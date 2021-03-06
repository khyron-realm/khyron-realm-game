using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Networking.Headquarters;


namespace Panels
{
    public class PlayerDisplayName : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private TextMeshProUGUI _name;
        #endregion

        private void Awake()
        {
            HeadquartersManager.OnPlayerDataReceived += UpdateText;
        }

        private void UpdateText()
        {
            _name.text = HeadquartersManager.Player.Id;
        }

        private void OnDestroy()
        {
            HeadquartersManager.OnPlayerDataReceived -= UpdateText;
        }
    }
}