using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace AuxiliaryClasses
{
    public class MovingObjects : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private float _initPosition;
        [SerializeField] private float _lastPosition;

        [SerializeField] private Ease _easeType;
        [SerializeField] private float _duration;
        #endregion

        #region "Private members"

        private Sequence _mySequence;

        #endregion

        private void OnDrawGizmosSelected()
        {
            if(_initPosition < _lastPosition)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawLine(new Vector2(_initPosition, gameObject.transform.position.y), new Vector2(_lastPosition, gameObject.transform.position.y));
        }


        private void Awake()
        {
            _mySequence = DOTween.Sequence();
            _mySequence.Append(transform.DOMoveX(_lastPosition, _duration).SetEase(_easeType).OnComplete(Method));
            
        }

        private void Method()
        {
            transform.position = new Vector3(_initPosition, transform.position.y, transform.position.z);
            _mySequence.Restart();
        }
    }
}