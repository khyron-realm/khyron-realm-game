using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Sounds
{
    public class ButtonClickSound : MonoBehaviour
    {
        [SerializeField] private byte _index;

        public void ChangeSoundsState()
        {
            SoundManager.SetSounds(!SoundManager.SoundsOn);
        }

        public void ChangeMusicState()
        {
            SoundManager.SetMusic(!SoundManager.MusicOn);
        }

        public void MakeSound()
        {
            SoundManager.MakeSound(_index);
        }
    }
}