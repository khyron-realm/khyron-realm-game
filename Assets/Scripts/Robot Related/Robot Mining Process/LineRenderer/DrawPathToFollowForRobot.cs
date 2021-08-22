using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPathToFollowForRobot : MonoBehaviour
{
    private LineRenderer _line;
    private List<Vector3> _points;

    private void Awake()
    {
        _points = new List<Vector3>();
        _line = GetComponent<LineRenderer>();
    }

    public void CreatePath(List<List<Vector3>> allHits, bool preview = false, List<Vector3> hitsPreview = null)
    {
        _points.Clear();

        if(allHits.Count == 0)
        {
            _points.Add(gameObject.transform.position);
        }

        for (int i = 0; i < allHits.Count; i++)
        {
            for (int j = 0; j < allHits[i].Count; j++)
            {
                _points.Add(new Vector3(allHits[i][j].x, allHits[i][j].y, 0));
            }
        }

        if (preview)
        {
            foreach(Vector3 item in hitsPreview)
            {
                _points.Add(item);
            }
        }

        _line.positionCount = _points.Count;
        _line.SetPositions(_points.ToArray());
    }

    public void SetWayPoint(SpriteRenderer image, int count)
    {
        if (count > 0)
        {
            image.sprite = RobotsHandler.WayPoints[count - 1];
        }
        else
        {
            image.sprite = null;
        }
    }

    public void DeleteLine()
    {
        _points.Clear();
        _line.positionCount = _points.Count;
    }
}