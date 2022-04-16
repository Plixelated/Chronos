using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputMonitor : MonoBehaviour
{
    public InputController input;

    public float yInput;
    public float xInput;

    public float holdDelay;
    public float timer;

    public static Action<Vector2, float> StartTouch;
    public static Action<Vector2, float> EndTouch;

    [SerializeField]
    private Camera mainCamera;


    private void OnEnable()
    {
        input.player.Enable();
    }

    private void OnDisable()
    {
        input.player.Disable();
    }

    private void Awake()
    {
        //mainCamera = Camera.main;
        input = new InputController();
    }


    // Start is called before the first frame update
    void Start()
    {
        input.player.XAxis.started += xAxis;
        input.player.XAxis.canceled += ResetAxis;

        input.player.YAxis.performed += yAxis;
        input.player.YAxis.canceled += ResetAxis;

        input.player.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        input.player.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        Broadcaster.Send(StartTouch, Utils.ScreenToWorld(mainCamera, input.player.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        Broadcaster.Send(EndTouch, Utils.ScreenToWorld(mainCamera, input.player.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, input.player.PrimaryPosition.ReadValue<Vector2>());
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
