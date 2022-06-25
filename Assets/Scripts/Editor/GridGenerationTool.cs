using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridGenerationTool : Editor
{
    public override void OnInspectorGUI()
    {
        var generator = target as GridManager;

        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
        {
            generator.CreateGrid();
        }
    }
}
