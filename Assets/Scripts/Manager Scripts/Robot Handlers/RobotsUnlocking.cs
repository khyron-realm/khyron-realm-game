using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Manager.Robots
{
    public static class RobotsUnlocking
    {
        public static void UnlockRobot(string name)
        {
            RobotsPlayerProgress temp;

            temp.availableRobot = true;
            temp.robotLevel = RobotsManager.robotsData[name].robotLevel;

            RobotsManager.robotsData[name] = temp;
        }
    }
}