using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Robots/StatusRobot", order = 1)]
public class StatusRobot : ScriptableObject
{
    public int health;
    public int movementSpeed;
    public int miningSpeed;

    private void OnValidate()
    {
        if (health < 0)
        {
            health = 0;
        }

        if (movementSpeed < 0)
        {
            movementSpeed = 0;
        }

        if (miningSpeed < 0)
        {
            miningSpeed = 0;
        }
    }
}
