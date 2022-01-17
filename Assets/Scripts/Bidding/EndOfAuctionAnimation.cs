using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;
using Scenes;
using TMPro;

namespace Bidding
{
    public class EndOfAuctionAnimation : MonoBehaviour
    {
       [SerializeField] private GameObject _endOfAuction;
       [SerializeField] private TextMeshProUGUI _text;

       [SerializeField] private ChangeScene _scene;

        private void Awake()
        {
            AuctionsManager.OnAuctionFinished += MineFinished;
        }


        private void MineFinished(uint roomId, string winner)
        {
            if(roomId == AuctionsManager.CurrentAuctionRoom.Id)
            {
                _endOfAuction.SetActive(true);

                if(winner.Length > 0)
                {
                    _text.text = "The winner is " + winner;
                }
                else
                {
                    _text.text = "No winner";
                }
                
            }
        }


        private void OnDestroy()
        {
            AuctionsManager.OnAuctionFinished -= MineFinished;
        }
    }
}