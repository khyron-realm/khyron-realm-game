using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenes;
using Networking.Auctions;
using AuxiliaryClasses;


public class GoToAnotherAuction : MonoBehaviour
{
    [SerializeField] private ChangeScene _changeScene;

    private void Awake()
    {
        AuctionsManager.OnReceivedRoom += ReceivedRoom;
        AuctionsManager.OnSuccessfulLeaveRoom += LeftConfirmation;        
        AuctionsManager.OnSuccessfulJoinRoom += SuccessfullyJoinedRoom;
    }

    public void EnterAnotherAuction()
    {
        AuctionsManager.GetAuctionRoom();        
    }
    private void ReceivedRoom()
    {
        AuctionsManager.LeaveAuctionRoom();
    }
    private void LeftConfirmation()
    {
        AuctionsManager.JoinAuctionRoom(AuctionsManager.CurrentAuctionRoom.Id);
    }     
    private void SuccessfullyJoinedRoom()
    {      
        _changeScene.GoToScene();
    }


    private void OnDestroy()
    {
        AuctionsManager.OnSuccessfulLeaveRoom -= LeftConfirmation;
        AuctionsManager.OnReceivedRoom -= ReceivedRoom;
        AuctionsManager.OnSuccessfulJoinRoom -= SuccessfullyJoinedRoom;
    }
}