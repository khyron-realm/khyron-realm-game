using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Manager.Train
{
    public class StoreTrainRobots : MonoBehaviour
    {
        [SerializeField] private int _robotsLimit;

        public static List<Robot> robotsInTraining;
        public static List<Robot> robotsTrained;
        public static int robotsLimit;

        private void Awake()
        {
            robotsInTraining = new List<Robot>();
            robotsTrained = new List<Robot>();
            robotsLimit = _robotsLimit;
        }
    }
}