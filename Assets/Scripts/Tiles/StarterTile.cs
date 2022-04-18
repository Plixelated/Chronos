using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterTile : Tile
{
    public static Action<Vector3> startingPosition;
    private void Start()
    {
        Broadcaster.Send(startingPosition, this.transform.position);
    }
}
