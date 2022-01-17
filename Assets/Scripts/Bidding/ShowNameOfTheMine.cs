using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Networking.Auctions;
using Networking.Mines;


public class ShowNameOfTheMine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameOfTheMine;
    [SerializeField] private bool _auctionOrMine;

    private void Awake()
    {
        if(_auctionOrMine)
        {
            _nameOfTheMine.text = AuctionsManager.CurrentAuctionRoom.Name;
        }
        else
        {
            _nameOfTheMine.text = MineManager.MineList[MineManager.CurrentMine].Name;   
        }       
    }
}