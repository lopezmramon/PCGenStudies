using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GridGenerator
{
    private System.Random random;
    private int totalSize;
    public Cell[,] cells;
    List<Cell> cellList = new List<Cell>();
    public Cell[,] RetrieveCells
    {
        get
        {
            if (cells == null)
            {
                Debug.Log("Which cells?");
                return null;
            }
            return cells;

        }
    }

    #region recursive backtracker


    public Dictionary<Directions, int> directionValuesX = new Dictionary<Directions, int>();
    public Dictionary<Directions, int> directionValuesY = new Dictionary<Directions, int>();

    public void InitializeRecursiveBacktracker(int sizeX, int sizeY, int seed)
    {
        random = new System.Random(seed);
        directionValuesX.Add(Directions.North, 0);
        directionValuesX.Add(Directions.South, 0);
        directionValuesX.Add(Directions.East, 1);
        directionValuesX.Add(Directions.West, -1);

        directionValuesY.Add(Directions.North, 1);
        directionValuesY.Add(Directions.South, -1);
        directionValuesY.Add(Directions.East, 0);
        directionValuesY.Add(Directions.West, 0);

        totalSize = sizeX * sizeY;
        cells = new Cell[sizeX, sizeY];
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                cells[i, j] = new Cell(i, j);
                cellList.Add(cells[i, j]);
            }
        }
        int startingX = random.Next(0, sizeX - 1);
        int startingY = random.Next(0, sizeY - 1);
        Debug.Log(startingY + " < y --- x > " + startingX);
        CarvePassageFrom(startingX, startingY, cells);
    }

    private void CarvePassageFrom(int currentX, int currentY, Cell[,] cells)
    {
        List<Directions> directions = new List<Directions>();
        directions.Add(Directions.North);
        directions.Add(Directions.South);
        directions.Add(Directions.East);
        directions.Add(Directions.West);

        cells[currentX, currentY].visited = true;

        for (int i = 0; i < 4; i++)
        {
            Directions randomDirection = directions[random.Next(0, directions.Count)];
            if (directions.Count == 1)
            {
                randomDirection = 0;
            }
            int newX = currentX + directionValuesX[randomDirection];
            int newY = currentY + directionValuesY[randomDirection];
            if (newX >= 0 && newY >= 0 && newX < cells.GetLength(0) && newY < cells.GetLength(1) && !cells[newX, newY].visited)
            {
                cells[currentX, currentY].OpenPathFromCurrentCellPerspective = randomDirection;
                cells[currentX, currentY].OpenPathFromPreviousCellPerspective = randomDirection;
                CarvePassageFrom(newX, newY, cells);
                break;
            }
            directions.Remove(randomDirection);
            if (cellList.Count > 0)
            {
                CarvePassageFrom(currentX, currentY, cells);

            }
        }

        cellList.Remove(cells[currentX, currentY]);
    }



    #endregion

    #region own generator
    private bool[,] hasFloor;
    private int[,] map;
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
    #endregion

}
