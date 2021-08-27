using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBlockCollider : MonoBehaviour
{
    [SerializeField]
    private float _ortoMin;

    [SerializeField]
    private float _ortoMax;

    [SerializeField]
    private float _minColliderSize;

    [SerializeField]
    private float _maxColliderSize;

    private void Awake()
    {
        PanPinch.OnChangingOrto += ChangeColliderSize;
    }

    private void ChangeColliderSize()
    {
        float tempValueAxis = AuxiliaryMethods.Scale(_ortoMin, _ortoMax, _minColliderSize, _maxColliderSize, Camera.main.orthographicSize);
        Vector2 temp = new Vector2(tempValueAxis, tempValueAxis);

        foreach (BoxCollider2D item in CreateCommandBlock.commandBlocks)
        {
            item.size = temp;
        } 
    }
}