using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Save;
using Grid;
using CountDown;

namespace Mine
{
    public class ShowMineDetails : MonoBehaviour
    {
        #region "Input"
        [Header("GameObject with all the attributes of a mine displayed on minimap")]
        [SerializeField] private GameObject _mineDetails;

        [Header("Button To Enter In the mine")]
        [SerializeField] private Button _mineButton;

        [Header("Button To Refresh The Mine")]
        [SerializeField]  private Button _refreshButton;

        [Header("All Mines on the minimap")]
        [SerializeField] private List<MineTouched> _mines;

        [Space(20f)]

        [Header("Aquired gameObject with the button")]
        [SerializeField] private GameObject _aquiredDetails;

        [Header("All Mines on the minimap")]
        [SerializeField] private Button _aquiredButton;
        #endregion

        #region "Private Members"
        private GameObject _currentGameObject;
        private Timer _tempTimer;

        private MineValues _value;
        private TimeValues _time;
        #endregion


        private void Awake()
        {
            _mineDetails.SetActive(false);

            foreach (MineTouched item in _mines)
            {
                item.OnGameObjectTouched += TouchedGameObject;
            }
        }

        private void TouchedGameObject(GameObject temp, bool aquired)
        {
            _value = temp.GetComponent<MineValues>();
            _time = temp.GetComponent<TimeValues>();

            AdjustStaticMembers(_value, _time);

            if (_currentGameObject != temp)
            {
                _currentGameObject = temp;
            
                if(aquired)
                {
                    _mineDetails.SetActive(false);
                    _aquiredDetails.SetActive(true);

                    _aquiredDetails.transform.position = temp.transform.position;
                    
                    AnimateMineButton();
                }
                else
                {
                    ActualizeTimer(temp);

                    _mineDetails.SetActive(true);
                    _aquiredDetails.SetActive(false);

                    _mineDetails.transform.position = temp.transform.position;


                    // Add listeners to refresh button
                    AddListenersToRefreshButton();


                    // Animate mine button and refresh button
                    AnimateMineButton();
                    AnimateRefreshButton();
                }
            }
        }


        private void AddListenersToRefreshButton()
        {
            _refreshButton.onClick.RemoveAllListeners();
            _refreshButton.onClick.AddListener(_value.Refresh);
            _refreshButton.onClick.AddListener(
                delegate
                {
                    AdjustStaticMembers(_value, _time);
                });
        }
        private void ActualizeTimer(GameObject temp)
        {
            if (_tempTimer != null)
            {
                _tempTimer.HasTimeText = false;
            }

            _tempTimer = temp.GetComponent<Timer>();

            if (_tempTimer != null)
            {
                _tempTimer.HasTimeText = true;
            }
        }


        private void AnimateMineButton()
        {
            _mineButton.image.color = new Color(1, 1, 1, 0);
            _mineButton.transform.localPosition = new Vector2(0, -2);

            _mineButton.transform.DOLocalMoveY(-2.4f, 0.2f);
            _mineButton.image.DOFade(1, 0.4f);
        }
        private void AnimateRefreshButton()
        {
            _refreshButton.image.color = new Color(1, 1, 1, 0);
            _refreshButton.transform.localPosition = new Vector2(-2.4f, 2.2f);
            _refreshButton.transform.DOLocalMoveX(-2.8f, 0.2f);
            _refreshButton.image.DOFade(1, 0.4f);
        }


        private void AdjustStaticMembers(MineValues value, TimeValues time)
        {
            GetMineGenerationData.HiddenSeed = value.HiddenSeed;
            GetMineGenerationData.ResourcesData = new List<ResourcesData>(value.ResourcesData);

            GetTimeTillAuctionEnds.TimeOfTheMine = time.TimeTillFinished;
        }
    }
}