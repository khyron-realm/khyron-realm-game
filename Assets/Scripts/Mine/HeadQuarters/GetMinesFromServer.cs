using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;
using Networking.Mines;
using Networking.Tags;


public class GetMinesFromServer : MonoBehaviour
{
    private void Awake()
    {
        MineManager.OnReceivedMines += GetMinesForPlayer;
        AuctionsManager.OnReceivedOpenRooms += GetOpenRooms;
        AuctionsManager.OnSuccessfulJoinRoom += SuccessfulJoinRoom;
        AuctionsManager.OnSuccessfulLeaveRoom += SuccessfulLeaveRoom;
        AuctionsManager.OnSuccessfulAddBid += SuccessfulAddBid;
        AuctionsManager.OnFailedAddBid += FailedAddBid;
    }

    private void Start()
    {
        MineManager.GetUserMines();
        AuctionsManager.GetOpenAuctionRooms();
    }
    
    private void GetOpenRooms()
    {
        Debug.Log("Open rooms: " + AuctionsManager.RoomList.Count);
        
        AuctionsManager.JoinAuctionRoom(1);
    }

    private void GetMinesForPlayer()
    {
        Debug.LogWarning("Mines: " + MineManager.MineList.Count);
    }
    
    private void SuccessfulJoinRoom(List<Player> players)
    {
        Debug.LogWarning("Successfully joined room with nr players " + players.Count);
        Debug.LogWarning("nr scans = " + AuctionsManager.CurrentAuctionRoom.Scans.Length);
        //AuctionsManager.LeaveAuctionRoom();
        AuctionsManager.AddBid(1500);
        AuctionsManager.AddScan(new MineScan("gigel123", 3, 8));
    }
    
    private void SuccessfulLeaveRoom()
    {
        Debug.LogWarning("Successfully left room");
    }
    
    private void SuccessfulAddBid()
    {
        Debug.LogWarning("Successfully added bid");
    }
    
    private void FailedAddBid()
    {
        Debug.LogWarning("Failed to add bid");
    }
}