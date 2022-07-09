using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LongClickButton))]
public class ShowSubmenu : MonoBehaviour
{
    public GameObject submenu;
    public RectTransform viewRect;
    public float viewRectHeight = 385;
    public float defaultViewRectHeight;

    private void OnEnable()
    {
        TileManager.cancel += Cancel;
    }

    private void OnDisable()
    {
        TileManager.cancel -= Cancel;
    }

    private void Start()
    {
        defaultViewRectHeight = viewRect.rect.height;
    }

    public void ToggleMenu()
    {
        if (!submenu.activeSelf)
        {
            submenu.gameObject.SetActive(true);
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
        viewRect.sizeDelta = new Vector2(viewRect.sizeDelta.x, defaultViewRectHeight);
    }
}
