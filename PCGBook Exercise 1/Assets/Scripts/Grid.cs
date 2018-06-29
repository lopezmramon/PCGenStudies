using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int sizeX;
    public int sizeY;
    public GameObject[] prefabs;
    public GameObject floorPrefab;
    public GameObject[,] gameObjects;
    GridData gridData;
    WaitForSeconds generationThrottle;

    private void Awake()
    {
        generationThrottle = new WaitForSeconds(0.1f);
    }

    void Start()
    {
        gridData = new GridData();
        gridData.GenerateGrid(sizeX, sizeY);
        StartCoroutine(InstantiateGrid(gridData));
    }

    IEnumerator InstantiateGrid(GridData gridData)
    {
        gameObjects = new GameObject[gridData.map.GetLength(0), gridData.map.GetLength(1)];

        for (int i = 0; i < gridData.map.GetLength(0); i++)
        {
            for (int j = 0; j < gridData.map.GetLength(1); j++)
            {
                yield return generationThrottle;
                if (gridData.hasFloor[i, j] == true)
                {
                    gameObjects[i, j] = Instantiate(floorPrefab, PositionFromCoordinates(i, j, TileType.Floor), Quaternion.identity, this.transform);
                }
                gameObjects[i, j] = Instantiate(prefabs[gridData.map[i, j]], PositionFromCoordinates(i, j, (TileType)gridData.map[i, j]), Quaternion.Euler(RotationFromCoordinates(i, j, (TileType)gridData.map[i, j])), this.transform);
            }
        }
    }

    Vector3 PositionFromCoordinates(int x, int y, TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Floor:
                return new Vector3(x, 0, y);
            case TileType.Wall:
                return new Vector3(x, 0.5f, y);
        }

        return new Vector3(x, 0, y);
    }

    Vector3 RotationFromCoordinates(int x, int y, TileType tileType)
    {
        switch (tileType)
        {
            case TileType.Wall:
                if (y != 0 && y +1 != gridData.map.GetLength(1))
                {
                    return new Vector3(0, 90, 0);
                }
                break;
        }

        return Vector3.zero;
    }
}
