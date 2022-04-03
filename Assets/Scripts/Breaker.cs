using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public int NumberOfPasses;
    public LayerMask playerMask;

    public SpriteRenderer spriteRenderer;

    public Sprite default_sprite;
    public Sprite broken_sprite;

    private void Start()
    {
        
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
            //this.GetComponent<SpriteRenderer>().color = new Color32(255, 93, 0, 255);
        }
    }

    private void Update()
    {
        ChangeColor();
    }



}
