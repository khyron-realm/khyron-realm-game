using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager.Robots
{
    // Static class that PERSIST during the aplication runtime 
    // The static values remain the same even if you change the scenes
    // Class must not inherit from MonoBehaviour
    // Used for comunication between scenes
    public static class GetRobotsTrained
    {
        public static List<RobotSO> RobotsBuilt;
    }
}