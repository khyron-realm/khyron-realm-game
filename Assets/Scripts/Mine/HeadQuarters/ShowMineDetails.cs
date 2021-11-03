using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Save;
using CountDown;
using AuxiliaryClasses;


namespace Mine
{
    public class ShowMineDetails : MonoBehaviour
    {
        #region "Input"
        [Header("GameObjects with button and info that are activated when user touches")]
        [SerializeField] private GameObject _Details;

        [Header("Buttons")]
        [SerializeField] private Button _button; //button enter place
        [SerializeField] private GameObject _noMinePanel; // panel bg text

        [Header("All Mines on the minimap and Auction island")]
        [SerializeField] private List<MineTouched> _mines;
        [SerializeField] private MineTouched _auction;
        #endregion


        #region "Private Members"
        private GameObject _currentGameObject;
        private Text _textButton;
        #endregion


        private void Awake()
        {
            _Details.SetActive(false);

            _textButton = _button.transform.GetChild(0).GetComponent<Text>();

            foreach (MineTouched item in _mines)
            {
                item.OnGameObjectTouched += TouchedGameObject;
            }

            _auction.OnGameObjectTouched += TouchedGameObject;
        }


        private void TouchedGameObject(GameObject temp, bool isMine, bool isAuction)
        {
            if (_currentGameObject != temp)
            {
                _currentGameObject = temp;

                _Details.SetActive(true);
                _Details.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, 0);

                IsMineAvailable(isMine);
                IsAuctionHub(isAuction);
            }
        }


        private void IsAuctionHub(bool isAuction)
        {
            if (isAuction)
            {
                _textButton.text = "Find Auction";
            }
            else
            {
                _textButton.text = "Enter Mine";
            }
        }
        private void IsMineAvailable(bool isMine)
        {
            if (isMine)
            {
                _noMinePanel.SetActive(true);
                _button.gameObject.SetActive(false);
                Animations.AnimateMineText(_noMinePanel);
            }
            else
            {
                _noMinePanel.SetActive(false);
                _button.gameObject.SetActive(true);
                Animations.AnimateMineButton(_button);
            }
        }
    }
}