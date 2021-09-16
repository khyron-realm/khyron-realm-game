using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace CameraActions
{
    public class WholeMapView : MonoBehaviour
    {
        [SerializeField]
        [Header("List of cameras that see the minimap")]
        private List<Camera> _cameras; // first element shall be the main camera

        [SerializeField]
        private float _zoomInOutSpeed; // 1.4f

        [SerializeField]
        [Header("The position of the cameras in the moment MINIMAP is ACTIVE")]
        private Vector3 _centerPosition; //60, 38, -10

        [SerializeField]
        [Header("The orthoSize of the cameras in the moment MINIMAP is ACTIVE")]
        private int _setOrthoSizeActive;

        [SerializeField]
        [Header("The orthoSize of the cameras in the moment MINIMAP is NOT ACTIVE")]
        private int _setOrthoSizeNonActive;

        [SerializeField]
        [Header("The map with tiles")]
        [Space(20f)]
        private GameObject _map;

        [SerializeField]
        [Header("The minimap")]
        private GameObject _miniMap;

        private Vector3 _cameraPosition;

        private void Awake()
        {
            PanPinch.OnMinimapActivation += MinimapActivation;
        }

        private void MinimapActivation(bool temp)
        {
            if (temp)
            { 
                StartCoroutine("ActivateMinimap");
            }
            else
            {
                StartCoroutine("DezactivateMinimap");
            }
        }


        private IEnumerator ActivateMinimap()
        {
            _cameraPosition = _cameras[0].transform.position;

            float temp = 0f;
            while (temp < 1f)
            {
                temp += Time.deltaTime * _zoomInOutSpeed;

                foreach (Camera item in _cameras)
                {
                    item.transform.position = Vector3.Lerp(_cameraPosition, _centerPosition, Mathf.SmoothStep(0f, 1f, temp));
                    item.orthographicSize = Mathf.Lerp(_setOrthoSizeNonActive, _setOrthoSizeActive, Mathf.SmoothStep(0f, 1f, temp));
                }

                yield return null;
            }

            _map.SetActive(false);
            _miniMap.SetActive(true);

        }


        private IEnumerator DezactivateMinimap()
        {
            GetComponent<PanPinch>().enabled = false;

            float temp = 0f;
            while (temp < 1f)
            {
                temp += Time.deltaTime * _zoomInOutSpeed;

                foreach (Camera item in _cameras)
                {
                    item.transform.position = Vector3.Lerp(_centerPosition, _cameraPosition, Mathf.SmoothStep(0f, 1f, temp)); ;
                    item.orthographicSize = Mathf.Lerp(_setOrthoSizeActive, _setOrthoSizeNonActive, Mathf.SmoothStep(0f, 1f, temp));
                }

                yield return null;
            }

            GetComponent<PanPinch>().enabled = true;

            _map.SetActive(true);
            _miniMap.SetActive(false);
        }
    }
}