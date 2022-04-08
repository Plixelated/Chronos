using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementTile : Tile
{
    public int movementModifier;
    public static Action<int> modifier;
    public static Action<int> resetModifier;

    public override void Effect()
    {
        Broadcaster.Send(modifier, movementModifier);
    }

    protected override void OnExit()
    {
        Broadcaster.Send(resetModifier, 1);
    }
}
