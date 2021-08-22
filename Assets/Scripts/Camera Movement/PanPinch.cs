using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanPinch : MonoBehaviour
{
    private float speed;

    public float limitXMin = -12f;
    public float limitXMax = 52f;

    public float limitYMin = -24f;
    public float limitYMax = 10f;

    public float orthoMin = 4f;
    public float orthoMax = 16f;

    public float canvasButtonsWidth;


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 58;
    }


    void Update()
    { 
        // One finger on the screen [Pan]
        if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if(Input.GetTouch(0).position.x > canvasButtonsWidth)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //float deltaX = Scale(0, 1080 * Camera.main.aspect, 0, Camera.main.orthographicSize * 2 * Camera.main.aspect, touchDeltaPosition.x);
                //float deltaY = Scale(0, 1080, 0, Camera.main.orthographicSize * 2 , touchDeltaPosition.y);

                transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
                //transform.Translate(-deltaX, -deltaY, 0);

                float x = Mathf.Clamp(transform.position.x, limitXMin + Camera.main.orthographicSize * 1.77f, limitXMax - Camera.main.orthographicSize * 1.77f);
                float y = Mathf.Clamp(transform.position.y, limitYMin + Camera.main.orthographicSize, limitYMax - Camera.main.orthographicSize);

                transform.position = new Vector3(x, y, -10f);
            }
        }


        // Two or more fingers on the screen [Pan + Pinch]
        if (Input.touchCount > 1)
        {
            if(Input.GetTouch(0).position.x > canvasButtonsWidth && Input.GetTouch(1).position.x >canvasButtonsWidth)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);


                transform.Translate((-touchZero.deltaPosition.x - touchOne.deltaPosition.x) * speed, (-touchZero.deltaPosition.y - touchOne.deltaPosition.y) * speed, 0);

                float x = Mathf.Clamp(transform.position.x, limitXMin + Camera.main.orthographicSize * 1.77f, limitXMax - Camera.main.orthographicSize * 1.77f);
                float y = Mathf.Clamp(transform.position.y, limitYMin + Camera.main.orthographicSize, limitYMax - Camera.main.orthographicSize);

                transform.position = new Vector3(x, y, -10f);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                float zoomSpeed = Scale(orthoMin, orthoMax, 0.0028f, 0.0072f, Camera.main.orthographicSize);
                Camera.main.orthographicSize += deltaMagnitudeDiff * zoomSpeed;

                Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthoMin, orthoMax);

                speed = Scale(orthoMin, orthoMax, 0.0036f, 0.015f, Camera.main.orthographicSize);
            } 
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
}