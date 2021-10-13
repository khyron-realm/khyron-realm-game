using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Manager.Robots.Damage
{
    public class RobotsDead : MonoBehaviour
    {
        #region "Input data"
        [SerializeField] private RobotsGetDamage _damage;
        [SerializeField] private Animator _animator;
        #endregion

        private void Awake()
        {
            _damage.OnDead += WhenRobotDies;
        }

        private void WhenRobotDies(GameObject temp)
        {
            StartCoroutine("CheckForEndOfDead", temp);
        }

        private IEnumerator CheckForEndOfDead(GameObject temp)
        {
            _animator.SetBool("isMining", false);
            _animator.SetBool("isDead", true);

            yield return new WaitForSeconds(1.4f);

            temp.GetComponent<SpriteRenderer>().DOFade(0, 1.4f).OnComplete(() => temp.SetActive(false));
        }
    }
}