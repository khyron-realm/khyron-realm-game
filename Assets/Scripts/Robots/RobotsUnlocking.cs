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

            temp.AvailableRobot = true;
            temp.RobotLevel = RobotsManager.robotsData[name].RobotLevel;

            RobotsManager.robotsData[name] = temp;
        }
    }
}