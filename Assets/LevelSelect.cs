using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public int selectedLevel;
    public LevelLoader loader;

    public void MenuButton()
    {
        loader.LoadSpecificLevel("MainMenu");
    }

    public void OnThumbnailClick()
    {
        loader.LoadSpecificLevel(selectedLevel);
    }

    public void UpButton()
    {
        selectedLevel += 100;
    }

    public void DownButton()
    {
        selectedLevel -= 100;
    }

    public void LeftButton()
    {
        selectedLevel--;
    }

    public void RightButton()
    {
        selectedLevel++;
    }
}
