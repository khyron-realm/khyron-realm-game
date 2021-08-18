using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {miner, defender, probe}

[CreateAssetMenu(fileName = "Data", menuName = "Robot", order = 1)]
public class Robot: ScriptableObject
{
    public Type type;
    public int movementSpeed;
    public int fieldOfVision;
    public int hitPoints;
    public int damagePerSecond;
    public int actionNumber;
    public int actionLength;
}