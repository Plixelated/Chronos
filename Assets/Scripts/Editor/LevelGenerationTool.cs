using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class LevelGenerationTool : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        float texturePositionY = 144;
        float texturePositionX = 25;
        float textureWidth = 128;
        float textureHeight = 128;
        float newTextureOffset = 132f;

        var generator = target as MapGenerator;

        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label("Path 1: ");
        GUILayout.BeginHorizontal();

        if (generator.maps != null)
        {
            if (generator.maps.Count > 0)
            {


                var mapPreview = generator.maps;

                Rect texturePosition = new Rect(texturePositionX + newTextureOffset, texturePositionY, textureWidth, textureHeight);


                foreach (Texture2D map in mapPreview)
                {
                    if (map != null)
                    {
                        var thumbnail = ResizeTexture(map, map.width * 10, map.height * 10);

                        GUILayout.Box(thumbnail, GUILayout.Width(128), GUILayout.Height(128));
                    }
                }

            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            if (GUILayout.Button("Generate"))
            {
                generator.GenerateLevel();
            }

        }

    }

    public Texture2D ResizeTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);

       int inc_x = Mathf.CeilToInt((float)targetWidth / (float)source.width);
       int inc_y = Mathf.CeilToInt((float)targetHeight / (float)source.height);

        for (int x = 0; x < targetWidth; x++)
        {
            for (int y = 0; y < targetHeight; y++)
            {
                var pixel = source.GetPixel(Mathf.CeilToInt(x/inc_x), Mathf.CeilToInt(y/inc_y));
                result.SetPixel(x, y, pixel);
            }
            
        }

        result.Apply();
        return result;
    }

}
