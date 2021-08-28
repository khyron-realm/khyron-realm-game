using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TilesData
{
	public class StoreAllTiles : MonoBehaviour
	{
		public static StoreAllTiles instance;
		public Tilemap Tilemap;

		public List<List<StoreDataAboutTiles>> tiles;

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

			tiles = new List<List<StoreDataAboutTiles>>();
		}
	}
}
