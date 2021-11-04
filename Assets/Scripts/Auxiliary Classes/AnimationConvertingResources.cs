using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AnimationConvertingResources : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] bool tt;

    [SerializeField] private GameObject _resource1;
    [SerializeField] private GameObject _resource2;
    [SerializeField] private GameObject _resource3;


    private void Awake()
    {

    }


    private void StartAnimation()
    {
        _animator.enabled = true;
    }   
    

    private void StopAnimation()
    {
        _animator.enabled = false;
    }
}
