using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mine Resource", menuName = "Mine Resource")]
public class MineResources : ScriptableObject
{
    public RuleTile resourceTile;
    public int dropValueMin;
    public int dropValueMax;
    public float rarityCoeficient;
    public float frequency;
}
