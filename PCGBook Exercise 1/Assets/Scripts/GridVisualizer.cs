using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    private GridGenerator gridGenerator;
    private bool waitingForGrid;
    private Cell[,] cells;
    public GameObject[] prefabs;
    public GameObject floorPrefab;
    public GameObject recursiveMazeTilePrefab;
    public GameObject[,] gameObjects;
    private WaitForSeconds generationThrottle;

    private void Start()
    {
        gridGenerator = new GridGenerator();
    }

    public void GenerateRecursivelyBacktrackedGrid()
    {
        gridGenerator.InitializeRecursiveBacktracker(5, 5, 5);
        waitingForGrid = true;

    }

    private void Update()
    {
        if (waitingForGrid)
        {
            this.cells = gridGenerator.RetrieveCells;

            if (this.cells == null)
            {
                waitingForGrid = true;
            }
            else
            {
                waitingForGrid = false;
                StartCoroutine(InstantiateGrid(cells));
            }
        }
    }

    private void DisplayGrid(Cell[,] cells)
    {

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
                gameObjects[i, j] = Instantiate(prefabs[gridData.map[i, j]], PositionFromCoordinates(i, j, (TileType)gridData.map[i, j]), Quaternion.Euler(RotationFromCoordinates(i, j, (TileType)gridData.map[i, j], cells)), this.transform);
            }
        }
    }

    IEnumerator InstantiateGrid(Cell[,] cells)
    {
        gameObjects = new GameObject[cells.GetLength(0), cells.GetLength(1)];

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                yield return generationThrottle;
                gameObjects[i, j] = (GameObject)Instantiate(recursiveMazeTilePrefab, PositionFromCoordinates(i, j, 0), Quaternion.Euler(RotationFromCoordinates(i, j, 0, cells)), this.transform);
                RecursiveMazeTile tile = gameObjects[i, j].GetComponent<RecursiveMazeTile>();
                tile.southWall.SetActive(cells[i, j].southWall);
                tile.northWall.SetActive(cells[i, j].northWall);
                tile.eastWall.SetActive(cells[i, j].eastWall);
                tile.westWall.SetActive(cells[i, j].westWall);

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

    Vector3 RotationFromCoordinates(int x, int y, TileType tileType, object[,] objects)
    {
        switch (tileType)
        {
            case TileType.Wall:
                if (y != 0 && y + 1 != objects.GetLength(1))
                {
                    return new Vector3(0, 90, 0);
                }
                break;
        }

        return Vector3.zero;
    }
}

