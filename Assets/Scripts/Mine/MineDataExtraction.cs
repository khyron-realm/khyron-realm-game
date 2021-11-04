using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Mines;
using Networking.Auctions;


public class MineDataExtraction : MonoBehaviour
{
    [Header("Auction or Mine values")]
    [SerializeField] private bool _auctionOrMine;

    public static List<ResourcesData> ResourcesSeeds;

    private void Awake()
    {
        if(_auctionOrMine)
        {
            ResourcesSeeds = new List<ResourcesData>();
            ResourcesSeeds.Add();
        }
        else
        {

        }
    }
}