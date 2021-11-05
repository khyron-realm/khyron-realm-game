using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraActions
{
    public class DezactivateCamera : MonoBehaviour
    {
        [SerializeField] private PanPinchCameraMovement _cameraMovement;
        [SerializeField] private Camera _camera;

        void OnEnable()
        {
            _cameraMovement.enabled = false;
            _camera.orthographicSize = 12f;
            _camera.transform.position = new Vector3(0, 0, _camera.transform.position.z);
        }
    }
}