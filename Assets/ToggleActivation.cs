using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActivation : MonoBehaviour
{

    private void OnEnable()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Deactivate()
    {
        this.GetComponentInParent<Crumble>().gameObject.SetActive(false);
    }

    public void ToggleSprite()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Reactivate()
    {
        this.gameObject.SetActive(true);
    }
}
