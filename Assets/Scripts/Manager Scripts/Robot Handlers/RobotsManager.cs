using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AuxiliaryMethods;


namespace Managers.Robots
{
    [RequireComponent(typeof(RobotsLevelingUp))]
    [RequireComponent(typeof(RobotsUnlocking))]
    public class RobotsManager : MonoBehaviour
    {
        // Scriptable object with data about the robots
        [SerializeField]
        [Header("All robots that exist in the game")]
        private List<Robot> _robots;

        public static List<Robot> robots;
        public static Dictionary<string, RobotsPlayerProgress> robotsData;

        private void Awake()
        {
            robots = new List<Robot>(_robots);
            robotsData = new Dictionary<string, RobotsPlayerProgress>(AuxiliaryMethods.AuxiliaryMethods.CreateDictionary(_robots));
        }
    }
}