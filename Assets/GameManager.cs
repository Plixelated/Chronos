using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startingTile;
    public LevelLoader levelLoader;
    public PathManager pathManager;
    public PlayerController player;
    public Vector3 startingCoordinates;
    public GameObject movementChecker;

    public static Action hasReset;

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

        if (hasReset != null)
            hasReset();

        player.currentAge = player.startingAge;
        player.ageCount.text = $"Age: {player.currentAge.ToString()}";
        ResetTiles();
        pathManager.ResetPaths();
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.position = startingCoordinates;
        movementChecker.transform.position = startingCoordinates;
        player.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ResetTiles()
    {

        for (int i = 0; i < pathManager.pathways.Count; i++)
        {
            var paths = pathManager.pathways[i].GetComponentsInChildren<Transform>(true);

            foreach (Transform child in paths)
            {
                if (!child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(true);
                }
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentAge > pathManager.maxAge)
        {
            ResetLevel();
        }

        if (pathManager.levelCompleted)
        {
            levelLoader.NextLevel();
        }
    }
}
