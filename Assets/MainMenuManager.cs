using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    public InputController input;
    public Animator animator;

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
    }
    private void Start()
    {
        input.main_menu.Click.performed += Skip;
    }

    public void Skip(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("skip");
    }


}
