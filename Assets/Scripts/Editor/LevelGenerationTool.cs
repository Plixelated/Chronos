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
        var generator = target as MapGenerator;

        if (generator.map != null)
        {
            var mapPreview = generator.map;

            //var aspectRatio = ((float)generator.map.height / (float)generator.map.width);
            //GUILayout.Box(generator.map, GUILayout.Width(Screen.width), GUILayout.Height(Screen.width * aspectRatio));
            //GUILayout.Box(generator.map, GUILayout.MinWidth(13f), GUILayout.MinHeight(13f));
            //Rect position = new Rect(0, 0, 128, 128);
            //GUI.DrawTexture(position, generator.map);
            //EditorGUI.DrawPreviewTexture(new Rect(0,0,128,128), generator.map);

            //var window = EditorWindow.GetWindow<EditorWindow>();
            //var windowSize = window.position.size;

            float texturePositionY = 144;
            float texturePositionX = 25;
            float textureWidth = 128;
            float textureHeight = 128;
            float newTextureOffset = 0f;

            Rect texturePosition = new Rect(texturePositionX + newTextureOffset, texturePositionY, textureWidth, textureHeight);

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("Path 1: ");
            //GUILayout.Space(138);
            GUILayout.BeginHorizontal();

            var thumbnail = ResizeTexture(mapPreview, mapPreview.width*10, mapPreview.height*10);

            GUILayout.Box(thumbnail, GUILayout.Width(128), GUILayout.Height(128));
            //EditorGUI.DrawTextureTransparent(texturePosition, mapPreview);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            //Texture2D mapTexture = AssetPreview.GetAssetPreview(generator.map);
            //GUILayout.Label(mapTexture, GUILayout.MinHeight(128f), GUILayout.MinWidth(128f));
            //EditorGUI.DrawTextureTransparent(new Rect(0, 0, windowSize.x*2, windowSize.y), generator.map) ;
            
            



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
