using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject player;
    public GameObject engine;
    public GameObject level;
    public GameObject tileEditor;

    public void TestLevel()
    {
        foreach (Transform child in level.transform)
        {
            if (child.gameObject.tag != "Starter")
            {
                Debug.LogWarning("ERROR: NO STARTING TILE FOUND SPAWNING PLAYER IN DEFAULT POSITION");
            }
        }

        inGameUI.SetActive(true);
        player.SetActive(true);
        engine.SetActive(true);
        tileEditor.SetActive(false);
    }
}
