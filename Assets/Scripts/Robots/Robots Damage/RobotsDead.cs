using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Robots.Damage
{
    public class RobotsDead : MonoBehaviour
    {
        [SerializeField] private RobotsGetDamage _damage;

        private void Awake()
        {
            _damage.OnDead += WhenRobotDies;
        }

        private void WhenRobotDies(GameObject temp)
        {
            temp.SetActive(false);
        }
    }
}