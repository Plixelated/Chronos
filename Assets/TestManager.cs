using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject editorUI;
    public GameObject player;
    public GameObject engine;
    public GameObject level;
    public GameObject tileEditor;
    public GameObject stopButton;

    private void OnEnable()
    {
        PlayerController.playerDied += ResetLevel;
    }
    private void OnDisable()
    {
        PlayerController.playerDied -= ResetLevel;
    }
    public void TestLevel()
    {
        foreach (Transform child in level.transform)
        {
            if (child.gameObject.tag != "Starter")
            {
                Debug.LogWarning("ERROR: NO STARTING TILE FOUND SPAWNING PLAYER IN DEFAULT POSITION");
            }
        }

        editorUI.SetActive(false);
        inGameUI.SetActive(true);
        player.SetActive(true);
        engine.SetActive(true);
        tileEditor.SetActive(false);
        stopButton.SetActive(true);
    }

    public IEnumerator ResetDelay()
    {
        yield return new WaitForSeconds(1.25f);
        ResetTiles();
    }

    public void ResetLevel()
    {
        StartCoroutine("ResetDelay");
    }

    public void ResetTiles()
    {
        foreach (Transform child in level.transform)
        {
            if (child != null)
            {
                if (!child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void StopTest()
    {
        inGameUI.SetActive(false);
        editorUI.SetActive(true);
        player.SetActive(false);
        engine.SetActive(false);
        tileEditor.SetActive(true);
        stopButton.SetActive(false);
        ResetTiles();
    }
}
