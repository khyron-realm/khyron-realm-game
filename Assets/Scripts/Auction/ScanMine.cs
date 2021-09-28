using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TilesData;

public class ScanMine : MonoBehaviour
{
    [SerializeField] private int _vision;
    [SerializeField] private bool _showScannedArea;

    private HashSet<Vector3Int> _blocksToDiscover;

    private void Awake()
    {
        _blocksToDiscover = new HashSet<Vector3Int>();
    }


    public void Discover(Vector3Int temp)
    {
        CreateCircleVision();

        foreach (Vector3Int item in _blocksToDiscover)
        {
            if(StoreAllTiles.instance.Tilemap.GetTile(temp + item) != null && _showScannedArea)
            {
                StoreAllTiles.instance.Tilemap.SetColor(temp + item, Color.red);
            }

            if (StoreAllTiles.instance.Tilemap.GetTile(temp + item) != null && StoreAllTiles.instance.tiles[temp.x + item.x][temp.y + item.y].Resource != null)
            {             
               StoreAllTiles.instance.Tilemap.SetTile(temp + item, StoreAllTiles.instance.tiles[temp.x + item.x][temp.y + item.y].Resource.ResourceTile);
            }
        }
    }


    // Calculate the scan
    private void VisionField(int length, Vector3Int one, Vector3Int two)
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length - i; j++)
            {
                _blocksToDiscover.Add(i * one + j * two);
            }
        }
    }


    private void CreateCircleVision()
    {
        VisionField(_vision, Vector3Int.up, Vector3Int.left);
        VisionField(_vision, Vector3Int.up, Vector3Int.right);
        VisionField(_vision, Vector3Int.down, Vector3Int.left);
        VisionField(_vision, Vector3Int.down, Vector3Int.right);
    }
}