using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Mine Resource", menuName = "Mine/Mine Values")]
public class MineValues : ScriptableObject
{
    public int HiddenSeed;
    public List<ResourcesValuesForMineGeneration> Seeds;
}