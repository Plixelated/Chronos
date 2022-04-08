using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public PathManager pathManager;
    public Collider2D currentTile;
    public Collider2D previousTile;
 

    protected virtual void OnEnable()
    {
        PlayerController.currentTile += GetCurrentTile;
    }

    protected virtual void OnDisable()
    {
        PlayerController.currentTile -= GetCurrentTile;
    }

    protected virtual void Awake()
    {
        if (pathManager == null)
        {
            pathManager = FindObjectOfType<PathManager>();
        }
    }

    private void GetCurrentTile(Collider2D tile)
    {
        previousTile = currentTile;
        currentTile = tile;
        var activeTile = this.GetComponent<Collider2D>();

        if (tile == activeTile)
        {
            Effect();
        }
        if (previousTile == activeTile)
        {
            OnExit();
        }

    }

    protected virtual void OnExit() { }

    public virtual void Effect() { }
}
