using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Save;
using CountDown;
using AuxiliaryClasses;
using TMPro;
using Networking.Mines;


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
        [SerializeField] private GameObject _nameOfTheMine;
        [SerializeField] private GameObject _slotsFull;


        [Header("All Mines on the minimap and Auction island")]
        [SerializeField] private List<MineTouched> _mines;
        [SerializeField] private MineTouched _auction;
        #endregion


        #region "Private Members"
        private GameObject _currentGameObject;
        public static byte MineIndex;
        #endregion


        private void Awake()
        {
            _DetailsMine.SetActive(false);
            _DetailsAuction.SetActive(false);

            foreach (MineTouched item in _mines)
            {
                item.OnGameObjectTouched += TouchedGameObject;
                item.OnMineSelected += NameOfTheMine;
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
                _slotsFull.SetActive(false);

                if (!isMine)
                {
                    _noMinePanel.SetActive(true);
                    _nameOfTheMine.SetActive(false);

                    _buttonMine.gameObject.SetActive(false);
                    Animations.AnimateMineText(_noMinePanel);
                }
                else
                {
                    _noMinePanel.SetActive(false);
                    _nameOfTheMine.SetActive(true);

                    _buttonMine.gameObject.SetActive(true);
                    Animations.AnimateMineText(_nameOfTheMine);
                    Animations.AnimateMineButton(_buttonMine);
                }
            }
            else
            {
                _noMinePanel.SetActive(false);
                _nameOfTheMine.SetActive(false);
                
                _buttonMine.gameObject.SetActive(false);

                if(MineManager.MineList.Count < 6)
                {
                    _buttonAuction.gameObject.SetActive(true);
                    _slotsFull.SetActive(false);

                    Animations.AnimateMineButton(_buttonAuction);
                }
                else
                {
                    _buttonAuction.gameObject.SetActive(false);
                    _slotsFull.SetActive(true);

                    Animations.AnimateMineText(_slotsFull);
                }                             
            }           
        }


        private void NameOfTheMine(byte index)
        {
            _nameOfTheMine.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = MineManager.MineList[index].Name;
        }


        private void OnDestroy()
        {
            foreach (MineTouched item in _mines)
            {
                item.OnGameObjectTouched -= TouchedGameObject;
                item.OnMineSelected -= NameOfTheMine;
            }

            _auction.OnGameObjectTouched -= TouchedGameObject;
        }
    }
}