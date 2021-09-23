using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager.Store;
using Manager.Train;


namespace GameErrors
{
    public class RaiseGameError : MonoBehaviour
    {
        [SerializeField] private Text _text;

        [Space(20f)]

        [SerializeField] private string _notEnoughResources;
        [SerializeField] private string _notEnoughEnergy;
        [SerializeField] private string _toManyResources;
        [SerializeField] private string _maxCapacity;

        private float _time;
        private bool _once = false;

        private void Awake()
        {
            _text.enabled = false;

            ManageResourcesOperations.OnNotEnoughResources += NotEnoughResources;
            ManageResourcesOperations.OnNotEnoughEnergy += NotEnoughEnergy;
            ManageResourcesOperations.OnToMuchResources += ToManyResources;
            StoreTrainRobotsOperations.OnMaximumCapacityAchieved += MaxCapacityAchieved;
        }

        private void NotEnoughResources()
        {
            _text.text = _notEnoughResources;
            StartShowingPopUp();
        }
        private void NotEnoughEnergy()
        {
            _text.text = _notEnoughEnergy;
            StartShowingPopUp();
        }
        private void ToManyResources()
        {
            _text.text = _toManyResources;
            StartShowingPopUp();
        }
        private void MaxCapacityAchieved()
        {
            _text.text = _maxCapacity;
            StartShowingPopUp();
        }


        private void StartShowingPopUp()
        {
            if (_once == false)
            {
                _once = true;
                StartCoroutine(ShowPopUp());
                StartCoroutine(FontChange());
            }
            else
            {
                _time = 0;
                StartCoroutine(FontChange());
            }
        }
        private IEnumerator ShowPopUp()
        {
            _text.enabled = true;

            _time = 0;
            while (_time < 3)
            {
                _time += Time.deltaTime;
                _text.color = Color.Lerp(Color.red, new Color(1, 1, 1, 0), _time/3);     
                yield return null;
            }

            _once = false;
            _text.enabled = false;
        }
        private IEnumerator FontChange()
        {
            float temp = 0;
            while (temp < 1)
            {
                temp += Time.deltaTime * 12;
                _text.fontSize = (int)Mathf.Lerp(64, 72, temp);

                yield return null;
            }

            temp = 0;
            while (temp < 1)
            {
                temp += Time.deltaTime * 12;
                _text.fontSize = (int)Mathf.Lerp(72, 64, temp);

                yield return null;
            }
        }
    }
}