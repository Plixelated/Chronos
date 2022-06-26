using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExportMenu : MonoBehaviour
{
    public GameObject exportMenu;
    public LevelExporter exporter;
    public GameObject tileManager;
    public GameObject inputField;
    public string fileName;

    public void SaveLevel()
    {
        exportMenu.SetActive(true);
        tileManager.SetActive(false);
    }

    public void ExportLevel()
    {
        if (inputField.GetComponent<TMP_InputField>().text != null)
        {
            fileName = inputField.GetComponent<TMP_InputField>().text;
            exporter.ExportLevel(fileName);
        }
        else
        {
            exporter.ExportLevel();
        }

        fileName = "";
        inputField.GetComponent<TMP_InputField>().text = fileName;
        exportMenu.SetActive(false);
    }

    public void Cancel()
    {
        fileName = "";
        inputField.GetComponent<TMP_InputField>().text = fileName;
        exportMenu.SetActive(false);
        tileManager.SetActive(true);
    }
}
