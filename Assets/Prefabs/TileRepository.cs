using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRepository : MonoBehaviour {

    public TileReference[] tiles;
    public MapGenerator mapGenerator;
    public void CopyList()
    {
        var colorMap = mapGenerator.colorMapping;

        for (int i = 0; i < colorMap.Length; i++)
        {
            Debug.Log("Copying...");
            tiles[i].tile = colorMap[i].prefab;
            tiles[i].color = colorMap[i].color;
        }
    }

    private void Start()
    {
        CopyList();
    }
}

