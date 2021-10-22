using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    [CreateAssetMenu]
    // In Progress [need all the sprites of the blocks first]
    public class EmpyTileRule : RuleTile<TilesRule.Neighbor>
    {
        public bool customField;

        public TilesRule _ruleTile1;
        public TilesRule _ruleTile2;
        public TilesRule _ruleTile3;
        public TilesRule _ruleTile4;
        public TilesRule _ruleTile5;
        public TilesRule _ruleTile6;

        public class Neighbor : RuleTile.TilingRule.Neighbor
        {
            public const int NullTile = 3;
            public const int Tile = 4;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor)
            {
                case Neighbor.NullTile: return CheckIfThereIsNoTile(tile);
                case Neighbor.Tile: return !CheckIfThereIsNoTile(tile);
            }
            return base.RuleMatch(neighbor, tile);
        }


        private bool CheckIfThereIsNoTile(TileBase tile)
        {
            if (tile == _ruleTile1 || tile == _ruleTile2 || tile == _ruleTile3 || tile == _ruleTile4 || tile == _ruleTile5 || tile == _ruleTile6)
            {               
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}