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
        AuctionsManager.OnReceivedRoom += ReceivedRoom;
        AuctionsManager.OnSuccessfulJoinRoom += SuccessfullyJoinedRoom;
    }


    public void FindAuction()
    {
        AuctionsManager.GetAuctionRoom();        
    }
    private void ReceivedRoom(byte playerBidsNo)
    {
        AuctionsManager.JoinAuctionRoom(AuctionsManager.CurrentAuctionRoom.Id);
    }
    private void SuccessfullyJoinedRoom()
    {
        ManageStatesOfScreen.ChangeState();
        _changeScene.GoToScene();
    }


    private void OnDestroy()
    {
        AuctionsManager.OnReceivedRoom -= ReceivedRoom;
        AuctionsManager.OnSuccessfulJoinRoom -= SuccessfullyJoinedRoom;
    }
}