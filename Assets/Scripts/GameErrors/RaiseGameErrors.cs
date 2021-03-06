using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayerDataUpdate;
using TMPro;


namespace GameErrors
{
    public class RaiseGameErrors : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private int _fontMin;
        [SerializeField] private int _fontMax;

        [Space(20f)]

        [SerializeField] private string _notEnoughResources;
        [SerializeField] private string _notEnoughEnergy;
        [SerializeField] private string _tooManyResources;
        [SerializeField] private string _maxCapacity;
        [SerializeField] private string _lastBid;
        [SerializeField] private string _bidOverYou;
        [SerializeField] private string _maximumBidsAchieved;
        #endregion


        private float _time;
        private bool _once = false;


        private void Awake()
        {
            _text.enabled = false;

            PlayerDataOperations.OnNotEnoughEnergy += NotEnoughEnergy;
            PlayerDataOperations.OnNotEnoughResources += NotEnoughResources;
            PlayerDataOperations.OnToManyResources += TooManyResources;
            PlayerDataOperations.OnNotEnoughSpaceForRobots += MaxCapacityAchieved;
            BidManager.OnLastBidWasYours += LastBidWasYours;
            BidManager.OnSomeOneBiddedOverYou += SomeOneBiddedOverYou;

            BidManager.OnNotEnoughEnergy += NotEnoughEnergy;
            BidManager.OnToMuchEnergy += TooManyResources;

            BidManager.OnMaximBidsAchieved += MaximumBidsAcheived;
        }


        #region "Handlers for errors"
        private void NotEnoughResources(byte tag)
        {
            _text.text = _notEnoughResources;
            StartShowingPopUp();
        }
        private void NotEnoughEnergy(byte tag)
        {
            _text.text = _notEnoughEnergy;
            StartShowingPopUp();
        }
        private void TooManyResources()
        {
            _text.text = _tooManyResources;
            StartShowingPopUp();
        }
        private void MaxCapacityAchieved(byte tag)
        {
            _text.text = _maxCapacity;
            StartShowingPopUp();
        }
        private void LastBidWasYours()
        {
            _text.text = _lastBid;
            StartShowingPopUp();
        }
        private void SomeOneBiddedOverYou()
        {
            _text.text = _bidOverYou;
            StartShowingPopUp();
        }
        private void MaximumBidsAcheived()
        {
            _text.text = _maximumBidsAchieved;
            StartShowingPopUp();
        }
        #endregion


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
                _text.color = Color.Lerp(Color.red, new Color(1, 1, 1, 0), _time / 3);
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
                _text.fontSize = (int)Mathf.Lerp(_fontMin, _fontMax, temp);

                yield return null;
            }

            temp = 0;
            while (temp < 1)
            {
                temp += Time.deltaTime * 12;
                _text.fontSize = (int)Mathf.Lerp(_fontMax, _fontMin, temp);

                yield return null;
            }
        }


        private void OnDestroy()
        {
            PlayerDataOperations.OnNotEnoughEnergy -= NotEnoughEnergy;
            PlayerDataOperations.OnNotEnoughResources -= NotEnoughResources;
            PlayerDataOperations.OnToManyResources -= TooManyResources;
            PlayerDataOperations.OnNotEnoughSpaceForRobots -= MaxCapacityAchieved;
            BidManager.OnLastBidWasYours -= LastBidWasYours;
            BidManager.OnSomeOneBiddedOverYou -= SomeOneBiddedOverYou;

            BidManager.OnNotEnoughEnergy -= NotEnoughEnergy;
            BidManager.OnToMuchEnergy -= TooManyResources;

            BidManager.OnMaximBidsAchieved -= MaximumBidsAcheived;
        }
    }
}