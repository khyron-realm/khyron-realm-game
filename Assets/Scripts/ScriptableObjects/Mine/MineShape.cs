using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mine Shape", menuName = "Mine/Mine Shape")]
[System.Serializable]
public class MineShape : ScriptableObject
{
    public bool[] values;
}