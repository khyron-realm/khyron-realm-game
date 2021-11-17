using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sounds;

namespace Panels
{
    public class ChangeButtonImg : MonoBehaviour
    {
        [SerializeField] private Sprite _activeButton;
        [SerializeField] private Sprite _nonActiveButton;

        [SerializeField] private Sprite _activeButton1;
        [SerializeField] private Sprite _nonActiveButton1;

        [SerializeField] private Button _buttonMusic;
        [SerializeField] private Button _buttonSfx;


        private Image _img1;
        private Image _img2;


        private void Awake()
        {
            _img1 = _buttonMusic.GetComponent<Image>();
            _img2 = _buttonSfx.GetComponent<Image>();

            SoundManager.OnMusicChanged += ChangeImageMusic;
            SoundManager.OnSoundsChanged += ChangeImageSfx;

        }
        private void Start()
        {
            ChangeImageMusic(SoundManager.MusicOn);
            ChangeImageSfx(SoundManager.SoundsOn);
        }


        public void ChangeImageMusic(bool temp)
        {
            if (temp)
            {
                _img1.sprite = _activeButton;
            }
            else
            {
                _img1.sprite = _nonActiveButton;
            }
        }
        public void ChangeImageSfx(bool temp)
        {
            if (temp)
            {
                _img2.sprite = _activeButton1;
            }
            else
            {
                _img2.sprite = _nonActiveButton1;
            }
        }


        private void OnDestroy()
        {
            SoundManager.OnMusicChanged -= ChangeImageMusic;
            SoundManager.OnSoundsChanged -= ChangeImageSfx;
        }
    }
}