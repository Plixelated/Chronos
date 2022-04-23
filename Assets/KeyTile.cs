using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTile : Tile
{
    public GameObject key;

    public static Action<bool> unlocked;

    public override void Effect()
    {
        if (key.activeSelf)
        { 
            key.SetActive(false);
            Broadcaster.Send(unlocked, true);
        }

    }
}
