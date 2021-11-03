using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Save;
using Mine;
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

        [Header("All Mines on the minimap")]
        [SerializeField] private List<MineTouched> _mines;

        [Space(20f)]

        [Header("Aquired gameObject with the button")]
        [SerializeField] private GameObject _aquiredDetails;
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


        private void TouchedGameObject(GameObject temp, GameObject manager)
        {
            _value = manager.GetComponent<MineValues>();
            _time = manager.GetComponent<TimeValues>();

            _mineButton.onClick.AddListener(
                delegate
                {
                    AdjustStaticMembers(_value, _time);
                });

            AdjustStaticMembers(_value, _time);

            if (_currentGameObject != temp)
            {
                _currentGameObject = temp;
            
                TimeActualisation(manager);

                _mineDetails.SetActive(true);
                _aquiredDetails.SetActive(false);

                _mineDetails.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, 0);

                // Animate mine button 
                AnimateMineButton(_mineButton);               
            }
        }


        private void TimeActualisation(GameObject temp)
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


        private void AnimateMineButton(Button temp)
        {
            temp.image.color = new Color(1, 1, 1, 0);
            temp.transform.localPosition = new Vector2(0, -2);

            temp.transform.DOLocalMoveY(-2.4f, 0.2f);
            temp.image.DOFade(1, 0.4f);
        }


        private void AdjustStaticMembers(MineValues value, TimeValues time)
        {
            GetMineGenerationData.HiddenSeed = value.HiddenSeed;
            GetMineGenerationData.ResourcesData = new List<ResourcesData>(value.ResourcesData);

            GetTimeTillAuctionEnds.TimeOfTheMine = time.TimeTillFinished;
        }
    }
}