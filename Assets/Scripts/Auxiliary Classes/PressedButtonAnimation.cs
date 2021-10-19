using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;


namespace AuxiliaryClasses
{
    public class PressedButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            gameObject.transform.DOKill();
            gameObject.transform.DOScale(0.86f, 0.1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            gameObject.transform.DOKill();
            gameObject.transform.DOScale(1f, 0.1f);
        }
    }
}