using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelExporter : MonoBehaviour
{
    public int col, row;
    public TileManager tileManager;
    public Vector2 offset;
    public GameObject exportMenu;
    public GameObject confirmationMenu;
    public string fileName;
    private byte[] pngData;
    public GameObject saveConfirmation;
    private float saveNotificationTimer = 1f;

    public void SetGridOffset()
    {
        if (col == 7 && row == 7)
        {
            offset = new Vector2(3, 2);
        }
        else if (col == 11 && row == 12)
        {
            offset = new Vector2(5, 5);
        }
        else if (col == 13 && row == 18)
        {
            offset = new Vector2(6, 8);
        }
    }

    public void ExportLevel()
    {
        SetGridOffset();

        var level = new Texture2D(col, row);
        var levelTiles = tileManager.placedTiles;

        for (int i = 0; i < levelTiles.Count; i++)
        {
            var x = (int)levelTiles[i].position.x + (int)offset.x;
            var y = (int)levelTiles[i].position.y + (int)offset.y;
            var color = levelTiles[i].color;
            if (color.a != 1)
                color.a = 1;
            level.SetPixel(x, y, color);
        }

        level.filterMode = FilterMode.Point;
        level.Apply();

        var pngData = level.EncodeToPNG();

        fileName = "default";

        ExportToFile();

    }

    public void ExportLevel(string _fileName)
    {
        SetGridOffset();

        var level = new Texture2D(col, row);
        var levelTiles = tileManager.placedTiles;

        for (int i = 0; i < levelTiles.Count; i++)
        {
            var x = (int)levelTiles[i].position.x + (int)offset.x;
            var y = (int)levelTiles[i].position.y + (int)offset.y;
            var color = levelTiles[i].color;
            level.SetPixel(x, y, color);
        }

        level.filterMode = FilterMode.Point;
        level.Apply();

        pngData = level.EncodeToPNG();

        fileName = _fileName;

        ExportToFile();
    }

    public void ExportToFile()
    { 

        if (!Directory.Exists(Path.Join(Application.persistentDataPath, "/custom")))
        {
            Directory.CreateDirectory(Path.Join(Application.persistentDataPath, "/custom"));
        }

        if (!File.Exists(Path.Join(Application.persistentDataPath, $"/custom/{fileName}.png")))
        {
            File.WriteAllBytes(Path.Join(Application.persistentDataPath, $"/custom/{fileName}.png"), pngData);
            ConfirmSave();
        }
        else
            Confirm();
    }

    public void Confirm()
    {
        tileManager.gameObject.SetActive(false);
        exportMenu.SetActive(false);
        confirmationMenu.SetActive(true);
    }

    public void Overwrite()
    {
        File.WriteAllBytes(Path.Join(Application.persistentDataPath, $"/custom/{fileName}.png"), pngData);
        confirmationMenu.SetActive(false);
        ConfirmSave();
    }

    public void Cancel()
    {
        confirmationMenu.SetActive(false);
        exportMenu.SetActive(true);
    }

    public void ConfirmSave()
    {
        saveConfirmation.SetActive(true);
        StartCoroutine("SaveNotification");
    }

    public IEnumerator SaveNotification()
    {
        yield return new WaitForSeconds(saveNotificationTimer);
        saveConfirmation.SetActive(false);
        tileManager.gameObject.SetActive(true);
    }
}
