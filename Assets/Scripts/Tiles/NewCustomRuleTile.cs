using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class NewCustomRuleTile : RuleTile<NewCustomRuleTile.Neighbor> {
    public bool customField;
    public RuleTile checkTile;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Null = 3;
        public const int TileType = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.Null: return CheckIfThereIsTile(tile);
            case Neighbor.TileType: return CheckIfThereIsTileOfType(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }

    private bool CheckIfThereIsTile(TileBase tile)
    {
        if(tile == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CheckIfThereIsTileOfType(TileBase tile)
    {
        if (tile == checkTile)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}