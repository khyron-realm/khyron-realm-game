using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeColorBasedOnHealth : MonoBehaviour
{
    [SerializeField] private GameObject _color;

    private SpriteRenderer _renderer;

    private Sequence _seq;

    void Start()
    {
        _seq = DOTween.Sequence();

        _renderer = _color.GetComponent<SpriteRenderer>();
        _renderer.color = Color.green;

        _seq.Append(_renderer.DOColor(new Color(0.5f, 1, 0, 1), 5f));
        _seq.Append(_renderer.DOColor(new Color(1, 1, 0, 1), 5f));
        _seq.Append(_renderer.DOColor(new Color(1, 0.5f, 0, 1), 5f));
        _seq.Append(_renderer.DOColor(Color.red, 5f));
    }
}