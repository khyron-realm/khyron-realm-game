using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class OpenChat : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private float _hidePositionPanel;

    [Space(30f)]

    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private float _hidePositionRect;

    [SerializeField] private bool gizmoToDraw;


    private static bool _hidden = true;
    private static float _initPositionPanel;
    private static float _initPositionScroll;

    private void OnDrawGizmos()
    {
        if(gizmoToDraw)
        {
            Gizmos.DrawLine(new Vector3(_hidePositionPanel, _scrollRect.transform.position.y, 0), new Vector3(_panel.transform.position.x, _panel.transform.position.y, 0));
        }
        else
        {
            Gizmos.DrawLine(new Vector3(_hidePositionRect, _scrollRect.transform.position.y, 0), new Vector3(_scrollRect.transform.position.x, _scrollRect.transform.position.y, 0));

        }
    }

    private void Awake()
    {
        _initPositionScroll = _scrollRect.transform.position.x;
        _initPositionPanel = _panel.transform.position.x;
    }

    public void ChangeModuleState()
    {
        if(_hidden)
        {
            _scrollRect.transform.DOMoveX(_hidePositionPanel, 0.6f);
            _panel.transform.DOMoveX(_hidePositionRect, 0.6f);
        }
        else
        {
            _scrollRect.transform.DOMoveX(_initPositionScroll, 0.6f);
            _panel.transform.DOMoveX(_initPositionPanel, 0.6f);
        }

        _hidden = !_hidden;
    }
}