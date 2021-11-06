using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;
using Networking.Chat;
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
        ChatManager.OnRoomMessage += RoomMessageReceived;
        ChatManager.OnPrivateMessage += PrivateMessageReceived;
        ChatManager.OnSuccessfulJoinGroup += SuccessfulJoinGroup;
    }

    private void Start()
    {
        MineManager.GetUserMines();
        AuctionsManager.GetOpenAuctionRooms();
    }
    
    private void RoomMessageReceived(ChatMessage message)
    {
        PrintListMessages(ChatManager.Messages);
    }
    
    private void PrivateMessageReceived(ChatMessage message)
    {
        Debug.Log(message.Sender + ": " + message.Content);
    }
    
    private void PrintListMessages(List<ChatMessage> messages)
    {
        foreach (var message in messages)
        {
            Debug.Log(message.Sender + ": " + message.Content);
        }
    }
    
    private void GetOpenRooms()
    {
        Debug.Log("Open rooms: " + AuctionsManager.RoomList.Count);
        // print RoomList ids
        foreach (var room in AuctionsManager.RoomList)
        {
            Debug.Log(room.Id);
        }
        Debug.LogWarning("joining room: " + AuctionsManager.RoomList[1].Id);
        AuctionsManager.JoinAuctionRoom(AuctionsManager.RoomList[1].Id);
    }

    private void GetMinesForPlayer()
    {
        Debug.LogWarning("Mines: " + MineManager.MineList.Count);
    }
    
    private void SuccessfulJoinRoom()
    {
        Debug.LogWarning("Successfully joined room with nr players " + AuctionsManager.Players.Count);
        Debug.LogWarning("nr scans = " + AuctionsManager.CurrentAuctionRoom.Scans.Length);
        Debug.LogWarning("global = " + AuctionsManager.CurrentAuctionRoom.MineValues.Global.Seed);
        Debug.LogWarning("silicon = " + AuctionsManager.CurrentAuctionRoom.MineValues.Global.RarityCoefficient);
        Debug.LogWarning("Lithium = " + AuctionsManager.CurrentAuctionRoom.MineValues.Global.Frequency);
        //AuctionsManager.LeaveAuctionRoom();
        //AuctionsManager.AddBid(1500);
        AuctionsManager.AddScan(new MineScan("gigel123", 3, 8));
        //ChatManager.SendRoomMessage("Gigel is here");
        ChatManager.JoinChatGroup("chat123");
        ChatManager.SendPrivateMessage("gigel123", "salut gigele");
    }
    
    private void SuccessfulJoinGroup(string groupName)
    {
        Debug.LogWarning("Successfully joined group" + groupName);
        ChatManager.GetActiveGroups();
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