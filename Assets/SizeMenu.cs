using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeMenu : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject editorUI;
    public GameObject editorObjects;

    private void OnEnable()
    {
        GridSize.grid_size += CreateNewGrid;
    }

    public void CreateNewGrid(Vector2 size, Vector2 startPos)
    {
        editorObjects.SetActive(true);
        editorUI.SetActive(true);
        gridManager.CreateGrid((int)size.x, (int)size.y, startPos.x, startPos.y);
        this.gameObject.SetActive(false);
    }
}