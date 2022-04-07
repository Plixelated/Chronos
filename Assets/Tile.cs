using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public PathManager pathManager;
 

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
        var activeTile = this.GetComponent<Collider2D>();

        if (tile == activeTile)
        {
            Effect();
        }
    }

    protected virtual void Effect()
    {

    }
}
