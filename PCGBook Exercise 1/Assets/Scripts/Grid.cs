using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public int sizeX;
    public int sizeY;
    public GameObject[] prefabs;
    public GameObject[,] gameObjects;
    GridData gridData;
    WaitForSeconds generationThrottle;

    private void Awake()
    {
        generationThrottle = new WaitForSeconds(0.1f);
    }

    void Start()
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
                gameObjects[i, j] = Instantiate(prefabs[gridData.map[i, j]], positionFromCoordinates(i, j), Quaternion.identity);
            }
        }
    }

    Vector3 positionFromCoordinates(int x, int y)
    {
        return new Vector3(x, y, 0);
    }
}
