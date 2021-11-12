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
        AuctionsManager.OnSuccessfulLeaveRoom += LeftConfirmation;
        AuctionsManager.OnReceivedRoom += ReceivedRoom;
        AuctionsManager.OnSuccessfulJoinRoom += SuccessfullyJoinedRoom;
    }

    public void EnterAnotherAuction()
    {
        Debug.LogWarning("Requesting room");
        AuctionsManager.GetAuctionRoom();        
    }
    
    private void LeftConfirmation()
    {
        Debug.LogWarning("Leaving old room");
        AuctionsManager.LeaveAuctionRoom();
    }
    
    private void ReceivedRoom()
    {
        Debug.LogWarning("Getting room: " + AuctionsManager.CurrentAuctionRoom.Id);
        AuctionsManager.JoinAuctionRoom(AuctionsManager.CurrentAuctionRoom.Id);
    }
    
    private void SuccessfullyJoinedRoom()
    {
        Debug.LogWarning("Joining room: " + AuctionsManager.CurrentAuctionRoom.Id);
        _changeScene.GoToScene();
    }


    private void OnDestroy()
    {
        AuctionsManager.OnSuccessfulLeaveRoom -= LeftConfirmation;
        AuctionsManager.OnReceivedRoom -= ReceivedRoom;
        AuctionsManager.OnSuccessfulJoinRoom -= SuccessfullyJoinedRoom;
    }
}