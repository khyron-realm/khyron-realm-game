using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Auctions;
using Networking.Headquarters;
using TMPro;
using PlayerDataUpdate;

public class BidManager : MonoBehaviour
{
    #region "Input  data"
    [SerializeField] private TextMeshProUGUI _textDisplayed;
    [SerializeField] private Button _yesButton;

    [SerializeField] private uint _biddingPrice;
    #endregion

    public static event Action OnLastBidWasYours;
    public static event Action OnSomeOneBiddedOverYou;


    private static int bidValue;


    private void Awake()
    {
        AuctionsManager.OnFailedAddBid += BidFailed;
        PlayerDataOperations.OnEnergyModified += BidAccepted;

        _yesButton.onClick.AddListener(Bid);
    }


    public void ShowBidStatus()
    {       
        _textDisplayed.text = "You are bidding " + (AuctionsManager.CurrentAuctionRoom.LastBid.Amount + _biddingPrice) + " energy for the mine";               
    }
    public void Bid()
    {
        if (AuctionsManager.CurrentAuctionRoom.LastBid.PlayerName != HeadquartersManager.Player.Id)
        {
            bidValue = (int)(AuctionsManager.CurrentAuctionRoom.LastBid.Amount + _biddingPrice);
            PlayerDataOperations.PayEnergy((int)-(AuctionsManager.CurrentAuctionRoom.LastBid.Amount + _biddingPrice), OperationsTags.BIDING_MINE);
        }
        else
        {
            OnLastBidWasYours?.Invoke();
        }
    }


    private void BidAccepted(byte index)
    {
        if (index != OperationsTags.BIDING_MINE) return;
        AuctionsManager.AddBid(AuctionsManager.CurrentAuctionRoom.LastBid.Amount + _biddingPrice, HeadquartersManager.Player.Energy);
    }


    private void BidFailed()
    {
        Debug.LogWarning("FAILED");
        PlayerDataOperations.PayEnergy(bidValue, 255);
        OnSomeOneBiddedOverYou?.Invoke();
    }


    private void OnDestroy()
    {
        AuctionsManager.OnFailedAddBid -= BidFailed;
        PlayerDataOperations.OnEnergyModified -= BidAccepted;

        _yesButton.onClick.RemoveAllListeners();
    }
}