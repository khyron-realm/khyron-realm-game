using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Sounds
{
    public class ButtonClickSound : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Button _button;
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _audioClip;
        #endregion

        private void Awake()
        {
            _button.onClick.AddListener(ButtonPressed);
        }

        public void ButtonPressed()
        {
            _source.PlayOneShot(_audioClip);
        }
    }
}