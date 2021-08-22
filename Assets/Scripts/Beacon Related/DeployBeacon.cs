using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployBeacon : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
       // _button.onClick.AddListener(Deploy);
    }

    //private IEnumerator Deploy()
    //{
    //    bool check = true;

    //    while (check)
    //    {
    //        Vector3Int temp = UserTouch.TouchPositionInt(0, UserTouch.touchArea);
    //        Vector3Int nullVector = new Vector3Int(-99999, -99999, -99999);

    //        if (temp != nullVector && UserTouch.TouchPhaseEnded(0))
    //        {
    //            StoreAllTiles.instance.Tilemap.SetTile(temp, null);
    //            StoreAllTiles.instance.tiles[temp.x][temp.y].Health = -1;

    //            _robot.SetActive(true);
    //            _robot.GetComponent<RobotManager>().commandBlock.SetActive(true);

    //            _robot.GetComponent<RobotManager>().commandBlock.transform.position = new Vector3(temp.x + 0.5f, temp.y + 0.5f, -6f);
    //            _robot.transform.position = new Vector3(temp.x + 0.5f, temp.y + 0.5f, 0f);

    //            _button.onClick.RemoveListener(StartDeployOperation);

    //            OnDeployed?.Invoke();

    //            check = false;
    //        }
    //        yield return null;
    //    }
    //}
}
