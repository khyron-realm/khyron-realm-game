using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenes;
using Networking.Auctions;
using AuxiliaryClasses;


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
        AuctionsManager.LeaveAuctionRoom();

        if (auctionIndex < 5)
        {
            auctionIndex++;

            if(AuxiliaryMethods.TimeTillFinishStart(AuctionsManager.RoomList[auctionIndex].Id) < 1)
            {
                AuctionsManager.JoinAuctionRoom(AuctionsManager.RoomList[auctionIndex].Id);
            }
            else
            {
                AuctionsManager.GetOpenAuctionRooms();
            }            
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