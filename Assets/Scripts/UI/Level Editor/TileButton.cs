using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public GameObject currentTile;
    public Sprite tileSprite;
    public static Action<GameObject> selectedTile;
    public static Action<Sprite> selectedSprite;

    private void Start()
    {
        tileSprite = GetComponent<Image>().sprite;
    }
    public void SelectTile()
    {
        Broadcaster.Send(selectedTile, currentTile);
        Broadcaster.Send(selectedSprite, tileSprite);
    }
}
