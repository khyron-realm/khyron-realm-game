using System;
using System.Collections;
using UnityEngine;

// Move gameobject to desired position
namespace RobotActions
{
    public class Movement : MonoBehaviour, IMove
    {
        public event Action OnStartingMoving;
        public event Action OnMoving;

        // Used in a Coroutine to move a gameObject from point A to B in the desired maner
        public IEnumerator MoveTo(GameObject robot, Vector3 pointA, Vector3 pointB, bool smoothMovement = false)
        {
            OnStartingMoving?.Invoke();

            float temp = 0f;
            while (temp < 1f)
            {
                temp += Time.deltaTime;
                OnMoving?.Invoke();
                if (smoothMovement)
                {
                    robot.transform.position = Vector3.Lerp(pointA, pointB, temp);
                }
                else
                {
                    robot.transform.position = Vector3.Lerp(pointA, pointB, Mathf.SmoothStep(0f, 1f, temp));
                }
                yield return null;
            }
        }
    }
}