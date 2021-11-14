using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Mines;
using Networking.Headquarters;
using Tiles.Tiledata;
using Scenes;


public class ManageMineHandler : MonoBehaviour
{
    [SerializeField] private ChangeScene _scene;

    private static bool[] s_validBlocks;


    private void Awake()
    {
        s_validBlocks = new bool[45 * 30];

        MineManager.OnSaveMine += MineSaved;
        MineManager.OnSaveMineFailed += MineSavedFailed;

        MineManager.OnFinishMine += FinishMineCompleted;
        MineManager.OnFinishMineFailed += MineSavedFailed;
    }


    public void SavePlayerMine()
    {       
        // x:45 by y:30
        for (int i = 0; i < 45; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                if(StoreAllTiles.Instance.Tilemap.GetTile(new Vector3Int(i,j,0)) == DataOfTile.NullTile)
                {
                    s_validBlocks[i * 30 + j] = false;
                }
                else
                {
                    s_validBlocks[i * 30 + j] = true;
                }
            }
        }

        Debug.LogWarning("Pressed");
        MineManager.SavePlayerMine(MineManager.MineList[MineManager.CurrentMine].Id, s_validBlocks, HeadquartersManager.Player.Robots, HeadquartersManager.Player.Resources);
    }
    private void MineSaved()
    {
        Debug.LogWarning("Back");
        _scene.GoToScene();
    }
    private void MineSavedFailed(byte errorId)
    {
        Debug.LogWarning(errorId);
    }


    public void FinishMine()
    {
        Debug.LogWarning("Pressed");
        MineManager.FinishPlayerMine(MineManager.MineList[MineManager.CurrentMine].Id, HeadquartersManager.Player.Robots, HeadquartersManager.Player.Resources);
    }
    private void FinishMineCompleted()
    {
        Debug.LogWarning("Finished Mine");
        _scene.GoToScene();
    }


    private void OnDestroy()
    {
        MineManager.OnSaveMine -= MineSaved;
        MineManager.OnFinishMine -= FinishMineCompleted;
        MineManager.OnFinishMineFailed += MineSavedFailed;
    }
}