using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Auctions;
using Networking.Headquarters;
using TMPro;

public class BidManager : MonoBehaviour
{
    #region "Input  data"
    [SerializeField] private TextMeshProUGUI _textDisplayed;
    [SerializeField] private Button _yesButton;

    [SerializeField] private uint _biddingPrice;
    #endregion

    private void Awake()
    {
        AuctionsManager.OnFailedAddBid += BidFailed;
        _yesButton.onClick.AddListener(Bid);
    }


    public void ShowBidStatus()
    {
          _textDisplayed.text = "You are bidding " + (AuctionsManager.CurrentAuctionRoom.LastBid.Amount + _biddingPrice) + " energy for the mine";              
    }


    public void Bid()
    {
          AuctionsManager.AddBid(AuctionsManager.CurrentAuctionRoom.LastBid.Amount + _biddingPrice, HeadquartersManager.Player.Energy);        
    }


    private void BidFailed()
    {
        print("SomeOne bidded over you already. Bid Again!!");
    }


    private void OnDestroy()
    {
        AuctionsManager.OnFailedAddBid -= BidFailed;
        _yesButton.onClick.RemoveAllListeners();
    }
}