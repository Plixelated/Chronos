using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMonitor : MonoBehaviour
{
    public InputController input;

    public float yInput;
    public float xInput;

    //public static Action up;
    //public static Action down;
    //public static Action left;
    //public static Action right;

    public int up;
    public int down;
    public int left;
    public int right;

    public float holdDelay;
    public float timer;


    // Start is called before the first frame update
    void Start()
    {
        input = new InputController();
        input.player.Enable();

        input.player.XAxis.started += xAxis;
        input.player.XAxis.canceled += ResetAxis;

        input.player.YAxis.performed += yAxis;
        input.player.YAxis.canceled += ResetAxis;
    }

    private void xAxis(InputAction.CallbackContext obj)
    {
        if (xInput == 0)
        {
            xInput = input.player.XAxis.ReadValue<float>();
            timer = holdDelay;
        }
    }

    private void yAxis(InputAction.CallbackContext obj)
    {
        if (yInput == 0)
        {
            yInput = input.player.YAxis.ReadValue<float>();
            timer = holdDelay;
        }
    }

    private void ResetAxis(InputAction.CallbackContext obj)
    {
        xInput = 0;
        yInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = xInput = yInput = 0;
            }
        }
    }
}
