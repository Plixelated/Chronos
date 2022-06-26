using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class EditMenu : MonoBehaviour
{
    public List<string> savedCustomLevels;
    public GameObject entriesParent;
    public GameObject entries;
    public MapGenerator levelGenerator;
    public int selectedEntry;
    public GameObject editorUI;
    public GameObject editorEngine;
    public GridManager gridManager;
    public GameObject editorParent;
    public TileManager tilemanager;
    public GameObject selectionMenu;

    private void Start()
    {
        ActivateMenu();
    }

    private void LoadLevelList()
    {
        if (Directory.Exists(Path.Join(Application.persistentDataPath, "/custom")))
        {
            var customLevels = Directory.GetFiles(Path.Join(Application.persistentDataPath, "/custom"));

            savedCustomLevels.AddRange(customLevels);
        }
        else if (!Directory.Exists(Path.Join(Application.persistentDataPath, "/custom")))
        {
            Directory.CreateDirectory(Path.Join(Application.persistentDataPath, "/custom"));
        }

        if(savedCustomLevels.Count == 0)
        {
            var entry = Instantiate(entries, entriesParent.transform);
            entry.gameObject.GetComponentInChildren<TMP_Text>().text = "No Levels To Load";
        }

    }

    private void LoadEntries()
    {
        for (int i = 0; i < savedCustomLevels.Count; i++)
        {
            int entryIndex = i;
            var entry = Instantiate(entries, entriesParent.transform);
            string[] levelFormatted = savedCustomLevels[i].Split("custom\\");
            entry.gameObject.GetComponentInChildren<TMP_Text>().text = levelFormatted[1];
            entry.gameObject.GetComponent<Button>().onClick.AddListener( () => OnKeyPressed(entryIndex));
        }
    }

    private void OnKeyPressed(int index)
    {
        selectedEntry = index;
        SelectLevelToEdit();
    }

    private void ActivateMenu()
    {
        LoadLevelList();
        LoadEntries();
    }

    public void Cancel()
    {
        selectionMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SelectLevelToEdit()
    {
        var level = LoadLevelTexture();
        levelGenerator.maps.Add(level);
        levelGenerator.GenerateLevel(editorParent);
        SetGridSize(level);
        editorEngine.SetActive(true);
        LoadLevelTiles();
        editorUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SetGridSize(Texture2D level)
    {
        int x_start = 0;
        int y_start = 0;

        if (level.width == 10)
        {
            x_start = -3;
            y_start = -2;
        }
        else if (level.width == 12)
        {
            x_start = -5;
            y_start = -5;
        }
        else if (level.width == 13)
        {
            x_start = -6;
            y_start = -8;
        }
        gridManager.CreateGrid(level.width, level.height, x_start, y_start);
        gridManager.columns = level.width;
        gridManager.rows = level.height;
    }

    private void LoadLevelTiles()
    {
        foreach (Transform child in editorParent.transform)
        {
            PlacedTile tile = new PlacedTile();
            tile.position = child.position;
            tile.tile = child.gameObject; 
            tilemanager.placedTiles.Add(tile);
        }
    }

    private Texture2D LoadLevelTexture()
    {
        Texture2D level = null;
        byte[] levelData;

        if (File.Exists(savedCustomLevels[selectedEntry]))
        {
            levelData = File.ReadAllBytes(savedCustomLevels[selectedEntry]);
            level = new Texture2D(2, 2);
            level.LoadImage(levelData);
            level.Apply();
        }
        return level;
    }
}
