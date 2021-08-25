using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DrawPathToFollowForRobot : MonoBehaviour
{
    private LineRenderer _line;
    private HashSet<Vector3> _points;

    private void Awake()
    {
        _points = new HashSet<Vector3>();
        _line = GetComponent<LineRenderer>();
    }

    public void CreatePath(List<List<Vector3>> allHits, bool preview = false, List<Vector3> hitsPreview = null)
    {
        _points.Clear();

        if(allHits.Count < 1)
        {
            _points.Add(gameObject.transform.position);
        }

        for (int i = 0; i < allHits.Count; i++)
        {
            _points.UnionWith(allHits[i]);
        }

        if (preview)
        {
            _points.UnionWith(hitsPreview);
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