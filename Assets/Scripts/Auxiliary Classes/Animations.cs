using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace AuxiliaryClasses
{
    public class Animations
    {
        // Error messages with red text animation in the middle of the screen
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


        // Animate button for Mine & Auction entering
        public static void AnimateMineButton(Button temp)
        {
            temp.image.color = new Color(1, 1, 1, 0);
            temp.transform.localPosition = new Vector2(0, -2);

            temp.transform.DOLocalMoveY(-2.4f, 0.2f);
            temp.image.DOFade(1, 0.4f);
        }


        // Animate text[no mine available] 
        public static void AnimateMineText(GameObject temp)
        {
            temp.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            temp.transform.localPosition = new Vector2(0, 2f);

            temp.transform.DOLocalMoveY(3f, 0.2f);
            temp.GetComponent<Image>().DOFade(0.7f, 0.4f);
        }
    }
}