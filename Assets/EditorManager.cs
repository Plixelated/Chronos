using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public Camera mainCam;
    public GridManager gridManager;
    public int defaultZoom;

    private void Start()
    {
        mainCam.GetComponent<Camera>().orthographicSize = defaultZoom;
    }

    public void Update()
    {
        if (gridManager.gameObject.activeSelf)
        {
            if (mainCam.GetComponent<Camera>().orthographicSize != (gridManager.columns + 1))
            {
                mainCam.GetComponent<Camera>().orthographicSize = (gridManager.columns + 1);
            }
        }
    }
}
