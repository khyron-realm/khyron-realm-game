using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Robots/Robot", order = 1)]
public class RobotSO: ScriptableObject
{
    public byte _robotId;
    public string nameOfTheRobot;
    public string description;
    public Sprite icon;
    public List<RobotLevel> robotLevel;
}