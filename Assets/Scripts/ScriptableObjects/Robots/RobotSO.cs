using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Robots/Robot", order = 1)]
public class RobotSO: ScriptableObject
{
    public byte RobotId;
    public int HousingSpace;
    public int BuildTime;
    public string NameOfTheRobot;
    public string Description;
    public Sprite Icon;
    public List<RobotLevel> RobotLevel;
}