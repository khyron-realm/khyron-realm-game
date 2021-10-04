using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace AuxiliaryClasses
{
    public class MovingObjects : MonoBehaviour
    {
        [SerializeField] private float _initPosition;
        [SerializeField] private float _lastPosition;

        [SerializeField] private float _time;

        private Sequence _tween;
        private Vector2 _initVector;

        private void Awake()
        {
            _tween = DOTween.Sequence();

            _initVector = new Vector2(_initPosition, gameObject.transform.position.y);

            _tween.OnStart(() => { gameObject.transform.position = _initVector; });
            _tween.Append(gameObject.transform.DOMoveX(_lastPosition, _time));          
            _tween.SetLoops(-1);

            _tween.Restart();
        }
    }
}