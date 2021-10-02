using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Store;
using CountDown;

namespace Manager.Convert
{
    public class ConvertResources : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private Timer _timer;
        [SerializeField] private Button _button;
        [SerializeField] private int _timeToConvert;
        #endregion

        private void Awake()
        {
            _timer.TimeTextState(false);
        }

        public void Convert()
        {
            ResourcesOperations.PayResources(30, 40, 50);
            _timer.TimeTextState(true);
            _timer.AddTime(_timeToConvert);
            _button.enabled = false;

            StartCoroutine(RunConversion());
        }

        private IEnumerator RunConversion()
        {
            int temp = 0;
            while(temp < _timeToConvert)
            {
                temp += 1;
                yield return _timer.ActivateTimer();
            }

            _button.enabled = true;
            _timer.TimeTextState(false);
            ResourcesOperations.Add(StoreResourcesAmount.energy, 100);
        }
    }
}