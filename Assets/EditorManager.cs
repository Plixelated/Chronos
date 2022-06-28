using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public Camera mainCam;
    public GridManager gridManager;
    public int defaultZoom;

    public GameObject selectionMenu;
    public GameObject editorMenu;
    public GameObject editorEngine;
    public GameObject grid;
    public GameObject level;
    public MapGenerator generator;
    public GameObject saveConfirmation;
    public GameObject tileManager;

    private void Start()
    {
        mainCam.GetComponent<Camera>().orthographicSize = defaultZoom;
    }

    public void Update()
    {
        if (gridManager.gameObject.activeSelf)
        {
            if (mainCam.GetComponent<Camera>().orthographicSize != (gridManager.columns + 2))
            {
                mainCam.GetComponent<Camera>().orthographicSize = (gridManager.columns + 2);
            }
        }
    }

    public void EmptyGrid()
    {
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RemoveTiles()
    {
        foreach (Transform child in level.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RefreshMapGenerator()
    { 
        generator.maps.Clear();
    }

    public void ResetLevelSpawnPosition()
    {
        level.transform.position = Vector2.zero;
    }

    public void ResetEditorSettings()
    {
        EmptyGrid();
        RemoveTiles();
        RefreshMapGenerator();
        ResetLevelSpawnPosition();
    }

    public void ExitEditor()
    {
        ResetEditorSettings();
        editorMenu.SetActive(false);
        if (saveConfirmation.activeSelf)
        {
            saveConfirmation.SetActive(false);
            tileManager.SetActive(true);
        }
        tileManager.GetComponent<TileManager>().placedTiles.Clear();
        editorEngine.SetActive(false);
        selectionMenu.SetActive(true);
    }
}
