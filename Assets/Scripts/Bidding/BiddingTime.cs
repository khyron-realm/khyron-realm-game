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
            _timeTillFinished.TotalTime = GetTimeTillAuctionEnds.TimeOfTheMine;
            StartCoroutine(AuctionIsWorking());
        }

        
        private IEnumerator AuctionIsWorking()
        {
            while(_timeTillFinished.TotalTime > 0)
            {
                yield return _timeTillFinished.ActivateTimer();
            }

            OnAuctionFinished?.Invoke();
        }
    }
}