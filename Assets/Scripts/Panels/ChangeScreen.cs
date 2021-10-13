using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Panels
{
    public class ChangeScreen : MonoBehaviour, IPointerClickHandler
    {
        #region "Input data"
        [SerializeField] private GameObject _currentScreen;
        [SerializeField] private GameObject _screenToGo;

        [SerializeField] private bool _activateOnPointerClick;
        #endregion

        public void ScreenChange()
        {
            _currentScreen.SetActive(false);
            _screenToGo.SetActive(true);
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if(_activateOnPointerClick)
            {
                ScreenChange();
            }               
        }
    }
}