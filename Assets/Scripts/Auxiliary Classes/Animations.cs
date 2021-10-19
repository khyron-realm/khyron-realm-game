using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace AuxiliaryClasses
{
    public class Animations : MonoBehaviour
    {
        public static void MesageErrorAnimation(Text errorsText, string text)
        {
            errorsText.enabled = true;
            errorsText.transform.localScale = Vector3.one;
            errorsText.color = Color.red;

            errorsText.text = text;

            errorsText.DOKill();
            errorsText.transform.DOScale(1.14f, 0.1f).OnComplete(() => errorsText.transform.DOScale(1f, 0.1f));
            errorsText.DOFade(0, 2f).SetDelay(1).OnComplete(() => errorsText.enabled = false);
        }
    }
}