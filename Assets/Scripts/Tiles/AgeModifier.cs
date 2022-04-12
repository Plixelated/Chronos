using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgeModifier: Tile
{
    public int ageModification;
    public TextMeshProUGUI modificationText;
    public bool ager;
    public GameObject sprite;
    public static Action<int, string> modifiedAge;
    public string type;

    private void Start()
    {
        type = this.tag;
    }

    public override void Effect()
    {
        if (sprite.activeSelf)
        {
            sprite.SetActive(false);
            Broadcaster.Send(modifiedAge, this.ageModification, this.tag);
        }
        else
            this.tag = "Path";
    }

    public override void OnReset()
    {
        if (!sprite.activeSelf)
        sprite.SetActive(true);
        if (this.tag != type)
            this.tag = type;
    }

    private void Update()
    {
        if (ager)
        this.modificationText.text = $"+{this.ageModification.ToString()}";
        else
            this.modificationText.text = $"-{this.ageModification.ToString()}";
    }

}
