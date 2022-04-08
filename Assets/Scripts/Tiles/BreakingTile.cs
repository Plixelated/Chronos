using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingTile : Tile
{
    public int maxPasses;
    public int currentPasses;

    public SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;


    private void Start()
    {
        currentPasses = 0;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Effect()
    {
        if (currentPasses < maxPasses)
        {
            currentPasses++;
        }

    }

    protected override void OnExit()
    {
        if (currentPasses >= maxPasses)
            Break();
    }

    public void Break()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangeSprite()
    {
        this.spriteRenderer.sprite = sprites[currentPasses];
    }

    private void Update()
    {
        ChangeSprite();
    }



}
