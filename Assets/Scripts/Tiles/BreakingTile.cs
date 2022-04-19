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

    public CameraShake cameraShake;


    private void Start()
    {
        if (cameraShake == null)
            cameraShake = FindObjectOfType<CameraShake>();

        currentPasses = 0;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Effect()
    {
        StartCoroutine(cameraShake.Shake(0.1f, 0.1f));

        if (currentPasses < maxPasses)
        {
            currentPasses++;
        }

    }

    public override void OnReset()
    {
        currentPasses = 0;
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
