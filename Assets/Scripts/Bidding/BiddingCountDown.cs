using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CountDown;
using AuxiliaryClasses;
using Networking.Auctions;
using TMPro;

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
            StartCoroutine(AuctionIsWorking(AuxiliaryMethods.TimeTillFinishStart(AuctionsManager.CurrentAuctionRoom.EndTime)));
        }
        

        private IEnumerator AuctionIsWorking(int time)
        {
            _timeTillFinished.AddTime(time);

            int temp = 0;
            while (temp < time)
            {
                temp += 1;
                yield return _timeTillFinished.ActivateTimer();
            }
        }


        private void FinishedAuction(uint roomId, string winner)
        {

        }



        private void OnDestroy()
        {
            AuctionsManager.OnSuccessfulJoinRoom -= StartCounter;
            AuctionsManager.OnAuctionFinished -= FinishedAuction;
        }
    }
}