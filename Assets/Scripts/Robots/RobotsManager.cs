using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

namespace Manager.Robots
{
    public class RobotsManager : MonoBehaviour
    {
        // Scriptable object with data about the robots
        [Header("All robots that exist in the game")]
        [SerializeField]  private List<RobotSO> _robots;

        public static List<RobotSO> robots;

        private void Awake()
        {
            robots = new List<RobotSO>(_robots);
        }
    }
}