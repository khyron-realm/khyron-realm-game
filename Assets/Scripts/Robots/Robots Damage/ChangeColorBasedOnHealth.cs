using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AuxiliaryClasses;


public class ChangeColorBasedOnHealth : MonoBehaviour
{
    [SerializeField] private GameObject _color;
    [SerializeField] private Gradient gradient;

    private SpriteRenderer _renderer;
    
    private void Awake()
    {
        _renderer = _color.GetComponent<SpriteRenderer>();
        _renderer.color = Color.green;
    }


    public void AdjustColorBasedOnHealth(int value, int max)
    {
        _renderer.color = gradient.Evaluate(AuxiliaryMethods.Scale(0, max, 0, 1, (max - value)));
    }
}