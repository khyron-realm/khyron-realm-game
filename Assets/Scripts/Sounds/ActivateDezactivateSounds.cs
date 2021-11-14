using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ActivateDezactivateSounds : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _music;
    [SerializeField] private AudioListener _listener;


    private static bool _musicOn = true;
    private static bool _sfxOn = true;


    public void MusicHandler()
    {
        if(_musicOn)
        {
            foreach (AudioSource item in _music)
            {
                item.enabled = false;
                _musicOn = false;
            }
        }
        else
        {
            foreach (AudioSource item in _music)
            {
                item.enabled = true;
                _musicOn = true;
            }
        }
    }


    public void SfxHandler()
    {
        if (_sfxOn)
        {
            _listener.enabled = false;
            _sfxOn = false;
        }
        else
        {
            _listener.enabled = true;
            _sfxOn = true;

            foreach (AudioSource item in _music)
            {
                item.enabled = true;
                _musicOn = true;
            }
        }
    }
}