using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Mine
{
    public class ShowMineDetailes : MonoBehaviour
    {
        [Header("GameObject with all the attributes of a mine displayed on minimap")]
        [SerializeField] private GameObject _mineDetailes;

        [Header("Button To Enter In the mine")]
        [SerializeField] private GameObject _mineButton;

        [Header("All Mines on the minimap")]
        [SerializeField] private List<MineTouched> _mines;

        private RectTransform _enterButtonTransform;
        private Image _enterButtonImage;

        private void Awake()
        {
            _enterButtonTransform = _mineButton.GetComponent<RectTransform>();
            _enterButtonImage = _mineButton.GetComponent<Image>();

            _mineDetailes.SetActive(false);
            foreach (MineTouched item in _mines)
            {
                item.OnGameObjectTouched += TouchedGameObject;
            }
        }


        private void TouchedGameObject(GameObject temp)
        {
            _enterButtonImage.color = new Color(1,1,1,0);
            _enterButtonTransform.localPosition = new Vector2(0, -2);
            _mineDetailes.SetActive(true);
            _mineDetailes.transform.position = temp.transform.position;
            
            _enterButtonTransform.DOLocalMoveY(-2.4f, 0.2f);
            _enterButtonImage.DOFade(1, 0.4f);
        }
    }
}