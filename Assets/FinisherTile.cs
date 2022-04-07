using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherTile : Tile
{
    protected override void Effect()
    {
        pathManager.levelCompleted = true;
    }
}
