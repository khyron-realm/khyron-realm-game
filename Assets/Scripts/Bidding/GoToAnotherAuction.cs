using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenes;
using Networking.Auctions;


public class GoToAnotherAuction : MonoBehaviour
{
    [SerializeField] private ChangeScene _changeScene;

    private static byte auctionIndex = 0;

    private void Awake()
    {
        AuctionsManager.OnReceivedOpenRooms += ReceivedOpenRooms;
        AuctionsManager.OnSuccessfulJoinRoom += SuccessfullyJoinedRoom;
    }


    public void EnterAnotherAuction()
    {
        if (auctionIndex < 5)
        {
            auctionIndex++;
            AuctionsManager.JoinAuctionRoom(AuctionsManager.RoomList[auctionIndex].Id);
        }
        else
        {
            AuctionsManager.GetOpenAuctionRooms();
        }
    }
    private void ReceivedOpenRooms()
    {
        auctionIndex = 0;
        AuctionsManager.JoinAuctionRoom(AuctionsManager.RoomList[0].Id);
    }


    private void SuccessfullyJoinedRoom()
    {
        _changeScene.GoToScene();
    }


    private void OnDestroy()
    {
        AuctionsManager.OnReceivedOpenRooms -= ReceivedOpenRooms;
        AuctionsManager.OnSuccessfulJoinRoom -= SuccessfullyJoinedRoom;
    }
}