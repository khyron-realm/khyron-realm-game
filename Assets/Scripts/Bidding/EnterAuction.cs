using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Networking.Auctions;
using Scenes;


public class EnterAuction : MonoBehaviour
{
    [SerializeField] private ChangeScene _changeScene;

    private void Awake()
    {
        AuctionsManager.OnReceivedOpenRooms += ReceivedOpenRooms;
        AuctionsManager.OnSuccessfulJoinRoom += SuccesfulyJoinedRoom;
    }

    public void FindAuction()
    {
        AuctionsManager.GetOpenAuctionRooms();        
    }


    private void ReceivedOpenRooms()
    {
        AuctionsManager.JoinAuctionRoom(AuctionsManager.RoomList[0].Id);
    }


    private void SuccesfulyJoinedRoom(List<Player> players)
    {
        _changeScene.GoToScene();
    }
}