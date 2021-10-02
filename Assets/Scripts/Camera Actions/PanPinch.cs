using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CameraActions
{
    /// <summary>
    /// [NOT FINISHED --- IN WORK BY CODRIN ]
    /// </summary>
    public class PanPinch : MonoBehaviour
    {
        [SerializeField]
        [Header("Camera that is child of the main camera")]
        private GameObject _camera;
        private Camera _lineCamera;

        [SerializeField]
        [Header("X Inferior limit")]
        private float _limitXMin;

        [SerializeField]
        [Header("X Superior limit")]
        private float _limitXMax;

        [SerializeField]
        [Header("Y Inferior limit")]
        private float limitYMin;

        [SerializeField]
        [Header("Y Superior limit")]
        private float limitYMax;

        [SerializeField]
        [Header("Minimum orthographic size")]
        private float orthoMin;

        [SerializeField]
        [Header("Maximum orthographic size")]
        private float orthoMax;


        private void Awake()
        {
            _lineCamera = _camera.GetComponent<Camera>();
        }

        private void Update()
        {
                Panning();
        }

        private void Panning()
        {
            // One finger on the screen [Pan]
            
            {
                if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    PanningFunction(touchDeltaPosition);
                }
            }
        }

        private void Pinching()
        {
            if (Input.touchCount > 1)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                if (Camera.main.orthographicSize <= orthoMax)
                {
                    PanningFunction((touchZero.deltaPosition + touchOne.deltaPosition) / 2);

                    Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                    float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                    float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                    float zoomSpeed = AuxiliaryMethods.AuxiliaryMethods.Scale(orthoMin, orthoMax, 0.0052f, 0.0162f, Camera.main.orthographicSize);

                    Camera.main.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
                    _lineCamera.orthographicSize += deltaMagnitudeDiff * zoomSpeed;


                    Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthoMin, orthoMax);
                    _lineCamera.orthographicSize = Mathf.Clamp(_lineCamera.orthographicSize, orthoMin, orthoMax);
                }
            }
        }

        private void PanningFunction(Vector2 touchDeltaPosition)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(UserTouch.touchArea, Input.GetTouch(0).position))
            {
                Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 1f);
                Vector3 screenTouch = screenCenter + new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 0f);

                Vector3 worldCenterPosition = Camera.main.ScreenToWorldPoint(screenCenter);
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(screenTouch);

                Vector3 worldDeltaPosition = worldTouchPosition - worldCenterPosition;

                transform.Translate(-worldDeltaPosition);
            }
        }
    }
}