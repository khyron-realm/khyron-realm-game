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

        [SerializeField] private float _time;
        #endregion

        #region "Private members"

        private Vector2 _initVector;
        private Vector2 _lastVector;

        private float _temp;

        #endregion

        private void Awake()
        {
            _temp = 0;

            _initVector = new Vector2(_initPosition, gameObject.transform.position.y);
            _lastVector = new Vector2(_lastPosition, gameObject.transform.position.y);       
        }


        private void Update()
        {   
            if (_temp < 1)
            {
                _temp += Time.deltaTime / _time;
                gameObject.transform.position = Vector2.Lerp(_initVector, _lastVector, _temp); ;
            }

            if(_temp > 1)
            {
                _initVector = new Vector2(_initPosition, gameObject.transform.position.y);
                _temp = 0;
            }         
        }
    }
}