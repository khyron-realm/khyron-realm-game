using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Grid
{

    // Used to create custom shape of the mine 
    public class ShapeTheMine : MonoBehaviour
    {
        [SerializeField] private Tilemap _map;

        [SerializeField] private int _rows;
        [SerializeField] private int _columns;

        [SerializeField] private MineShape _shape;

        private static bool[,] s_values;
        
        // Update is called once per frame

        private void Awake()
        {
            s_values = new bool[_rows, _columns];

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    s_values[i, j] = true;
                }
            }
        }


        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            Vector3Int position = _map.WorldToCell(worldPoint);

            if(position.x >= 0 && position.x < _rows)
            {
                if (position.y >= 0 && position.y < _columns)
                {
                    s_values[position.x, position.y] = false;
                    _map.SetColor(position, Color.green);
                }
            }          
        }


        void OnDisable()
        {
            Debug.Log("Saved Shape");

            //_shape.values = new bool[_rows * _columns];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    _shape.values[(i * _columns)  + j] = s_values[i, j];
                }
            }
        }
    }
}