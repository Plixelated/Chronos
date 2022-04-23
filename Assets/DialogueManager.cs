using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public List<TypingEffect> dialogueList;
    public TypingEffect currentDialogue;
    public int index;

    private void Start()
    {
        index = 0;
        currentDialogue = dialogueList[index];
        currentDialogue.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        NextDialogueWindow();
    }

    public void NextDialogueWindow()
    {
        if (currentDialogue.read)
        {
            currentDialogue.gameObject.SetActive(false);
            if (index < dialogueList.Count-1)
            {
                index++;
                currentDialogue = dialogueList[index];
                currentDialogue.gameObject.SetActive(true);
            }
        }
    }
}
