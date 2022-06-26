using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject inGameUI;
    public GameObject editorUI;
    public GameObject playerObject;
    public GameObject level;
    public GameObject tileEditor;
    public GameObject stopButton;
    public GameObject playerSprite;
    public GameObject movementValidator;

    public static Action ageResetRequest;

    private void OnEnable()
    {
        PlayerController.playerDied += ResetLevel;
        GameManager.ageLimit += ResetLevel;
    }
    private void OnDisable()
    {
        PlayerController.playerDied -= ResetLevel;
        GameManager.ageLimit -= ResetLevel;
    }
    public void TestLevel()
    {

        foreach (Transform child in level.transform)
        {
            if (child.gameObject.tag == "Starter")
            {
                var startingPos = child.GetComponent<StarterTile>().start;

                if (playerSprite.GetComponent<PlayerController>().startingPosition != startingPos)
                {
                    playerSprite.GetComponent<PlayerController>().startingPosition = startingPos;
                }
                playerSprite.transform.position = startingPos;
                movementValidator.transform.position = startingPos;
            }
        }

        editorUI.SetActive(false);
        inGameUI.SetActive(true);
        playerObject.SetActive(true);
        tileEditor.SetActive(false);
        stopButton.SetActive(true);
        Debug.Log(playerSprite.transform.position);
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
                
                if (child.gameObject.GetComponent<Tile>() != null)
                    child.gameObject.GetComponent<Tile>().OnReset();
            }
        }
    }

    public void StopTest()
    {
        inGameUI.SetActive(false);
        editorUI.SetActive(true);
        playerObject.SetActive(false);
        tileEditor.SetActive(true);
        stopButton.SetActive(false);
        ResetTiles();
        Debug.Log(playerSprite.transform.position);
        Broadcaster.Send(ageResetRequest);
    }
}
