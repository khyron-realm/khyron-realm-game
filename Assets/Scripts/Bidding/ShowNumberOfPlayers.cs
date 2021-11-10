using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Networking.Auctions;

public class ShowNumberOfPlayers : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberOfPlayers;

    private void Awake()
    {
        UpdatePlayerCount(new Player());
        AuctionsManager.OnPlayerJoined += UpdatePlayerCount;
        AuctionsManager.OnPlayerLeft += UpdatePlayerCount;
    }


    private void UpdatePlayerCount(Player player)
    {
        _numberOfPlayers.text = AuctionsManager.Players.Count.ToString() + "/20";
    }
    private void UpdatePlayerCount(string name)
    {
        _numberOfPlayers.text = AuctionsManager.Players.Count.ToString() + "/20";
    }


    private void OnDestroy()
    {
        AuctionsManager.OnPlayerJoined -= UpdatePlayerCount;
        AuctionsManager.OnPlayerLeft -= UpdatePlayerCount;
    }
}