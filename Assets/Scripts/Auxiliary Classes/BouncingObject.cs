using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BouncingObject : MonoBehaviour
{
    [SerializeField] private bool local;

    [SerializeField] private float positionDiff;
    [SerializeField] private float time;

    private void Start()
    {
        float value = Random.Range(0f, 2.0f);

        if (local)
        {
            transform.DOLocalMoveY(transform.localPosition.y - positionDiff, time).SetDelay(value).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            transform.DOMoveY(transform.position.y - positionDiff, time).SetDelay(value).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }       
    }
}