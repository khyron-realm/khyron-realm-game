using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;


public class GetMinesFromServer : MonoBehaviour
{
    private void Awake()
    {
        AuctionsManager.OnReceivedMines += GetMinesForPlayer;
    }
    

    private void Start()
    {
        AuctionsManager.GetUserMines();
    }
    

    private void GetMinesForPlayer(List<Networking.Mines.Mine> mineList)
    {
        
    }
}