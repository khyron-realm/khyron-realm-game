using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Player/LevelsThresholds", order = 1)]
public class LevelsThresholds : ScriptableObject
{
    public List<int> levelsThresholds;
}