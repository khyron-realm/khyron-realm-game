using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panels;

namespace Manager.Robots.Damage
{
    public class RobotsGetDamage : MonoBehaviour
    {
        #region "Private memebers"   
        private int _health;
        private GameObject _robotGameObject;
        #endregion

        //event
        public event Action<GameObject> OnDead;

        /// <summary>
        /// Method for doing damage to robots
        /// </summary>
        /// <param name="amount"> The amount of damage given </param>
        public bool DoDamage(int amount)
        {
            _health -= amount;

            if (_health < 0)
            {
                OnDead?.Invoke(_robotGameObject);
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Destory robot
        /// </summary>
        public void DestroyRobot()
        {
            _health = -1;
            OnDead?.Invoke(_robotGameObject);
        }


        /// <summary>
        /// Used to get health of the robot from its stats based on level
        /// </summary>
        /// <param name="currentRobot"></param>
        public void GetHealthFromRobot(Robot currentRobot)
        {
            int level = RobotsManager.robotsData[currentRobot.nameOfTheRobot].RobotLevel;
            _health =  currentRobot.robotLevel[level].status.health;
        }


        /// <summary>
        /// Get the gameObject that represet the robot [with sprite component etc]
        /// </summary>
        /// <param name="robot"> The gameObject that </param>
        public void GetRobotGameObject(GameObject robot)
        {
            _robotGameObject = robot;
        }
    }
}