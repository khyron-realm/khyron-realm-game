using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Mines;
using Networking.Auctions;


public class MineDataExtraction : MonoBehaviour
{
    #region "Input values"
    [Header("Auction or Mine values")]
    [SerializeField] private bool _auctionOrMine;
    #endregion


    #region "Public members"
    public static ResourcesData GlobalSeed;
    public static List<ResourcesData> ResourcesSeeds;
    #endregion


    private void Awake()
    {
        GlobalSeed = null;
        ResourcesSeeds = null;

        if (_auctionOrMine)
        {
            GlobalSeed = AuctionsManager.CurrentAuctionRoom.MineValues.Global;

            ResourcesSeeds = new List<ResourcesData>();
            ResourcesSeeds.Add(AuctionsManager.CurrentAuctionRoom.MineValues.Silicon);
            ResourcesSeeds.Add(AuctionsManager.CurrentAuctionRoom.MineValues.Lithium);
            ResourcesSeeds.Add(AuctionsManager.CurrentAuctionRoom.MineValues.Titanium);
        }
        else
        {

        }
    }
}