using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraActions;
using AuxiliaryClasses;


public class BackgroundParalax : MonoBehaviour
{
    [SerializeField] private float _minXValue;
    [SerializeField] private float _maxXValue;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector2(_minXValue, gameObject.transform.position.y), new Vector2(_maxXValue, gameObject.transform.position.y));
    }

    // Start is called before the first frame update
    private void Awake()
    {
        PanPinchCameraMovement.OnCameraMovement += ChangeCoordonatesForParalax;
    }

    // Update is called once per frame
    private void ChangeCoordonatesForParalax(float x)
    {
        float temp = AuxiliaryMethods.Scale(-50, 50, _minXValue, _maxXValue, x);
        transform.position = new Vector3(temp, transform.position.y, transform.position.z);
    }
}
