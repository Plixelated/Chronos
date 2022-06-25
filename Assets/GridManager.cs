using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tile;
    public int columns, rows;
    public float x, y;
    public GameObject parent;

    public void CreateGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var gameObject = Instantiate(tile, new Vector2(x + i, y + j), Quaternion.identity, parent.transform);
                gameObject.name = $"{x + i},{y + j}";
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
            }
        }
    }

    public void CreateGrid(int _columns, int _rows, float x_start, float y_start)
    {
        columns = _columns;
        rows = _rows;
        x = x_start;
        y = y_start;

        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                var gameObject = Instantiate(tile, new Vector2(x_start + i, y_start + j), Quaternion.identity, parent.transform);
                gameObject.name = $"{x_start + i},{y_start + j}";
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
            }
        }
    }
}
