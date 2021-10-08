using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Robots/Robot", order = 1)]
public class Robot: ScriptableObject
{
    public string nameOfTheRobot;
    public string description;
    public int buildTime;
    public Sprite icon;
    public List<RobotLevel> robotLevel;
}