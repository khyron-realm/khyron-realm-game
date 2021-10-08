using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Prices/PriceToBuildOrUpgrade", order = 1)]
public class PriceToBuildOrUpgrade : ScriptableObject
{
    public int energy;
    public int lithium;
    public int silicon;
    public int titanium;

    private void OnValidate()
    {
        if (energy < 0)
        {
            energy = 0;
        }

        if (lithium < 0)
        {
            lithium = 0;
        }

        if (silicon < 0)
        {
            silicon = 0;
        }

        if (titanium < 0)
        {
            titanium = 0;
        }
    }
}