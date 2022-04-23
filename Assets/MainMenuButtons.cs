using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public LevelLoader level;

    public void PlayButton()
    {
        level.LoadSavedLevel();
    }

    public void LevelsButton()
    {
        level.LoadLevelSelect();
    }

    public void SettingsButton()
    {
        Debug.Log("SETTINGS");
    }
}
