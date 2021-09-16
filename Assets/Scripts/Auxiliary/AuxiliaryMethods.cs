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

        return Mathf.Clamp(NewValue, NewMin, NewMax);
    }
}
