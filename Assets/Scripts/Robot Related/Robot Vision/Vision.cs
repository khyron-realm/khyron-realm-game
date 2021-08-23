using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    private IMove _moveComponent;

    private HashSet<Vector3Int> _blocksToDiscover;

    private int _vision;

    private void Awake()
    {
        _vision = GetComponent<RobotManager>().robot.fieldOfVision;
        _blocksToDiscover = new HashSet<Vector3Int>();
        _moveComponent = GetComponent<IMove>();
        _moveComponent.OnMoving += Discover;
    }

    private void Discover()
    {
        Vector3Int temp = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, 0);

        CreateCircleVision();

        foreach (Vector3Int item in _blocksToDiscover)
        {
            if (StoreAllTiles.instance.tiles[temp.x + item.x][temp.y + item.y].Resource != null && StoreAllTiles.instance.Tilemap.GetTile(temp + item) != null)
            {
                StoreAllTiles.instance.Tilemap.SetTile(temp + item, StoreAllTiles.instance.tiles[temp.x + item.x][temp.y + item.y].Resource.resourceTile);
                StoreAllTiles.instance.Tilemap.SetColor(temp + item, Color.white);
            }
            else
            {
                StoreAllTiles.instance.Tilemap.SetColor(temp + item, Color.white);
            }
        }
    }

    private void VisionFiled(int length, Vector3Int one, Vector3Int two)
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
        VisionFiled(_vision, Vector3Int.up, Vector3Int.left);
        VisionFiled(_vision, Vector3Int.up, Vector3Int.right);
        VisionFiled(_vision, Vector3Int.down, Vector3Int.left);
        VisionFiled(_vision, Vector3Int.down, Vector3Int.right);
    }
}