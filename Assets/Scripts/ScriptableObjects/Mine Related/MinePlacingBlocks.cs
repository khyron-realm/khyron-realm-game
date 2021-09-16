using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MineTemplate", menuName = "MineTemplate", order = 1)]
public class MinePlacingBlocks : ScriptableObject
{
    public int[,] values;
}
