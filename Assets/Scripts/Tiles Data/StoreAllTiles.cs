using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Tiledata
{
	public class StoreAllTiles : MonoBehaviour
	{
		public static StoreAllTiles instance;
		public Tilemap Tilemap;

		// Nested List with data about the Tiles
		public List<List<DataOfTile>> tiles;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else if (instance != this)
			{
				Destroy(gameObject);
			}

			tiles = new List<List<DataOfTile>>();
		}
	}
}
