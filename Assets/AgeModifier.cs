using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgeModifier: MonoBehaviour
{
    public int ageModification;
    public TextMeshProUGUI modificationText;

    private void Update()
    {
        this.modificationText.text = $"x{this.ageModification.ToString()}";
    }
}
