using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct RobotsPlayerProgress
{
    public bool availableRobot;
    public int robotLevel;
}


public class RobotsManager : MonoBehaviour
{
    // Scriptable object with data about the robots
    [SerializeField]
    [Header("All robots that exist in the game")]
    private List<Robot> _robots;


    // List of robots
    public static List<Robot> robots;


    // List of robots with their stats
    public static Dictionary<string, RobotsPlayerProgress> robotsData;


    private void Awake()
    {
        robots = new List<Robot>();
        robotsData = new Dictionary<string, RobotsPlayerProgress>();

        foreach (Robot item in _robots)
        {
            robots.Add(item);
        }

        CreateDictionary();
    }

    private void CreateDictionary()
    {
        foreach (Robot item in _robots)
        {
            RobotsPlayerProgress temp;
            temp.availableRobot = false;
            temp.robotLevel = 0;

            try
            {
                robotsData.Add(item.name, temp);
            }
            catch
            {
                print("Duplicate of the robot");
            }
            
        }
    }

    public void UnlockRobot(string name)
    {
        RobotsPlayerProgress temp;
        temp.availableRobot = true;
        temp.robotLevel = robotsData[name].robotLevel;

        robotsData[name] = temp;
    }

    public void UpgradeRobot(string name)
    {
        RobotsPlayerProgress temp;
        temp.availableRobot = robotsData[name].availableRobot;
        temp.robotLevel = robotsData[name].robotLevel + 1;

        robotsData[name] = temp;
    }
}