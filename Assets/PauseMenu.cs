using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameManager manager;
    public LevelLoader levelLoader;

    public void ResumeButton()
    { 
        manager.ResumeGame();
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        levelLoader.LoadSpecificLevel("MainMenu");
    }

    public void LevelsButton()
    {
        Time.timeScale = 1f;
        levelLoader.LoadLevelSelect();  
    }

    public void SettingsButton()
    {
        Debug.Log("SETTINGS");
    }
}
