using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    private IMove _moveComponent;

    private void Awake()
    {
        _moveComponent = GetComponent<IMove>();
        _moveComponent.OnMoving += Discover;
    }

    private void Discover()
    {
        Vector3Int temp = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, 0);

        List<Vector3Int> ways = new List<Vector3Int>
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.right,
            Vector3Int.left,
            Vector3Int.up + Vector3Int.left,
            Vector3Int.up + Vector3Int.right,
            Vector3Int.down + Vector3Int.left,
            Vector3Int.down + Vector3Int.right,
        };

        for (int i = 0; i < 8; i++)
        {
            if(StoreAllTiles.instance.tiles[temp.x + ways[i].x][temp.y + ways[i].y].Resource != null && StoreAllTiles.instance.Tilemap.GetTile(temp + ways[i]) != null)
            {
                StoreAllTiles.instance.Tilemap.SetTile(temp + ways[i], StoreAllTiles.instance.tiles[temp.x + ways[i].x][temp.y + ways[i].y].Resource.resourceTile);
                StoreAllTiles.instance.Tilemap.SetColor(temp + ways[i], Color.white);
            }
            else
            {
                StoreAllTiles.instance.Tilemap.SetColor(temp + ways[i], Color.white);
            }         
        }
    }
}