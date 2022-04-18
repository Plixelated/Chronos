using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovementTile : Tile
{
    public bool yAxis;
    public InputMonitor input;

    public int movementModifier;

    public static Action<int> modifier;
    //public static Action<int> resetModifier;

    public int xMovement;
    public int yMovement;

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerController.y_movement += GetYAxis;
        PlayerController.x_movement += GetXAxis;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerController.y_movement -= GetYAxis;
        PlayerController.x_movement -= GetXAxis;
    }

    private void GetYAxis(int y)
    { 
        yMovement = y;
    }

    private void GetXAxis(int x)
    {
        xMovement = x;
    }

    private void Start()
    {
        if (input == null)
            input = FindObjectOfType<InputMonitor>();
    }

    public override void Effect()
    {
        if (/*input.yInput*/yMovement != 0 && yAxis || 
            /*input.xInput*/ xMovement!= 0 && !yAxis)
            Broadcaster.Send(modifier, movementModifier);

    }

    protected override void OnExit()
    {
        Broadcaster.Send(modifier, 1);
    }
}
