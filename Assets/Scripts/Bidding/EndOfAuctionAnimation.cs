using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;
using Scenes;

namespace Bidding
{
    public class EndOfAuctionAnimation : MonoBehaviour
    {
        [SerializeField] private ChangeScene _scene;

        private void Awake()
        {
            AuctionsManager.OnAuctionFinished += MineFinished;
        }


        private void MineFinished(uint roomId, uint winner)
        {
            if(roomId == AuctionsManager.CurrentAuctionRoom.Id)
            {
                _scene.GoToScene();
            }
        }


        private void OnDestroy()
        {
            AuctionsManager.OnAuctionFinished -= MineFinished;
        }
    }
}