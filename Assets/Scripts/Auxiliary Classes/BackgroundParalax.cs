using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraActions;


namespace AuxiliaryClasses
{
    public class BackgroundParalax : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private float _minXValue;
        [SerializeField] private float _maxXValue;
        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector2(_minXValue, gameObject.transform.position.y), new Vector2(_maxXValue, gameObject.transform.position.y));
        }

        private void Awake()
        {
            PanPinchCameraMovement.OnCameraMovement += ChangeCoordonatesForParalax;
        }


        private void ChangeCoordonatesForParalax(float x)
        {
            float temp = AuxiliaryMethods.Scale(-50, 50, _minXValue, _maxXValue, x);
            transform.position = new Vector3(temp, transform.position.y, transform.position.z);
        }


        private void OnDestroy()
        {
            PanPinchCameraMovement.OnCameraMovement -= ChangeCoordonatesForParalax;
        }
    }
}