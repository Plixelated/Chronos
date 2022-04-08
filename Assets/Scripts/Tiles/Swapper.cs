using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : Tile
{
    public int PathID;
    public static Action resetAgeRequest;

    public override void Effect()
    {
        Broadcaster.Send(resetAgeRequest);
        pathManager.ChangePath(PathID);
    }
}
