using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public int NumberOfPasses;
    public LayerMask playerMask;

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangeColor()
    {
        if (NumberOfPasses == 1)
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(255, 93, 0, 255);
        }
    }

    private void Update()
    {
        ChangeColor();
    }



}
