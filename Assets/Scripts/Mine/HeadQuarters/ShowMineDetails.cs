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
        [SerializeField] private GameObject _DetailsMine;
        [SerializeField] private GameObject _DetailsAuction;

        [Header("Buttons")]
        [SerializeField] private Button _buttonMine; //button enter place
        [SerializeField] private Button _buttonAuction; //
        [SerializeField] private GameObject _noMinePanel; // panel bg text


        [Header("All Mines on the minimap and Auction island")]
        [SerializeField] private List<MineTouched> _mines;
        [SerializeField] private MineTouched _auction;
        #endregion


        #region "Private Members"
        private GameObject _currentGameObject;
        #endregion


        private void Awake()
        {
            _DetailsMine.SetActive(false);
            _DetailsAuction.SetActive(false);

            foreach (MineTouched item in _mines)
            {
                item.OnGameObjectTouched += TouchedGameObject;
            }

            _auction.OnGameObjectTouched += TouchedGameObject;
        }


        private void TouchedGameObject(GameObject temp, bool isMine, bool isAuction)
        {
            if (_currentGameObject == temp) return;
            
            _currentGameObject = temp;

            if (!isAuction)
            {
                _DetailsAuction.SetActive(false);
                _DetailsMine.SetActive(true);
                _DetailsMine.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, 0);
            }
            else
            {
                _DetailsMine.SetActive(false);
                _DetailsAuction.SetActive(true);
                _DetailsAuction.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, 0);
            }
            
            IsMineAvailable(isMine, isAuction);           
        }

        private void IsMineAvailable(bool isMine, bool isAuction)
        {
            _buttonAuction.gameObject.SetActive(false);

            if (!isAuction)
            {
                _buttonAuction.gameObject.SetActive(false);

                if (isMine)
                {
                    _noMinePanel.SetActive(true);
                    _buttonMine.gameObject.SetActive(false);
                    Animations.AnimateMineText(_noMinePanel);
                }
                else
                {
                    _noMinePanel.SetActive(false);
                    _buttonMine.gameObject.SetActive(true);
                    Animations.AnimateMineButton(_buttonMine);
                }
            }
            else
            {
                _noMinePanel.SetActive(false);
                _buttonMine.gameObject.SetActive(false);

                _buttonAuction.gameObject.SetActive(true);
                Animations.AnimateMineButton(_buttonAuction);
            }           
        }
    }
}