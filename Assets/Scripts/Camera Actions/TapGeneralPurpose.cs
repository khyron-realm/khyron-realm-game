using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CameraActions
{
    public class TapGeneralPurpose : MonoBehaviour
    {
        [SerializeField] private float tapTimeThreshold;
        [SerializeField] private float _sensitivity;

        private float timeTouchBegan;
        private bool touchDidMove;

        public static TapGeneralPurpose Instance;
        public static event Action<Vector3, bool> OnTapDetected;

        private void Awake()
        {
            Instance = this;
        }

        public IEnumerator CheckForTap()
        {
            while(true)
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(0))
                    {
                        timeTouchBegan = Time.time;
                        touchDidMove = false;
                    }

                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        if (Mathf.Abs(Input.GetTouch(0).deltaPosition.x) > _sensitivity || Mathf.Abs(Input.GetTouch(0).deltaPosition.y) > _sensitivity)
                        {
                            touchDidMove = true;
                        }
                    }

                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        float tapTime = Time.time - timeTouchBegan;

                        if (tapTime <= tapTimeThreshold && touchDidMove == false)
                        {
                            OnTapDetected?.Invoke(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), true);
                        }
                    } 
                }
                yield return null;
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}