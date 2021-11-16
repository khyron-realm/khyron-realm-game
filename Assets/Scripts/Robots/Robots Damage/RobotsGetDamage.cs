using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networking.Headquarters;
using Levels;


namespace Manager.Robots.Damage
{
    public class RobotsGetDamage : MonoBehaviour
    {
        #region "Private memebers"   
        [SerializeField] private ChangeColorBasedOnHealth _colorHealth;

        private int _health;
        private GameObject _robotGameObject;
        
        private RobotSO _robot;
        private int _robotLevel;
        private int _maxHealth;
        #endregion

        //event
        public event Action<GameObject> OnDead;

        /// <summary>
        /// Method for doing damage to robots
        /// </summary>
        /// <param name="amount"> The amount of damage given </param>
        public bool DoDamage()
        {
            _health -= (int)LevelMethods.RobotMiningDamage((byte)_robotLevel, _robot.RobotId);
            _colorHealth.AdjustColorBasedOnHealth(_health, _maxHealth);

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
        public void GetHealthFromRobot(RobotSO currentRobot)
        {
            _robot = currentRobot;
            _robotLevel = HeadquartersManager.Player.Robots[currentRobot.RobotId].Level;
            _health =  LevelMethods.RobotHealth((byte)_robotLevel, currentRobot.RobotId);
            _maxHealth = _health;
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