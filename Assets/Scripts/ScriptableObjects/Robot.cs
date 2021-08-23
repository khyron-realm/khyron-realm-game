using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {miner, defender, probe}

[CreateAssetMenu(fileName = "Data", menuName = "Robot", order = 1)]
public class Robot: ScriptableObject
{
    public int movementSpeed;
    public int fieldOfVision;
    public int damagePerSecond;
    public int actionNumber;
    public int actionLength;
}