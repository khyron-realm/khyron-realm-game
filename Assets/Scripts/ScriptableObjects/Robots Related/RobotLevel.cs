using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Robots/RobotLevel", order = 1)]
public class RobotLevel : ScriptableObject
{
    public StatusRobot status;
    public Sprite statusImage;
    public PriceToBuildOrUpgrade priceToBuild;
    public PriceToBuildOrUpgrade priceToUpgrade;
    public Sprite upgradeImage;
}