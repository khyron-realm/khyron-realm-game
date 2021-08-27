using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliaryMethods
{
    public static float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {
        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public static Direction ConvertDirection(Direction temp)
    {
        if (temp == Direction.left)
        {
            return Direction.right;
        }
        else if (temp == Direction.right)
        {
            return Direction.left;
        }
        else if (temp == Direction.up)
        {
            return Direction.down;
        }
        else if (temp == Direction.down)
        {
            return Direction.up;
        }
        else
        {
            return Direction.none;
        }
    }
}
