using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementTile : Tile
{
    public bool yAxis;
    public InputMonitor input;

    public int movementModifier;

    public static Action<int> modifier;
    //public static Action<int> resetModifier;

    private void Start()
    {
        if (input == null)
            input = FindObjectOfType<InputMonitor>();
    }

    public override void Effect()
    {
        if (input.yInput != 0 && yAxis || input.xInput != 0 && !yAxis)
            Broadcaster.Send(modifier, movementModifier);

    }

    protected override void OnExit()
    {
        Broadcaster.Send(modifier, 1);
    }
}
