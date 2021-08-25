using UnityEngine;

public class CreateCommandBlock
{
    private GameObject _commandBlock;

    public void Create(Vector3 position, Vector2 collidersize)
    {
        _commandBlock = new GameObject("MinerController");
        
        _commandBlock.transform.position = new Vector3(position.x, position.y, position.z);
        _commandBlock.layer = 10;

        _commandBlock.AddComponent<CommandBlockHandler>();
        _commandBlock.AddComponent<SpriteRenderer>();
        _commandBlock.AddComponent<BoxCollider2D>().isTrigger = true;
        _commandBlock.GetComponent<BoxCollider2D>().size = collidersize;

        _commandBlock.GetComponent<SpriteRenderer>().sortingLayerName = "WayPoint";

        _commandBlock.SetActive(false);
    }

    public GameObject GetBlock()
    {
        return _commandBlock;
    }   
}