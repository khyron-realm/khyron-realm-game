using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Networking.Auctions;

public class ShowNameOfTheMine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameOfTheMine;

    private void Awake()
    {
        _nameOfTheMine.text = AuctionsManager.CurrentAuctionRoom.Name;
    }
}