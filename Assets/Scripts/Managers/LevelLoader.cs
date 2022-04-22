using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime;
    public float loadingBuffer;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public Animator transition;
    public int currentLevelIndex;

    private void Awake()
    {
        //Gets Current Level Progress
        GetCurrentLevel();
    }

    private void GetCurrentLevel()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") != 0)
            currentLevelIndex = PlayerPrefs.GetInt("CurrentLevel");
        else
        {
            currentLevelIndex = 1;
            SaveProgress();
        }

        //Debug.Log($"Current Level: {PlayerPrefs.GetInt("CurrentLevel")}");
    }

    private void SaveProgress()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("CurrentLevel", currentLevelIndex);
    }

    private void ResetTransitions()
    {
        transition.SetBool("fade_out", false);
        transition.SetBool("fade_in", true);
    }

    private void StartTransition()
    {
        if (!transition.GetBool("fade_out"))
            transition.SetBool("fade_out", true);
        if (transition.GetBool("fade_in"))
            transition.SetBool("fade_in", false);
    }

    //Loads Next Level
    public void NextLevel()
    {
        SaveProgress();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    //Loads Current Level
    public void LoadCurrentLevel()
    {
        StartCoroutine(LoadLevel(currentLevelIndex));
    }
    //Loads Level Selection Scene
    public void LoadLevelSelect()
    {
        StartCoroutine(LoadLevel("LevelSelect"));
    }

    public void LoadSpecificLevel(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    public void LoadSpecificLevel(string level)
    {
        StartCoroutine(LoadLevel(level));
    }

    //Loads a Specific Scene Using Name
    IEnumerator LoadLevel(string level)
    {
        StartTransition();

        yield return new WaitForSeconds(transitionTime);

        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(loadingBuffer);

        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBar.value = progress;

            yield return null;
        }
    }
    //Loads Level Using Index
    IEnumerator LoadLevel(int levelIndex)
    {

        StartTransition();

        yield return new WaitForSeconds(transitionTime);

        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(loadingBuffer);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBar.value = progress;

            yield return null;
        }
    }
}
