using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExporter : MonoBehaviour
{
    public int col, row;

    public void ExportLevel()
    {
        var level = new Texture2D(col, row);

    }
}
