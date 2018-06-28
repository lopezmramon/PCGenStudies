using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData {

    public int[,] map;
    public GridData GenerateGrid(int x, int y)
    {
        map = new int[x, y];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                map[i, j] = 0;
            }
        }

        return null;
    }

}
