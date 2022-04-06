using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public int NumberOfPasses;
    public int startingPasses;

    public SpriteRenderer spriteRenderer;

    public Sprite default_sprite;
    public Sprite broken_sprite;

    private void Start()
    {
        NumberOfPasses = startingPasses;
    }

    private void OnEnable()
    {
        NumberOfPasses = startingPasses;
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangeColor()
    {
        if (NumberOfPasses == 1)
        {
            this.spriteRenderer.sprite = broken_sprite;
        }
    }

    private void Update()
    {
        ChangeColor();
    }



}
