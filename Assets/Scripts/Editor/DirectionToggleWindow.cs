using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DirectionToggleWindow : EditorWindow
{
    public bool up_toggle;
    public bool down_toggle;
    public bool left_toggle;
    public bool right_toggle;

    [InitializeOnLoadMethod]
    static void Init()
    {
        MapGenerator.showDirectionWindow = ShowWindow; // note: there is no () on this
    }

    public static void ShowWindow()
    {
        GetWindow<DirectionToggleWindow>();
    }

    private void OnGUI()
    {
        up_toggle = EditorGUILayout.Toggle("Up", up_toggle);
        
    }


}

