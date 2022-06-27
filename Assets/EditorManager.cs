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

    public void ExitEditor()
    {
        EmptyGrid();
        editorMenu.SetActive(false);
        editorEngine.SetActive(false);
        selectionMenu.SetActive(true);
    }
}
