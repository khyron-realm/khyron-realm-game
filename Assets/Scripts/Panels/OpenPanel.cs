using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using CameraActions;


namespace Panels
{
    public class OpenPanel : MonoBehaviour
    {
        #region "Input Fields"

        [SerializeField]
        [Header("Panel that will be opened")]
        [Space(10f)]
        private GameObject _panel;

        [SerializeField]
        [Header("BackGround Panel")]
        [Space(30f)]
        private GameObject _bgPanel;

        [SerializeField]
        [Header("Scale Value")]
        [Space(30f)]
        private float _popUpScaleValue;

        [SerializeField]
        [Range(0, 1)]
        [Header("BackGround trasparency [alpha value]")]
        [Space(30f)]
        private float _bgTransparency;

        #endregion

        #region "Private members used in script"

        private Image _bgImage;
        private Sequence _mySequence;

        private static bool s_openPanel = false;
        #endregion

        private void Awake()
        {
            _bgImage = _bgPanel.GetComponent<Image>();
            _bgPanel.GetComponent<ClosePanelUsingBackround>().OnExit += SetFalse;

            OpenPanelAnimation();

            SetActive();
            SetFalse();
        }


        private void OnMouseUpAsButton()
        {
            if (s_openPanel == false && PanPinchCameraMovement.MovingCamera == false)
            {
                SetActive();
                s_openPanel = true;
            }                    
        }


        public void SetActive()
        {
            _panel.SetActive(true);
            _bgPanel.SetActive(true);

            _mySequence.Restart();
        }
        public void SetFalse()
        {
            s_openPanel = false;

            _panel.SetActive(false);
            _panel.transform.localScale = new Vector3(1, 1, 0);

            _bgPanel.SetActive(false);
            _bgImage.color = new Color(0, 0, 0, 0);
        }


        private void OpenPanelAnimation()
        {
            _mySequence = DOTween.Sequence();
            _mySequence.Append(_panel.transform.DOScale(_popUpScaleValue, 0.12f));
            _mySequence.Append(_panel.transform.DOScale(1, 0.12f));
            _mySequence.Append(_bgImage.DOColor(new Color(0, 0, 0, 0.5f), 0.3f));
            _mySequence.SetAutoKill(false);
        }


        private void OnDestroy()
        {
            try
            {
                _bgPanel.GetComponent<ClosePanelUsingBackround>().OnExit -= SetFalse;
            }
            catch
            {

            }
        }
    }
}