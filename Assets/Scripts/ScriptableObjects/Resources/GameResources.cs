using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Resources/Resource", order = 1)]
public class GameResources : ScriptableObject
{
    public string nameOfResource;
    public string description;

    public Sprite icon;
    public Sprite convertSprite;
    public Sprite block;

    [Range(0,1)]
    public float conversionRate;

    public int maxValue;
    public int currentValue;
}