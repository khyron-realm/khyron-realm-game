using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployBeacon : MonoBehaviour
{
    public static GameObject beacon;

    private Button _button;

    private List<Vector3Int> directions;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(StartDeploy);

        directions = new List<Vector3Int>()
        {
            Vector3Int.zero,
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right,
            Vector3Int.up + Vector3Int.left,
            Vector3Int.up + Vector3Int.right,
            Vector3Int.down + Vector3Int.left,
            Vector3Int.down + Vector3Int.right,
        };
    }

    private void StartDeploy()
    {
        StartCoroutine("Deploy");
    }

    private void StopDeploy()
    {
        StopCoroutine("Deploy");
    }

    private IEnumerator Deploy()
    {
        bool check = true;

        while (check)
        {
            Vector3Int temp = UserTouch.TouchPositionInt(0, UserTouch.touchArea);
            Vector3Int nullVector = new Vector3Int(-99999, -99999, -99999);

            if (temp != nullVector && UserTouch.TouchPhaseEnded(0))
            {
                foreach (Vector3Int item in directions)
                {
                    StoreAllTiles.instance.Tilemap.SetTile(temp + item, null);
                    StoreAllTiles.instance.tiles[temp.x + item.x][temp.y + item.y].Health = -1;
                }

                beacon.SetActive(true);
                beacon.transform.position = temp + new Vector3(0.5f, 0.5f, 0);

                _button.onClick.RemoveListener(StartDeploy);

                check = false;
            }
            yield return null;
        }
    }
}