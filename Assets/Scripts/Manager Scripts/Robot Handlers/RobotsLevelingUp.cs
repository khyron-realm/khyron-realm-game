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

            temp.availableRobot = RobotsManager.robotsData[name].availableRobot;
            temp.robotLevel = RobotsManager.robotsData[name].robotLevel + 1;

            RobotsManager.robotsData[name] = temp;
        }
    }
}