using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CountDown;
using AuxiliaryClasses;
using Networking.Auctions;


namespace Bidding
{
    public class BiddingCountDown : MonoBehaviour
    {
        #region "Input data" 
        [SerializeField] private Timer _timeTillFinished;
        #endregion


        private void Awake()
        {
            StartCounter();
            AuctionsManager.OnAuctionFinished += FinishedAuction;
        }


        private void StartCounter()
        {
            Debug.LogWarning(AuxiliaryMethods.TimeTillFinish(AuctionsManager.CurrentAuctionRoom.EndTime));
            StartCoroutine(AuctionIsWorking(600 - AuxiliaryMethods.TimeTillFinish(AuctionsManager.CurrentAuctionRoom.EndTime)));
        }
        
        private IEnumerator AuctionIsWorking(int time)
        {
            _timeTillFinished.SetMaxValueForTime(600);
            _timeTillFinished.AddTime(time);

            int temp = 0;
            while (temp < time)
            {
                temp += 1;
                yield return _timeTillFinished.ActivateTimer();
            }
        }

        private void FinishedAuction(uint roomId, uint winner)
        {

        }



        private void OnDestroy()
        {
            AuctionsManager.OnSuccessfulJoinRoom -= StartCounter;
            AuctionsManager.OnAuctionFinished -= FinishedAuction;
        }
    }
}