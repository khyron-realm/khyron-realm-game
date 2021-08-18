using System;
using System.Collections;
using UnityEngine;

public interface IMove
{
    public IEnumerator MoveTo(GameObject robot, Vector3 pointA, Vector3 pointB, bool smoothMovement = false);
    public event Action OnMoving;
}
