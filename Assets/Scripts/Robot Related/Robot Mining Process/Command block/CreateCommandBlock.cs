using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCommandBlock
{
    private GameObject _commandBlock;

    public static List<BoxCollider2D> commandBlocks = new List<BoxCollider2D>();

    public void Create(Vector3 position, Vector2 collidersize)
    {
        _commandBlock = new GameObject("MinerController");

        _commandBlock.transform.position = new Vector3(position.x, position.y, position.z);
        _commandBlock.layer = 10;

        _commandBlock.AddComponent<CommandBlockHandler>();
        _commandBlock.AddComponent<SpriteRenderer>();
        _commandBlock.AddComponent<BoxCollider2D>().isTrigger = true;
        _commandBlock.GetComponent<BoxCollider2D>().size = collidersize;

        commandBlocks.Add(_commandBlock.GetComponent<BoxCollider2D>());

        _commandBlock.GetComponent<SpriteRenderer>().sortingLayerName = "WayPoint";

        _commandBlock.SetActive(false);
    }

    public GameObject GetBlock()
    {
        return _commandBlock;
    }   
}