using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterTile : Tile
{
    public Vector3 start;
    public static Action<Vector3> startingPosition;
    private void Start()
    {
        start = this.transform.position;
        base.Awake();
        Broadcaster.Send(startingPosition, start);
        Debug.Log($"Starting Position Sent: {start}");
    }
}
