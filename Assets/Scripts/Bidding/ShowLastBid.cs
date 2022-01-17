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
        AuctionsManager.OnOverbid += LastBidInRoom;
    }


    private void LastBidEnterRoom()
    {
        _textMeshPro.text = AuctionsManager.CurrentAuctionRoom.LastBid.Amount.ToString();
    }
    private void LastBidInRoom(Bid bid)
    {
        _textMeshPro.text = bid.Amount.ToString();
    }


    private void OnDestroy()
    {
        AuctionsManager.OnAddBid -= LastBidInRoom;
        AuctionsManager.OnSuccessfulAddBid -= LastBidInRoom;
        AuctionsManager.OnOverbid -= LastBidInRoom;
    }
}