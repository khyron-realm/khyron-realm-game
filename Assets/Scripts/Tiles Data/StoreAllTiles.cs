using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Tiledata
{
	public class StoreAllTiles : MonoBehaviour
	{
		public static StoreAllTiles Instance;
		public Tilemap Tilemap;

		// Nested List with data about the Tiles
		public List<List<DataOfTile>> Tiles;

		// List with all blocks positions
		public List<Vector2Int> TilesPositions;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else if (Instance != this)
			{
				Destroy(gameObject);
			}

			Tiles = new List<List<DataOfTile>>();
			TilesPositions = new List<Vector2Int>();
		}
	}
}
