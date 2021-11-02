using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;


public class GetMinesFromServer : MonoBehaviour
{
    private void Awake()
    {
        AuctionsManager.OnReceivedOpenRooms += GetRoomsForPlayer;
    }


    private void Start()
    {
        AuctionsManager.GetOpenAuctionRooms();
    }


    private void GetRoomsForPlayer(List<AuctionRoom> roomList)
    {
        Debug.LogWarning("Received open rooms");
        Debug.LogWarning("--------" + roomList[0].Name + "--------");
        Debug.LogWarning("--------" + roomList[1].Name + "--------");
    }
}