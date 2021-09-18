using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Managers.Robots
{
    public class RobotsUnlocking:MonoBehaviour
    {
        public void UnlockRobot(string name)
        {
            RobotsPlayerProgress temp;
            temp.availableRobot = true;
            temp.robotLevel = RobotsManager.robotsData[name].robotLevel;

            RobotsManager.robotsData[name] = temp;
        }
    }
}