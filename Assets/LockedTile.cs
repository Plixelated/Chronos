using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedTile : Tile
{
    protected override void OnEnable()
    {
        base.OnEnable();
        KeyTile.unlocked += UnlockTile;
    }

    private void UnlockTile(bool unlock)
    {
        if (unlock)
            this.gameObject.layer = LayerMask.NameToLayer("Path");
    }
}
