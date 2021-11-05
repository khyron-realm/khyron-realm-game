using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraActions
{
    public class ActivateCamera : MonoBehaviour
    {
        [SerializeField] private PanPinchCameraMovement _cameraMovement;
        [SerializeField] private Camera _camera;

        public static float PreviousOrthoSize = 16f;
        public static Vector3 Position = Vector3.forward * -10;

        private void OnEnable()
        {
            _cameraMovement.enabled = true;
            _camera.orthographicSize = PreviousOrthoSize;
            _camera.transform.position = Position;
        }

        private void OnDisable()
        {
            if(_camera != null)
            {
                PreviousOrthoSize = _camera.orthographicSize;
                Position = _camera.transform.position;
            }           
        }
    }
}