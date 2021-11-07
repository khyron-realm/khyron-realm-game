using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AuxiliaryClasses;
using Manager.Convert;


namespace AnimationsFloors
{
    public class AnimationConvertingResources : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Animator _animator;

        [SerializeField] private MovingObjects _resource1;
        [SerializeField] private MovingObjects _resource2;
        [SerializeField] private MovingObjects _resource3;
        #endregion


        private void Awake()
        {
            _animator.enabled = false;
            _resource1.gameObject.SetActive(false);
            _resource2.gameObject.SetActive(false);
            _resource3.gameObject.SetActive(false);

            ConvertResources.OnConversionStarted += StartAnimation;
            ConvertResources.OnConversionEnded += StopAnimation;
        }


        private void StartAnimation()
        {            
            _animator.enabled = true;
            _resource1.gameObject.SetActive(true);
            _resource2.gameObject.SetActive(true);
            _resource3.gameObject.SetActive(true);
        }


        private void StopAnimation()
        {
            _animator.enabled = false;
            _resource1.gameObject.SetActive(false);
            _resource2.gameObject.SetActive(false);
            _resource3.gameObject.SetActive(false);
        }


        private void OnDestroy()
        {
            ConvertResources.OnConversionStarted -= StartAnimation;
            ConvertResources.OnConversionEnded -= StopAnimation;
        }
    }
}