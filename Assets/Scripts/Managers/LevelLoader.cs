using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime;
    public GameObject loadingScreen;
    public Slider loadingBar;
    public Animator transition;
    public void NextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        transition.SetBool("fade_out", false);
        transition.SetBool("fade_in", true);
    }

    IEnumerator LoadLevel(int levelIndex)
    {

        transition.SetBool("fade_out", true);
        transition.SetBool("fade_in", false);
        yield return new WaitForSeconds(transitionTime);

        loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBar.value = progress;

            yield return null;
        }
    }
}
