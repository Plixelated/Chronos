using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class MapGenerator : MonoBehaviour
{
    //public Texture2D maps;
    public List<Texture2D> maps;

    public ColorToPrefab[] colorMapping;

    public PathManager pathManager;

    public Vector2 startingSpawnLocation;

    public List<GameObject> generatedLevels;

    public static Action showDirectionWindow;

    public List<GameObject> pathAlts;

    public int altSpawnThreshold;

    public Color defaultPathColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void GenerateLevel()
    {
        for (int i = 0; i < maps.Count; i++)
        {
            GameObject parent = new GameObject($"Path {i+1}");
            parent.transform.position = Vector2.zero;

            for (int x = 0; x < maps[i].width; x++)
            {
                for (int y = 0; y < maps[i].height; y++)
                {
                    GenerateTile(x, y, i, parent);
                }
            }

            parent.transform.position = startingSpawnLocation;

            if (pathManager.pathways.Count > 0)
            {
                if (pathManager.pathways[i] == null)
                    pathManager.pathways[i] = parent;
            }
            else
                pathManager.pathways.Add(parent);
        }
    }

    public void GenerateTile(int x, int y, int counter, GameObject parent) 
    { 
        Color pixelColor = maps[counter].GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            //Ignore's transparent pixels
            return;
        }

        

        foreach (ColorToPrefab colorMapping in colorMapping)
        {
            if (VerifyColor(colorMapping.color, pixelColor))
            {
                Vector2 position  = new Vector2(x, y);

                if (pixelColor == defaultPathColor)
                {
                    int threshold = altSpawnThreshold;
                    if (UnityEngine.Random.Range(0, 100) <= threshold)
                    {
                        int alt = UnityEngine.Random.Range(0, pathAlts.Count);
                        Instantiate<GameObject>(pathAlts[alt], position, Quaternion.identity, parent.transform);
                    }
                    else
                        Instantiate<GameObject>(colorMapping.prefab, position, Quaternion.identity, parent.transform);
                }
                else
                {
                    var tile = Instantiate<GameObject>(colorMapping.prefab, position, Quaternion.identity, parent.transform);

                    var swapperTile = tile.GetComponent<Swapper>();
                    var oneWayTile = tile.GetComponent<OneWayTile>();

                    if (swapperTile != null)
                    {
                        swapperTile.PathID = counter + 1;
                    }

                    //if (oneWayTile != null)
                    //{
                    //    Broadcaster.Send(showDirectionWindow);

                    //}
                }

                break;
            }
        }
    }

    private bool VerifyColor(Color comparison, Color source)
    {
        if ((int)(comparison.r * 1000) == (int)(source.r * 1000) &&
                (int)(comparison.g * 1000) == (int)(source.g * 1000) &&
                (int)(comparison.b * 1000) == (int)(source.b * 1000))
        {
            return true;
        }
        else return false;
    }

}
