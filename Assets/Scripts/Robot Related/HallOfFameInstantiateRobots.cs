using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;


namespace Manager.PresentRobots
{
    public class HallOfFameInstantiateRobots : MonoBehaviour
    {
        [SerializeField] private GameObject _templateToInstantiate;
        [SerializeField] private GameObject _canvas;


        private void Start()
        {
            foreach(Robot item in RobotsManager.robots)
            {
                GameObject newPanel = Instantiate(_templateToInstantiate);
                newPanel.transform.SetParent(_canvas.transform, false);
            }
        }
    }
}