using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;


namespace Manager.Robots
{
    public class HallOfFameInstantiateRobots : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private GameObject _templateToInstantiate;
        [SerializeField] private GameObject _canvas;
        #endregion

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