using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CountDown;
using Mine;

namespace Bidding
{
    public class BiddingTime : MonoBehaviour
    {
        #region "Input data" 
        [SerializeField] private Timer _timeTillFinished;
        #endregion

        public static event Action OnAuctionFinished; 

        private void Awake()
        {
            StartCoroutine(AuctionIsWorking());
        }

        
        private IEnumerator AuctionIsWorking()
        {
            while(_timeTillFinished.CurrentTime > 0)
            {
                yield return _timeTillFinished.ActivateTimer();
            }

            OnAuctionFinished?.Invoke();
        }
    }
}