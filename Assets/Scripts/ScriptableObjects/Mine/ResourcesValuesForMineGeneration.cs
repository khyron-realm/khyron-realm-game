using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mine Resource", menuName = "Mine/Mine Data For A Resource")]
public class ResourcesValuesForMineGeneration : ScriptableObject
{
    public int Seed;
    public float RarityCoeficient;
    public float Frequency;
}