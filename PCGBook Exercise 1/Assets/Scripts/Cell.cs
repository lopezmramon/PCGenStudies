using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{

    public bool visited = false;
    public bool northWall = true;
    public bool southWall = true;
    public bool eastWall = true;
    public bool westWall = true;
    public Directions OpenPathFromCurrentCellPerspective
    {
        set
        {
            switch (value)
            {
                case Directions.East:
                    eastWall = false;
                    break;
                case Directions.North:
                    northWall = false;
                    break;
                case Directions.South:
                    southWall = false;
                    break;
                case Directions.West:
                    westWall = false;
                    break;
            }
        }
    }
    public Directions OpenPathFromPreviousCellPerspective
    {
        set
        {
            switch (value)
            {
                case Directions.East:
                    westWall = false;
                    break;
                case Directions.North:
                    southWall = false;
                    break;
                case Directions.South:
                    northWall = false;
                    break;
                case Directions.West:
                    eastWall = false;
                    break;
            }
        }
    }
    public int x, y;

    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;

    }
}
