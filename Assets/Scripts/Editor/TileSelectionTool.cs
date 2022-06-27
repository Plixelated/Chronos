using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

[CustomEditor(typeof(TileButton))]
public class TileSelectionTool : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var tileButton = target as TileButton;

        if (GUILayout.Button("Update"))
        {
            tileButton.name = tileButton.prefab.name;
            tileButton.SetSprites();
        }
    }
}
