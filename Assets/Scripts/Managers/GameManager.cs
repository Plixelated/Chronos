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
    public static Action hasReset;

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

        if (hasReset != null)
            hasReset();

        StartCoroutine(ResetPause());
    }

    public void ResetObjects()
    {
        player.transform.position = startingCoordinates;
        movementChecker.transform.position = startingCoordinates;

        ResetTiles();
        player.currentAge = player.startingAge;
        player.ageCount.text = $"Age: {player.currentAge.ToString()}";
        pathManager.ResetPaths();
        player.GetComponent<SpriteRenderer>().enabled = false;

        player.GetComponent<SpriteRenderer>().enabled = true;
        fade.SetBool("fade_in", true);
        fade.SetBool("fade_out", false);
    }

    public IEnumerator ResetPause()
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
                //if (!child.gameObject.activeSelf)
                //{
                    child.gameObject.SetActive(true);
                    var crumbleTile = child.GetComponent<Crumble>();
                    if (crumbleTile != null)
                    {
                        crumbleTile.resetting = true;
                    }
                //}
            }
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
        if (player.currentAge > pathManager.maxAge)
        {
            player.input.xInput = 0;
            player.input.yInput = 0;
            ResetLevel();
        }

        if (pathManager.levelCompleted)
        {
            fade.SetBool("fade_out", true);
            fade.SetBool("fade_in", false);
            StartCoroutine(LevelTransition());
        }
    }
}