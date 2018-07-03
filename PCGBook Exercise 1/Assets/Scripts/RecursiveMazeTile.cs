using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveMazeTile : MonoBehaviour
{

    public GameObject northWall, southWall, eastWall, westWall;

    private void Awake()
    {
        northWall.name = "North Wall";
        eastWall.name = "East Wall";
        westWall.name = "West Wall";
        southWall.name = "South Wall";
    }
    public Directions wallDirections
    {
        set
        {
            switch (value)
            {
                case Directions.North:
                    northWall.SetActive(false);
                    break;
                case Directions.South:
                    southWall.SetActive(false);
                    break;
                case Directions.East:
                    eastWall.SetActive(false);
                    break;
                case Directions.West:
                    westWall.SetActive(false);
                    break;
            }
        }
    }
}
