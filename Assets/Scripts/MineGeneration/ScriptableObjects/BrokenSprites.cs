using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Broken Sprites", menuName = "Broken Sprites")]
public class BrokenSprites : ScriptableObject
{
    public Mesh cornerUpLeft;
    public Mesh cornerUpRight;
    public Mesh cornerDownLeft;
    public Mesh cornerDownRight;

    public Mesh halfUp;
    public Mesh halfDown;
    public Mesh halfLeft;
    public Mesh halfRight;

    public Mesh whole;
}
