using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;
using Networking.Mine;


public class GetMinesFromServer : MonoBehaviour
{
    private void Awake()
    {
        AuctionsManager.OnReceivedOpenRooms += GetRoomsForPlayer;
        AuctionsManager.OnReceivedMines += GetMinesForPlayer;
    }


    private void Start()
    {
        AuctionsManager.GetOpenAuctionRooms();
        AuctionsManager.GetUserMines();
    }


    private void GetRoomsForPlayer(List<AuctionRoom> roomList)
    {
        Debug.LogWarning("Received open rooms");
        Debug.LogWarning("--------" + roomList[0].Name + "--------");
        Debug.LogWarning("--------" + roomList[1].Name + "--------");
    }
    
    private void GetMinesForPlayer(List<MineData> mineList)
    {
        Debug.LogWarning("Received mines");
        Debug.LogWarning("--------" + mineList.Count + "--------");
        if(mineList.Count > 0)
            Debug.LogWarning("--------" + mineList[0].Name + "--------");
    }
}