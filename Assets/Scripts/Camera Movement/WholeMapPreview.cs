using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class WholeMapPreview : MonoBehaviour
{  
    [SerializeField]
    private int _minSize;

    [SerializeField]
    private GameObject _map;

    [SerializeField]
    private GameObject _miniMap;

    [SerializeField]
    private Sprite _points;

    //RobotsHandler.robots

    private bool _reminder = false;

    void Update()
    {
        if (Camera.main.orthographicSize > _minSize && _reminder == false)
        {
            MinimapActive();
        }
        else if(Camera.main.orthographicSize < _minSize && _reminder == true)
        {
            MinimapInactive();
        }
    }


    private void MinimapInactive()
    {
        _map.SetActive(true);
        _miniMap.SetActive(false);

        _reminder = false;
    }

    private void MinimapActive()
    {
        _map.SetActive(false);
        _miniMap.SetActive(true);

        ManageButtonsTouched.DisableAllButtons();

        _reminder = true;
    }
}