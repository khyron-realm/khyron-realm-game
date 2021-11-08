using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Networking.Auctions;


public class ShowLastBid : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        LastBidEnterRoom();
        AuctionsManager.OnAddBid += LastBidInRoom;
        AuctionsManager.OnSuccessfulAddBid += LastBidInRoom;
    }


    private void LastBidEnterRoom()
    {
        _textMeshPro.text = AuctionsManager.CurrentAuctionRoom.LastBid.Amount.ToString();
    }
    private void LastBidInRoom()
    {
        _textMeshPro.text = AuctionsManager.Bids[AuctionsManager.Bids.Count - 1].Amount.ToString();
    }


    private void OnDestroy()
    {
        AuctionsManager.OnSuccessfulJoinRoom -= LastBidEnterRoom;
        AuctionsManager.OnAddBid -= LastBidInRoom;
        AuctionsManager.OnSuccessfulAddBid -= LastBidInRoom;
    }
}