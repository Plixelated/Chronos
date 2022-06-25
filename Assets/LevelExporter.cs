using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelExporter : MonoBehaviour
{
    public int col, row;
    public TileManager tileManager;
    public Vector2 offset = new Vector2(6f, 8f);

    public void ExportLevel()
    {
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

        var pngData = level.EncodeToPNG();

        if (!Directory.Exists(Path.Join(Application.persistentDataPath, "/custom")))
        {
            Directory.CreateDirectory(Path.Join(Application.persistentDataPath, "/custom"));
        }

        File.WriteAllBytes(Path.Join(Application.persistentDataPath, $"/custom/default.png"), pngData);
    }

    public void ExportLevel(string fileName)
    {
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

        var pngData = level.EncodeToPNG();

        if (!Directory.Exists(Path.Join(Application.persistentDataPath, "/custom")))
        {
            Directory.CreateDirectory(Path.Join(Application.persistentDataPath, "/custom"));
        }
        
        File.WriteAllBytes(Path.Join(Application.persistentDataPath, $"/custom/{fileName}.png"), pngData);
    }
}
