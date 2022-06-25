using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSize : MonoBehaviour
{
    public Vector2 gridSize;
    public Vector2 startingPosition;
    public static Action<Vector2, Vector2> grid_size;

    public void SendGridSize()
    {
        Broadcaster.Send(grid_size, gridSize, startingPosition);
    }
}
