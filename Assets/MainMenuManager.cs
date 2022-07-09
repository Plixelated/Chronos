using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    public InputController input;
    public Animator animator;
    public Animator buttonsAnimator;

    private void OnEnable()
    {
        input.main_menu.Enable();
    }

    private void OnDisable()
    {
        input.main_menu.Disable();
    }

    private void Awake()
    {
        input = new InputController();
        if (Application.targetFrameRate <= 120)
            Application.targetFrameRate = 120;
        else if (Application.targetFrameRate <= 60)
            Application.targetFrameRate = 60;
    }
    private void Start()
    {
        input.main_menu.Click.performed += Skip;
    }

    public void Skip(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("skip");
        buttonsAnimator.SetTrigger("skip");
    }


}
