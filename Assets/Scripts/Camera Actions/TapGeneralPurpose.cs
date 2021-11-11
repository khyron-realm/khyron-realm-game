using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CameraActions
{
    public class TapGeneralPurpose : MonoBehaviour
    {
        private float timeTouchBegan;
        private bool  touchDidMove;
        private float tapTimeThreshold = 0.2f;

        public static event Action<Vector3> OnTapDetected;

        private void Update()
        {
            if(Input.touchCount < 1) return;

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                timeTouchBegan = Time.time;
                touchDidMove = false;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                touchDidMove = true;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                float tapTime = Time.time - timeTouchBegan;

                if (tapTime <= tapTimeThreshold && touchDidMove == false)
                {
                    OnTapDetected?.Invoke(Input.GetTouch(0).position);
                }
            }           
        }
    }
}