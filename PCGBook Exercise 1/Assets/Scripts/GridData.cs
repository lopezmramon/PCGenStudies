using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{

    public int[,] map;
    public bool[,] hasFloor;

    public void GenerateGrid(int x, int y)
    {
        this.map = new int[x, y];
        this.hasFloor = new bool[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                map[i, j] = 0;
                hasFloor[i, j] = true;

                if (WallRequired(this.map, i, j))
                {
                    map[i, j] = 1;
                }
            }
        }
    }

    public bool WallRequired(int[,] map, int x, int y)
    {
        if (x == 0) return true;
        if (y == 0) return true;
        if (x == map.GetLength(0) - 1) return true;
        if (y == map.GetLength(1) - 1) return true;
        return false;
    }
}
