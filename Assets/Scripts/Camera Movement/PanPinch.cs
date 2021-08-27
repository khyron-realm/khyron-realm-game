using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanPinch : MonoBehaviour
{
    [SerializeField]
    private GameObject _camera;
    private Camera _lineCamera;

    [SerializeField]
    private float limitXMin;

    [SerializeField]
    private float limitXMax;

    [SerializeField]
    private float limitYMin;

    [SerializeField]
    private float limitYMax;

    [SerializeField]
    private float orthoMin;

    [SerializeField]
    private float orthoMax;

    [SerializeField]
    private int _minimapActivationSensibility = 120;

    private bool _touchingRobot = false;

    private bool _minimapActivated = false;

    private float resolutionRatio;

    // Raised when the orthographic size of the camera is changed
    public static event Action OnChangingOrto;

    // If orto is > than maxOrto, activiate the minimap view
    public static event Action<bool> OnMinimapActivation;

    private void Awake()
    {
        CommandBlockHandler.OnGivingCommand += SetTouchingRobot;
        resolutionRatio = Screen.width / Screen.height;
        _lineCamera = _camera.GetComponent<Camera>();
    }

    private void Update()
    {
        if(_touchingRobot == false)
        {
            Panning();
            Pinching();
        }
        else
        {
            _touchingRobot = false;
        }
    }

    private void Panning()
    {
        // One finger on the screen [Pan]
        if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).phase == TouchPhase.Moved && _minimapActivated == false)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            PanningFunction(touchDeltaPosition);
        }
    }

    private void Pinching()
    {
        if (Input.touchCount > 1)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            CheckIfMinimapIsActivated(ref touchZero, ref touchOne);

            if (_minimapActivated == false)
            {
                PanningFunction((touchZero.deltaPosition + touchOne.deltaPosition) / 2);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                float zoomSpeed = AuxiliaryMethods.Scale(orthoMin, orthoMax, 0.0052f, 0.0162f, Camera.main.orthographicSize);
                
                Camera.main.orthographicSize += deltaMagnitudeDiff * zoomSpeed;
                _lineCamera.orthographicSize += deltaMagnitudeDiff * zoomSpeed;          

                OnChangingOrto?.Invoke();

                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthoMin, orthoMax);
                _lineCamera.orthographicSize = Mathf.Clamp(_lineCamera.orthographicSize, orthoMin, orthoMax);
            }
        }
    }

    private void CheckIfMinimapIsActivated(ref Touch touchZero, ref Touch touchOne)
    {
        if (Mathf.Abs((touchZero.deltaPosition - touchOne.deltaPosition).x) > _minimapActivationSensibility && Camera.main.orthographicSize > orthoMax - 1)
        {
            if (UserTouch.TouchPhaseEnded(0))
            {
               _minimapActivated = !_minimapActivated;
               OnMinimapActivation?.Invoke(_minimapActivated);
            }
        }
    }

    private void PanningFunction(Vector2 touchDeltaPosition)
    {
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 1f);
        Vector3 screenTouch = screenCenter + new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 0f);

        Vector3 worldCenterPosition = Camera.main.ScreenToWorldPoint(screenCenter);
        Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(screenTouch);

        Vector3 worldDeltaPosition = worldTouchPosition - worldCenterPosition;

        transform.Translate(-worldDeltaPosition);

        float x = Mathf.Clamp(transform.position.x, limitXMin + Camera.main.orthographicSize * resolutionRatio, limitXMax - Camera.main.orthographicSize * resolutionRatio);
        float y = Mathf.Clamp(transform.position.y, limitYMin + Camera.main.orthographicSize, limitYMax - Camera.main.orthographicSize);

        transform.position = new Vector3(x, y, -10f);
    }

    private void SetTouchingRobot()
    {
        _touchingRobot = true;
    }
}