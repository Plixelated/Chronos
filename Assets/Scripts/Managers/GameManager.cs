using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public PathManager pathManager;
    public PlayerController player;
    public Vector3 startingCoordinates;
    public GameObject movementChecker;
    public GameObject loadingScreen;
    public AgeManager ageManager;
    public GameObject hourglass;
    public bool resetPlayer;

    public static Action<bool> resetting;

    public Animator fade;
    public float resetDelay;

    public GameObject pauseMenu;

    private void OnEnable()
    {
        PlayerController.playerDied += ResetLevel;
        StarterTile.startingPosition += GetStartingPosition;
    }

    private void OnDisable()
    {
        PlayerController.playerDied -= ResetLevel;
        StarterTile.startingPosition -= GetStartingPosition;
    }

    private void GetStartingPosition(Vector3 pos)
    {
        Debug.Log("Received Starting Coordinates");
        startingCoordinates = pos;
    }

    public void ResetLevel()
    {
        fade.SetBool("fade_in", false);
        fade.SetBool("fade_out", true);
        StartCoroutine(ResetDelay());
    }

    public void ResetObjects()
    {
        player.transform.position = startingCoordinates;
        movementChecker.transform.position = startingCoordinates;

        ResetTiles();
        ageManager.ResetAge();
        pathManager.ResetPaths();
        player.GetComponent<SpriteRenderer>().enabled = false;

        player.GetComponent<SpriteRenderer>().enabled = true;
        Broadcaster.Send(resetting, false);

        fade.SetBool("fade_in", true);
        fade.SetBool("fade_out", false);

        hourglass.SetActive(false);
        resetPlayer = false;
        player.canMove = true;
    }

    public IEnumerator ResetDelay()
    {
        player.canMove = false;

        yield return new WaitForSeconds(resetDelay/2);

        hourglass.SetActive(true);

        yield return new WaitForSeconds(resetDelay);
        ResetObjects();
    }

    public IEnumerator LevelTransition()
    {
        yield return new WaitForSeconds(resetDelay);
        levelLoader.NextLevel();
    }

    public void ResetTiles()
    {

        for (int i = 0; i < pathManager.pathways.Count; i++)
        {
            var paths = pathManager.pathways[i].GetComponentsInChildren<Transform>(true);

            foreach (Transform child in paths)
            {
                if (child.tag != "Particle")
                child.gameObject.SetActive(true);
                
                if (child != null)
                {
                    var tile = child.GetComponent<Tile>();
                    if (tile != null)
                    {
                        tile.OnReset();
                    }
                }
            }
        }
    

    }

    private void CheckPlayerAge()
    {
        if (player.currentAge >= ageManager.maxAge)
        {
            if (!resetPlayer && !pathManager.levelCompleted)
            {
                ResetLevel();
                resetPlayer = true;
            }
        }
    }

    private void LevelCompletion()
    {
        if (pathManager.levelCompleted)
        {
            fade.SetBool("fade_out", true);
            fade.SetBool("fade_in", false);
            StartCoroutine(LevelTransition());
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        player.transform.position = startingCoordinates;
        player.targetCell.transform.position = startingCoordinates;

        if (loadingScreen.activeSelf)
        {
            loadingScreen.SetActive(false);
        }
        player.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerAge();
        LevelCompletion();
    }
}
