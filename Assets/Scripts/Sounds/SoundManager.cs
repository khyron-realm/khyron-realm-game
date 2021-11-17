using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> _sounds;
        [SerializeField] private List<AudioSource> _music;

        public static bool SoundsOn = true;
        public static bool MusicOn = true;

        private static List<AudioSource> s_music;
        private static List<AudioSource> s_sounds;

        public static event Action<bool> OnSoundsChanged;
        public static event Action<bool> OnMusicChanged;

        private void Awake()
        {
            s_music = new List<AudioSource>(_music);
            s_sounds = new List<AudioSource>(_sounds);
        }

        public static void MakeSound(byte index)
        {
            if (SoundsOn == false) return;
            s_sounds[index].Play();
        }

        public static void SetMusic(bool state)
        {
            MusicOn = state;
            foreach (AudioSource item in s_music)
            {
                item.enabled = state;
            }

            OnMusicChanged?.Invoke(MusicOn);
        }

        public static void SetSounds(bool state)
        {
            SoundsOn = state;
            OnSoundsChanged?.Invoke(SoundsOn);
        }
    }
}