using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherTile : Tile
{
    public override void Effect()
    {
        pathManager.levelCompleted = true;
    }
}
