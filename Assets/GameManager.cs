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
    public void ResetLevel()
    {
        player.currentAge = player.startingAge;
        pathManager.ResetPaths();
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.transform.position = startingCoordinates;
        movementChecker.transform.position = startingCoordinates;
        player.GetComponent<SpriteRenderer>().enabled = true;
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
