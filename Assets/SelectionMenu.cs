using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenu : MonoBehaviour
{
    public GameObject NewMenu;
    public GameObject EditMenu;

    public void EditLevel()
    { 
    
    }

    public void NewLevel()
    { 
        NewMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
