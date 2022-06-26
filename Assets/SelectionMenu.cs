using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenu : MonoBehaviour
{
    public GameObject newMenu;
    public GameObject editMenu;

    public void EditLevel()
    {
        editMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void NewLevel()
    { 
        newMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
