using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Bidding
{
    public class EndOfAuctionAnimation : MonoBehaviour
    {
        #region "Input Data"
        [SerializeField] private GameObject _canvas;

        [SerializeField] private GameObject _sold;
        [SerializeField] private GameObject _aquired;

        [SerializeField] private Image _panelBackground;
        #endregion

        private void Awake()
        {
            BiddingCountDown.OnAuctionFinished += Animation;
        }


        private void Animation()
        {
            _canvas.SetActive(true);

            _aquired.transform.localScale = new Vector3(2f, 2f ,1);
            _panelBackground.color = new Color(0,0,0,0);

            _panelBackground.DOFade(0.5f, 0.4f);
            _aquired.transform.DOScale(1f, 0.8f);
        }

        private void OnDestroy()
        {
            BiddingCountDown.OnAuctionFinished -= Animation;
        }
    }
}