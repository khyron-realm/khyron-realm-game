using System.Collections.Generic;
using UnityEngine;
using Networking.Auctions;
using Networking.Chat;
using Networking.Mines;
using Networking.Headquarters;
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


    // Mine index
    private void SetForEachMine()
    {
        foreach (MineTouched item in _mines)
        {
            item.IndexPosition = 255;
            item.HasMine = false;
            item.IsAuction = false;
        }

        for (byte i = 0; i < MineManager.MineList.Count; i++)
        {
            if(MineManager.MineList[i].MapPosition != 255)
            {
                _mines[MineManager.MineList[i].MapPosition].IndexPosition = i;
                _mines[MineManager.MineList[i].MapPosition].HasMine = true;
                _mines[MineManager.MineList[i].MapPosition].IsAuction = false;                
            }
            else
            {
                foreach (MineTouched item in _mines)
                {
                    if(item.HasMine == false)
                    {
                        item.IndexPosition = i;
                        item.HasMine = true;
                        item.IsAuction = false;

                        MineManager.SaveMapPosition(MineManager.MineList[i].Id, (byte)_mines.IndexOf(item));

                        break;
                    }
                }               
            }
        }
    }


    private void OnDestroy()
    {
        MineManager.OnReceivedMines -= SetForEachMine;
    }
}