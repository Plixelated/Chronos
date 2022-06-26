using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TileButton : MonoBehaviour
{
    public GameObject prefab;
    public Sprite tileSprite;
    public static Action<GameObject> selectedTile;
    public static Action<Sprite> selectedSprite;
    public Image UIImage;

    private void Start()
    {
        //tileSprite = GetComponent<Image>().sprite;
        SetSprites();
    }
    public void SelectTile()
    {
        Broadcaster.Send(selectedTile, prefab);
        Broadcaster.Send(selectedSprite, tileSprite);
    }

    public void SetSprites()
    {
        if (prefab.GetComponentInChildren<SpriteRenderer>().sprite)
        {
            tileSprite = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            var image = prefab.GetComponentInChildren<SpriteRenderer>().sprite;
            UIImage.sprite = image;
        }
    }
}
