using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Manager.Robots
{
    public static class RobotsLevelingUp
    {
        public static void UpgradeRobot(string name)
        {
            RobotsPlayerProgress temp;

            temp.AvailableRobot = RobotsManager.robotsData[name].AvailableRobot;
            temp.RobotLevel = RobotsManager.robotsData[name].RobotLevel + 1;

            RobotsManager.robotsData[name] = temp;
        }
    }
}