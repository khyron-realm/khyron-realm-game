using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Manager.Train
{
    public class StoreRobots : MonoBehaviour
    {
        [SerializeField] private int _robotsLimit;

        public static List<Robot> RobotsInTraining;
        public static List<Robot> RobotsTrained;
        public static int RobotsLimit;

        private void Awake()
        {
            RobotsInTraining = new List<Robot>();
            RobotsTrained = new List<Robot>();

            RobotsLimit = _robotsLimit;
        }
    }
}