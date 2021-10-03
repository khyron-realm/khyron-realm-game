using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

namespace Manager.Robots
{
    public class RobotsManager : MonoBehaviour
    {
        // Scriptable object with data about the robots
        [Header("All robots that exist in the game")]
        [SerializeField]  private List<Robot> _robots;

        public static List<Robot> robots;
        public static Dictionary<string, RobotsPlayerProgress> robotsData;

        private void Awake()
        {
            robots = new List<Robot>(_robots);
            robotsData = new Dictionary<string, RobotsPlayerProgress>(AuxiliaryClasses.AuxiliaryMethods.CreateDictionary(_robots));
        }
    }
}