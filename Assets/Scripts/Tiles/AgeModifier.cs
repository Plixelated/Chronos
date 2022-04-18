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
    public bool reset;
    public GameObject sprite;
    public static Action<int, string> modifiedAge;
    public string type;

    public GameObject particle;

    protected override void OnEnable()
    {
        base.OnEnable();
        AgeManager.StartingAge += getStartingAge;
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        AgeManager.StartingAge += getStartingAge;
    }

    private void getStartingAge(int age)
    {
        if (reset)
        {
            if (ageModification != age)
            ageModification = age;
        }
    }

    private void Start()
    {
        Debug.Log($"Tile Location {this.transform.position}");
        Debug.Log($"Text Location {this.modificationText.transform.position}");
        type = this.tag;
    }

    public override void Effect()
    {
        if (sprite.activeSelf)
        {
            sprite.SetActive(false);
            Broadcaster.Send(modifiedAge, this.ageModification, this.tag);
            if (!this.particle.activeSelf)
            this.particle.SetActive(true);
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
        this.particle.SetActive(false);
    }

    private void Update()
    {
        if (ager)
            this.modificationText.text = $"+{this.ageModification.ToString()}";
        else if (reset)
            this.modificationText.text = this.ageModification.ToString();
        else
            this.modificationText.text = $"-{this.ageModification.ToString()}";
    }

}
