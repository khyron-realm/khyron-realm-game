using System;
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
        public static Dictionary<string, RobotsPlayerProgress> CreateDictionary(List<RobotSO> robots)
        {
            Dictionary<string, RobotsPlayerProgress> tempDictionary = new Dictionary<string, RobotsPlayerProgress>();

            foreach (RobotSO item in robots)
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


        /// <summary>
        /// Returns the difference in seconds between curent time and a var "time" time
        /// </summary>
        /// <param name="time">the time in binary format</param>
        /// <returns>time in seconds</returns>
        public static int TimeTillFinishEnd(long time)
        {
            DateTime startTime = DateTime.FromBinary(time);
            DateTime now = DateTime.UtcNow;

            int timeRemained = (int)now.Subtract(startTime).TotalSeconds;
            return timeRemained;
        }


        /// <summary>
        /// Returns the difference in seconds between curent time and a var "time" time
        /// </summary>
        /// <param name="time">the time in binary format</param>
        /// <returns>time in seconds</returns>
        public static int TimeTillFinishStart(long time)
        {           
            DateTime endTime = DateTime.FromBinary(time);
            DateTime now = DateTime.UtcNow;

            int timeRemained = (int)endTime.Subtract(now).TotalSeconds;
            return timeRemained;
        }
    }
}