using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraActions
{
    /// <summary>
    /// IN progress
    /// </summary>
    public class UserTouch : MonoBehaviour
    {
        [Header("The threshold value of deltaPosition so touch phase can be considered [Let some tolerances]")]
        [SerializeField] private float _touchSensitivity; //10

        // Panel that represent the touch area
        public static RectTransform touchArea;
        public static float touchSensitivity;

        private void Awake()
        {
            touchSensitivity = _touchSensitivity;
        }

        // Position in world coordonates of the touch
        public static Vector3 TouchPosition(int touchNumber)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(touchNumber);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                return touchPosition;
            }
            else
            {
                return new Vector3Int(-99999, -99999, -99999);
            }
        }
        public static Vector3Int TouchPositionInt(int touchNumber, bool temp = true)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(touchNumber);
                return new Vector3Int((int)Camera.main.ScreenToWorldPoint(touch.position).x, (int)Camera.main.ScreenToWorldPoint(touch.position).y, 0);
            }

            return new Vector3Int(-99999, -99999, -99999);
        }


        // Phases of touch
        public static bool TouchPhaseBegan(int touchNumber)
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(touchNumber).phase == TouchPhase.Began ? true : false;
            }
            return false;
        }

        public static bool TouchPhaseMoved(int touchNumber)
        {
            if (Input.touchCount > 0)
            {
                if (Mathf.Abs(Input.GetTouch(0).deltaPosition.x) > 20 || Mathf.Abs(Input.GetTouch(0).deltaPosition.y) > 20)
                {
                    return Input.GetTouch(touchNumber).phase == TouchPhase.Moved ? true : false;
                }

            }
            return false;
        }

        public static bool TouchPhaseEnded(int touchNumber)
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(touchNumber).phase == TouchPhase.Ended ? true : false;
            }
            return false;
        }
    }
}