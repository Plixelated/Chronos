using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startingTile;
    public LevelLoader levelLoader;
    public PathManager pathManager;
    public PlayerController player;
    public Vector3 startingCoordinates;
    public GameObject movementChecker;
    public GameObject loadingScreen;
    public AgeManager ageManager;

    public static Action<bool> resetting;

    public Animator fade;
    public float resetDelay;

    private void OnEnable()
    {
        PlayerController.playerDied += ResetLevel;
    }

    private void OnDisable()
    {
        PlayerController.playerDied -= ResetLevel;
    }
    public void ResetLevel()
    {
        fade.SetBool("fade_in", false);
        fade.SetBool("fade_out", true);

        //if (hasReset != null)
        //    hasReset();

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
        fade.SetBool("fade_in", true);
        fade.SetBool("fade_out", false);
    }

    public IEnumerator ResetDelay()
    {
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
                child.gameObject.SetActive(true);
                var crumbleTile = child.GetComponent<CrumbleTile>();
                var breakingTile = child.GetComponent<BreakingTile>();
                var oneWayTile = child.GetComponent<OneWayTile>();
                if (crumbleTile != null)
                {
                    crumbleTile.OnReset();
                }

                if (breakingTile != null)
                {
                    breakingTile.OnReset();
                }

                if (oneWayTile != null)
                { 
                    oneWayTile.OnReset();
                }
            }
        }
    

    }

    private void CheckPlayerAge()
    {
        if (player.currentAge >= pathManager.maxAge)
        {
            player.input.xInput = 0;
            player.input.yInput = 0;
            ResetLevel();
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

    private void Awake()
    {
        startingCoordinates = startingTile.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (loadingScreen.activeSelf)
        {
            loadingScreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerAge();
        LevelCompletion();
    }
}
