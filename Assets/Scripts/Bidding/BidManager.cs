using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Networking.Auctions;
using Networking.Headquarters;
using TMPro;
using PlayerDataUpdate;
using Levels;
using Networking.Mines;

public class BidManager : MonoBehaviour
{
    #region "Input  data"
    [SerializeField] private TextMeshProUGUI _textDisplayed;
    [SerializeField] private Button _yesButton;

    [SerializeField] private uint _biddingPrice;
    #endregion

    public static event Action OnLastBidWasYours;
    public static event Action OnSomeOneBiddedOverYou;
    public static event Action<byte> OnNotEnoughEnergy;
    public static event Action OnToMuchEnergy;

    public static event Action OnMaximBidsAchieved;

    private static int bidValue;


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
        if ((MineManager.MineList.Count + AuctionsManager.BidsNumber) > 5)
        {
            OnMaximBidsAchieved?.Invoke();
            return;
        }

        if (AuctionsManager.CurrentAuctionRoom.LastBid.PlayerName != HeadquartersManager.Player.Id)
        {
            bidValue = (int)(AuctionsManager.CurrentAuctionRoom.LastBid.Amount + _biddingPrice);
            int temp = (int)HeadquartersManager.Player.Energy - bidValue;

            if(temp > 0)
            {
                if (temp <= LevelMethods.MaxEnergyCount(HeadquartersManager.Player.Level))
                {
                    HeadquartersManager.Player.Energy -= (uint)bidValue;
                    AuctionsManager.AddBid((uint)bidValue, HeadquartersManager.Player.Energy);
                }
                else
                {
                    OnToMuchEnergy?.Invoke();
                }
            }
            else
            {
                OnNotEnoughEnergy?.Invoke(255);
            }
        }
        else
        {
            OnLastBidWasYours?.Invoke();
        }
    }


    private void BidFailed()
    {
        OnSomeOneBiddedOverYou?.Invoke();
    }


    private void OnDestroy()
    {
        AuctionsManager.OnFailedAddBid -= BidFailed;
        _yesButton.onClick.RemoveAllListeners();
    }
}