using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgeModifier: Tile
{
    public int ageModification;
    public TextMeshProUGUI modificationText;
    public bool ager;

    private void Update()
    {
        if (ager)
        this.modificationText.text = $"+{this.ageModification.ToString()}";
        else
            this.modificationText.text = $"-{this.ageModification.ToString()}";
    }

}
