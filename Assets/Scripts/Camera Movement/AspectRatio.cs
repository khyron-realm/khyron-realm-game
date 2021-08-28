using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraActions
{
    // Set aspect ratio of a camera
    public class AspectRatio : MonoBehaviour
    {
        [SerializeField]
        private float temp;

        void Start()
        {
            GetComponent<Camera>().aspect = temp;
        }
    }
}