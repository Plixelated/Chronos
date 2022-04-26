using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedTile : Tile
{
    public bool unlocked;
    protected override void OnEnable()
    {
        base.OnEnable();
        KeyTile.unlocked += UnlockTile;
    }

    private void UnlockTile(bool unlock)
    {
        unlocked = unlock;

        Debug.Log("Status: " + unlock);

        if (unlocked)
            this.gameObject.layer = LayerMask.NameToLayer("Path");
        else
            this.gameObject.layer = LayerMask.NameToLayer("Obstacle");
    }
}
