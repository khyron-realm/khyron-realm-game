using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;


public class GetMinesFromServer : MonoBehaviour
{
    private void Awake()
    {
        AuctionsManager.GetOpenAuctionRooms();
        AuctionsManager.OnReceivedOpenRooms += GetRoomsForPlayer;
    }


    private void GetRoomsForPlayer(List<AuctionRoom> roomList)
    {
        
    }
}