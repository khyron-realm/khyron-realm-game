using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mine Resource", menuName = "Mine/Mine Resource")]
public class MineResources : ScriptableObject
{
    public RuleTile ResourceTile;
    public int DropValueMin;
    public int DropValueMax;
}