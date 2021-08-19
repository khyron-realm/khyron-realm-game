using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public class GridGeneratePosition : MonoBehaviour
    {
        private static bool[,] _values;

        public static bool[,] GenerateValuesForPlacing(int rows, int columns, List<AreaToDestroy> list)
        {
            _values = new bool[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _values[i, j] = true;
                }
            }

            foreach (AreaToDestroy item in list)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if(item.columnNumber < columns/2)
                        {
                            if(item.rowNumber < rows/2)
                            {
                                LessThanHalf1(item, i, j);
                            }
                            else
                            {
                                LessThanHalf2(item, i, j);
                            }
                        }
                        else
                        {
                            if (item.rowNumber < rows / 2)
                            {
                                MoreThanHalf1(item, i, j);
                            }
                            else
                            {
                                MoreThanHalf2(item, i, j);
                            }
                        }
                    }
                }
            }
            return _values;
        }

        private static void LessThanHalf1(AreaToDestroy item, int i, int j)
        {
            if (item.columnNumber > j && item.rowNumber > i)
            {
                _values[i, j] = false;
            }
        }

        private static void LessThanHalf2(AreaToDestroy item, int i, int j)
        {
            if (item.columnNumber > j && item.rowNumber < i)
            {
                _values[i, j] = false;
            }
        }

        private static void MoreThanHalf1(AreaToDestroy item, int i, int j)
        {
            if (item.columnNumber < j && item.rowNumber > i )
            {
                _values[i, j] = false;
            }
        }

        private static void MoreThanHalf2(AreaToDestroy item, int i, int j)
        {
            if (item.columnNumber < j && item.rowNumber < i)
            {
                _values[i, j] = false;
            }
        }
    }

    [Serializable]
    public struct AreaToDestroy
    {
        public int rowNumber;
        public int columnNumber;
    }
}