using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.Robots;

namespace AuxiliaryClasses
{
    public class AuxiliaryMethods
    {
        /// <summary>
        /// 
        /// Map a value from and old range to a new one
        /// 
        /// </summary>
        /// <returns> New Value mapped in the range NewMin and NewMax </returns>
        public static float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
        {
            float OldRange = (OldMax - OldMin);
            float NewRange = (NewMax - NewMin);
            float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

            return Mathf.Clamp(NewValue, NewMin, NewMax);
        }


        // create in dictionary the robots
        public static Dictionary<string, RobotsPlayerProgress> CreateDictionary(List<Robot> robots)
        {
            Dictionary<string, RobotsPlayerProgress> tempDictionary = new Dictionary<string, RobotsPlayerProgress>();

            foreach (Robot item in robots)
            {
                RobotsPlayerProgress temp;

                temp.AvailableRobot = false;
                temp.RobotLevel = 0;

                try
                {
                    tempDictionary.Add(item.name, temp);
                }
                catch
                {
                    Debug.Log("Duplicate of the robot");
                }
            }

            return tempDictionary;
        }
    }
}