using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mine Resource", menuName = "Mine/Mine Resource")]
public class MineResources : ScriptableObject
{
    public RuleTile Soft1;
    public RuleTile Soft2;
    public RuleTile Soft3;
    public RuleTile Hard1;
    public RuleTile Hard2;
    public RuleTile Hard3;
    public int DropValueMin;
    public int DropValueMax;
}