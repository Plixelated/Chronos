using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTile : Tile
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    //public GameObject tileFace;
    public InputMonitor input;
    public SwipeDetection swipeDetection;

    public int xdirection;
    public int ydirection;

    public void CheckDirection(bool direction)
    {
        if (direction)
        {
            if (this.gameObject.layer != LayerMask.NameToLayer("Path"))
            {
                this.gameObject.layer = LayerMask.NameToLayer("Path");
            }
        }
        else if (!direction)
        {
            BlockMovement();
        }
    }

    //public void SetDirection()
    //{
    //    if (up)
    //    {
    //        Vector3 rotation = new Vector3(0f, 0f, 0f);
    //        Quaternion q = Quaternion.Euler(rotation);
    //        tileFace.transform.rotation = q;
    //    }

    //    if (down)
    //    {
    //        Vector3 rotation = new Vector3(0f, 0f, 180f);
    //        Quaternion q = Quaternion.Euler(rotation);
    //        tileFace.transform.rotation = q;
    //    }

    //    if (left)
    //    {
    //        Vector3 rotation = new Vector3(0f, 0f, 90f);
    //        Quaternion q = Quaternion.Euler(rotation);
    //        tileFace.transform.rotation = q;
    //    }

    //    if (right)
    //    {
    //        Vector3 rotation = new Vector3(0f, 0f, -90f);
    //        Quaternion q = Quaternion.Euler(rotation);
    //        tileFace.transform.rotation = q;
    //    }
    //}

    private void Start()
    {
        //SetDirection();
        if (input == null)
        { 
            input = FindObjectOfType<InputMonitor>();
        }
        if (swipeDetection == null)
        {
            swipeDetection = FindObjectOfType<SwipeDetection>();
        }
    }

    private void ResetDirections()
    {
        xdirection = 0;
        ydirection = 0;
    }

    protected override void OnExit()
    {
        BlockMovement();
    }

    public override void OnReset()
    {
        ResetDirections();
    }

    public void BlockMovement()
    {
        if (this.gameObject.layer != LayerMask.NameToLayer("Obstacle"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Obstacle");
        }
    }

    private void Update()
    {
        if (input.xInput != 0)
        {
            xdirection = (int)input.xInput;
            ydirection = 0;
        }
        if (input.yInput != 0)
        {
            ydirection = (int)input.yInput;
            xdirection = 0;
        }
        if (swipeDetection.xAxis != 0)
        {
            xdirection = (int)swipeDetection.xAxis;
            ydirection = 0;
        }
        if (swipeDetection.yAxis != 0)
        {
            ydirection = (int)swipeDetection.yAxis;
            xdirection = 0;
        }

        //right
        if (xdirection == -1)
        {
            CheckDirection(right);
        }
        //left
        if (xdirection == 1)
        {
            CheckDirection(left);
        }
        //up
        if (ydirection == -1)
        {
            CheckDirection(up);
        }
        //down
        if (ydirection == 1)
        {
            CheckDirection(down);
        }
    }
}
