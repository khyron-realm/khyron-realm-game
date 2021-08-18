using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserTouch : MonoBehaviour
{
    public static Vector3 TouchPosition(int touchNumber)
    {
        Touch touch = Input.GetTouch(touchNumber);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        return touchPosition;
    }
}