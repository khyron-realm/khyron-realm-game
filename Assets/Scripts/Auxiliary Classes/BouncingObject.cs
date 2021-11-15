using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BouncingObject : MonoBehaviour
{
    [SerializeField] private float positionDiff;
    [SerializeField] private float time;

    private void Start()
    {
        transform.DOMoveY(transform.position.y - positionDiff, time).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}