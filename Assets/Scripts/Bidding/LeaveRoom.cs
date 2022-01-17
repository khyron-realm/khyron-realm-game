using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;


public class LeaveRoom : MonoBehaviour
{
    public void LeaveAuctionRoom()
    {
        AuctionsManager.LeaveAuctionRoom();
    }
}