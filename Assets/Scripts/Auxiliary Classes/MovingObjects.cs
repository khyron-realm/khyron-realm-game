using UnityEngine;
using DG.Tweening;


namespace AuxiliaryClasses
{
    public class MovingObjects : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private bool _local = false;
        [SerializeField] private float _initPosition;
        [SerializeField] private float _lastPosition;

        [SerializeField] private Ease _easeType;
        [SerializeField] private float _duration;
        #endregion

        #region "Private members"
        private Sequence _mySequence;
        private float _initialXposition;

        private float _distMain;
        private float _distSecondary;

        private float _coef1;
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
            if(_local)
            {
                _initialXposition = gameObject.transform.localPosition.x;
            }
            else
            {
                _initialXposition = gameObject.transform.position.x;
            }
            
            _distMain = Mathf.Abs(_lastPosition - _initPosition);
            _distSecondary = Mathf.Abs(_lastPosition - _initialXposition);

            _coef1 = AuxiliaryMethods.Scale(0, _distMain, 0, 1, _distSecondary);

            if(_local)
            {
                _mySequence = DOTween.Sequence();
                _mySequence.Append(transform.DOLocalMoveX(_lastPosition, (_duration * _coef1)).SetEase(_easeType).OnComplete(Method1));
                _mySequence.Append(transform.DOLocalMoveX(_initialXposition, (_duration * (1 - _coef1))).SetEase(_easeType).OnComplete(Method2));
            }
            else
            {
                _mySequence = DOTween.Sequence();
                _mySequence.Append(transform.DOMoveX(_lastPosition, (_duration * _coef1)).SetEase(_easeType).OnComplete(Method1));
                _mySequence.Append(transform.DOMoveX(_initialXposition, (_duration * (1 - _coef1))).SetEase(_easeType).OnComplete(Method2));
            }
            
        }
        

        private void Method1()
        {
            if(_local)
            {
                transform.localPosition = new Vector3(_initPosition, transform.localPosition.y, transform.localPosition.z);
            }
            else
            {
                transform.position = new Vector3(_initPosition, transform.position.y, transform.position.z);
            }
                  
        }
        private void Method2()
        {
            _mySequence.Restart();
        }
    }
}