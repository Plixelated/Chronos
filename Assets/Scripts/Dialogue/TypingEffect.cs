using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class TypingEffect : MonoBehaviour
{
    public InputMonitor input;

    public float delay;
    [TextArea]
    public string fullText;
    public string currentText;
    public TextMeshProUGUI textObject;
    public Coroutine current;
    public bool completed;
    public float textTimer;
    public float timer;
    public int buttonPress;
    public bool letPlayerMove;
    public bool disableTimer;
    public bool read;

    //public static Action<bool> playerMovement;

    private void OnEnable()
    {
        DisablePlayerControls();
        completed = false;
        current = StartCoroutine(TYPING());
        timer = textTimer;
        buttonPress = 0;
        read = false;
    }

    private void Awake()
    {
        if (input == null)
            input = GetComponent<InputMonitor>();
    }

    private void Start()
    {
        input.input.dialogue.Click.performed += CountPress;
    }

    private void Update()
    {

        if (buttonPress == 1 && !completed)
        {
            StopCoroutine(current);
            textObject.text = fullText;
            completed = true;
        }

        if (buttonPress > 1 && completed)
        {
            read = true;
        }

        //if (buttonPress == 2 && completed)
        //{
        //    this.gameObject.SetActive(false);
        //}
        //else if (completed && buttonPress < 2 && !disableTimer)
        //{
        //    timer -= Time.deltaTime;

        //    if (buttonPress > 2)
        //        timer = 0;

        //    if (timer <= 0)
        //    {
        //        this.gameObject.SetActive(false);
        //    }
        //}
    }

    public void CountPress(InputAction.CallbackContext obj)
    {
        //if (currentText.Length > 2)
        buttonPress++;
    }

    IEnumerator TYPING()
    {
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText.Substring(0, i);
            textObject.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        completed = true;
        buttonPress++;
    }

    public void DisablePlayerControls()
    {
        input.input.player.Disable();
        input.input.dialogue.Enable();
    }

    public void RestorePlayerControls()
    {
        input.input.dialogue.Disable();
        input.input.player.Enable();
    }

    private void OnDisable()
    {
        RestorePlayerControls();
    }
}
