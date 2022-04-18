using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public List<GameObject> pathways;
    //public int maxAge;

    public int currentPath;
    public bool levelCompleted;

    // Start is called before the first frame update
    void Start()
    {
        levelCompleted = false;
        ResetPaths();

    }

    public void ResetPaths()
    {
        currentPath = 0;

        for (int i = 0; i < pathways.Count; i++)
        {
            if (i == currentPath)
            {
                if (!pathways[i].activeSelf)
                {
                    pathways[i].SetActive(true);
                }
            }
            if (i != currentPath)
            {
                if (pathways[i].activeSelf)
                {
                    pathways[i].SetActive(false);
                }
            }
        }
    }

    public void ChangePath(int pathID)
    {
        if (pathways[currentPath].gameObject.activeSelf)
        {
            pathways[pathID].gameObject.SetActive(true);
            pathways[currentPath].gameObject.SetActive(false);
            currentPath = pathID;
        }
    }

}
