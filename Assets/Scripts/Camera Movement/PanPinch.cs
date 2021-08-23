using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanPinch : MonoBehaviour
{
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

    private bool _touchingRobot = false;

    private float resolutionRatio;

    private void Awake()
    {
        CommandBlockHandler.OnGivingCommand += SetTouchingRobot;
        resolutionRatio = Screen.width / Screen.height;
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
        if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
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

            PanningFunction((touchZero.deltaPosition + touchOne.deltaPosition) / 2);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float zoomSpeed = Scale(orthoMin, orthoMax, 0.0052f, 0.0162f, Camera.main.orthographicSize);
            Camera.main.orthographicSize += deltaMagnitudeDiff * zoomSpeed;

            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthoMin, orthoMax);
        }
    }

    // Map value 
    public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    private void SetTouchingRobot()
    {
        _touchingRobot = true;
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
}