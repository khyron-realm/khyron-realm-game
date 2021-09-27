using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Save;
using Grid;

namespace Mine
{
    public class ShowMineDetails : MonoBehaviour
    {
        #region "Input"
        [Header("GameObject with all the attributes of a mine displayed on minimap")]
        [SerializeField] private GameObject _mineDetails;

        [Header("Button To Enter In the mine")]
        [SerializeField] private GameObject _mineButton;

        [Header("Button To Refresh The Mine")]
        [SerializeField]  private Button _refreshButton;

        [Header("All Mines on the minimap")]
        [SerializeField] private List<MineTouched> _mines;
        #endregion

        #region "Private Members"
        private RectTransform _enterButtonTransform;
        private RectTransform _refreshButtonTransform;

        private Image _enterButtonImage;
        private Image _refreshButtonImage;

        private GameObject _currentGameObject;
        #endregion

        private void Awake()
        {
            _enterButtonTransform = _mineButton.GetComponent<RectTransform>();
            _refreshButtonTransform = _refreshButton.GetComponent<RectTransform>();

            _enterButtonImage = _mineButton.GetComponent<Image>();
            _refreshButtonImage = _refreshButton.GetComponent<Image>();

            _mineDetails.SetActive(false);
            foreach (MineTouched item in _mines)
            {
                item.OnGameObjectTouched += TouchedGameObject;
            }
        }


        private void TouchedGameObject(GameObject temp)
        {
            if (_currentGameObject != temp)
            {
                _currentGameObject = temp;

                MineValues value = temp.GetComponent<MineValues>();

                _refreshButton.onClick.RemoveAllListeners();
                _refreshButton.onClick.AddListener(value.Refresh);

                _refreshButton.onClick.AddListener(
                    delegate
                    {
                        AdjustStaticMembers(value);
                    });

                AdjustStaticMembers(value);

                _enterButtonImage.color = new Color(1, 1, 1, 0);
                _refreshButtonImage.color = new Color(1, 1, 1, 0);

                _enterButtonTransform.localPosition = new Vector2(0, -2);
                _refreshButtonTransform.localPosition = new Vector2(-2.4f, 2.2f);

                _mineDetails.SetActive(true);
                _mineDetails.transform.position = temp.transform.position;

                _enterButtonTransform.DOLocalMoveY(-2.4f, 0.2f);
                _enterButtonImage.DOFade(1, 0.4f);

                _refreshButtonTransform.DOLocalMoveX(-2.8f, 0.2f);
                _refreshButtonImage.DOFade(1, 0.4f);
            }           
        }


        private static void AdjustStaticMembers(MineValues value)
        {
            GetMineGenerationData.HiddenSeed = value.HiddenSeed;
            GetMineGenerationData.ResourcesData = new List<ResourcesData>(value.ResourcesData);
        }
    }
}