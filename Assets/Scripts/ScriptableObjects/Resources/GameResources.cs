using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Resources/Resource", order = 1)]
public class GameResources : ScriptableObject
{
    public byte Id;

    public string NameOfResource;
    public string Description;

    public Sprite Icon;
    public Sprite ConvertSprite;
    public Sprite Block;
}