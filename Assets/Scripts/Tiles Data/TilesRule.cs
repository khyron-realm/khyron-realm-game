using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Tiles
{
    [CreateAssetMenu]
    // In Progress [need all the sprites of the blocks first]
    public class TilesRule : RuleTile<TilesRule.Neighbor>
    {
        public bool customField;
        public RuleTile emptyTile;

        public class Neighbor : RuleTile.TilingRule.Neighbor
        {
            public const int Null = 3;
            public const int Tile = 4;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor)
            {
                case Neighbor.Null: return CheckIfThereIsNoTile(tile);
                case Neighbor.Tile: return !CheckIfThereIsNoTile(tile);
            }
            return base.RuleMatch(neighbor, tile);
        }

        private bool CheckIfThereIsNoTile(TileBase tile)
        {
            if (tile == emptyTile || tile == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}