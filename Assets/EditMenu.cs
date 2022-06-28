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
    public LevelExporter levelExporter;

    private void OnEnable()
    {
        MapGenerator.tileData += GetTileData;
        ActivateMenu();
    }

    private void OnDisable()
    {
        MapGenerator.tileData -= GetTileData;
        savedCustomLevels.Clear();
        foreach (Transform child in entriesParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void LoadLevelList()
    {
        Debug.Log("FILE PATH: " + Application.persistentDataPath);

        if (Directory.Exists(Path.Join(Application.persistentDataPath, "/custom")))
        {
            var customLevels = Directory.GetFiles(Path.Join(Application.persistentDataPath, "/custom"));

            savedCustomLevels.AddRange(customLevels);

            Debug.Log("SAVED ENTRIES LOADED");
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

        Debug.Log("SAVED ENTRIES: " + savedCustomLevels.Count);

    }

    private void LoadEntries()
    {
        for (int i = 0; i < savedCustomLevels.Count; i++)
        {
            int entryIndex = i;
            var entry = Instantiate(entries, entriesParent.transform);
            string[] levelFormatted = new string[] { };
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                levelFormatted = savedCustomLevels[i].Split("custom\\");
            }
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                levelFormatted = savedCustomLevels[i].Split("custom/");
            }
            Debug.Log(savedCustomLevels[i]);
            Debug.Log(levelFormatted.Length);
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

    public Vector2 SetOffset(Texture2D level)
    {
        if (level.width == 7 && level.height == 7)
        {
            return new Vector2(-3, -2);
        }
        else if (level.width == 11 && level.height == 12)
        {
            return new Vector2(-5, -5);
        }
        else if (level.width == 13 && level.height == 18)
        {
            return new Vector2(-6, -8);
        }

        return new Vector2(0, 0);
    }

    public void SelectLevelToEdit()
    {
        var level = LoadLevelTexture();
        levelGenerator.maps.Add(level);
        //SettOffset(level);
        levelGenerator.startingSpawnLocation = SetOffset(level);
        levelGenerator.GenerateLevel(editorParent);
        SetGridSize(level);
        editorEngine.SetActive(true);
        levelExporter.col = level.width;
        levelExporter.row = level.height;
        levelExporter.offset = -SetOffset(level);
        //LoadLevelTiles();
        editorUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SetGridSize(Texture2D level)
    {
        int x_start = 0;
        int y_start = 0;

        if (level.width == 7)
        {
            x_start = -3;
            y_start = -2;
        }
        else if (level.width == 11)
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

    private void GetTileData(Color color, GameObject prefab, Vector2 position, GameObject tileObject)
    {
        LoadLevelTiles(color, prefab, position, tileObject);
    }

    //private void LoadLevelTiles()
    //{
    //    foreach (Transform child in editorParent.transform)
    //    {
    //        PlacedTile tile = new PlacedTile();
    //        tile.position = child.position;
    //        tile.tile = child.gameObject;
    //        tilemanager.placedTiles.Add(tile);
    //    }
    //}

    private void LoadLevelTiles(Color color, GameObject prefab, Vector2 position, GameObject tileObject)
    {
        PlacedTile tile = new PlacedTile();
        tile.position = position;
        tile.tile = tileObject;
        tile.prefab = prefab;
        tile.color = color;
        tilemanager.placedTiles.Add(tile);
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
