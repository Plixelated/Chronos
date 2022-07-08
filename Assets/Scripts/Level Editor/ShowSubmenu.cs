using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LongClickButton))]
public class ShowSubmenu : MonoBehaviour
{
    public GameObject submenu;
    public TileManager tileManager;
    public RectTransform viewRect;
    public float viewRectHeight = 385;
    public float defaultViewRectHeight;

    private void Start()
    {
        defaultViewRectHeight = viewRect.rect.height;
    }

    public void ToggleMenu()
    {
        if (!submenu.activeSelf)
        {
            submenu.gameObject.SetActive(true);
            tileManager.canPlaceTile = false;
            viewRect.sizeDelta = new Vector2(viewRect.sizeDelta.x, viewRectHeight);
        }
        else if (submenu.activeSelf)
        {
            Cancel();
        }
        
    }

    public void Cancel()
    {
        submenu.gameObject.SetActive(false);
        tileManager.canPlaceTile = true;
        viewRect.sizeDelta = new Vector2(viewRect.sizeDelta.x, defaultViewRectHeight);
    }
}
