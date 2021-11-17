using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;
using Networking.Chat;
using Networking.Mines;
using Networking.Tags;
using Mine;


public class GetMinesFromServer : MonoBehaviour
{
    [SerializeField] private List<MineTouched> _mines;

    private void Awake()
    {
        MineManager.GetUserMines();
        MineManager.OnReceivedMines += SetForEachMine;
    }


    private void SetForEachMine()
    {
        for (byte i = 0; i < MineManager.MineList.Count; i++)
        {
            _mines[i].index = i;
            _mines[i].HasMine = true;
            _mines[i].IsAuction = false;
        }

        for (int i = MineManager.MineList.Count; i < _mines.Count; i++)
        {
            _mines[i].index = 255;
            _mines[i].HasMine = false;
            _mines[i].IsAuction = false;
        }
    }


    private void OnDestroy()
    {
        MineManager.OnReceivedMines -= SetForEachMine;
    }
}