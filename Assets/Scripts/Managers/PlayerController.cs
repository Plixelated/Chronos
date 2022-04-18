using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public InputMonitor input;

    public float xMovement;
    public float yMovement;

    public SwipeDetection swipe;

    public Vector2 currentCell;

    public Transform targetCell;

    public Vector3 startingPosition;

    public float movementSpeed;

    public int movementModifier;

    public float movementCheckThreshold;

    public LayerMask obstacle;
    public LayerMask pathway;

    public PathManager pathManager;

    public int currentAge;
    //public int startingAge;
    //public int agingRate;

    public bool resetting;


    public static Action playerDied;
    public static Action<string> tileType;
    public static Action<Collider2D> currentTile;
    //public static Action<int, string>modifiedAge;
    public static Action<int> y_movement;
    public static Action<int> x_movement;

    public Animator playerAnimator;

    Collider2D checkXPath;
    Collider2D checkYPath;
    Collider2D checkXObstacle;
    Collider2D checkYObstacle;

    private void OnEnable()
    {
        AgeManager.Age += GetCurrentAge;
        FreeMovementTile.modifier += GetMovementModifier;
        GameManager.resetting += GetResetStatus;
        StarterTile.startingPosition += GetStartingPosition;
        //FreeMovementTile.resetModifier += ResetMovementModifier;
    }

    /// <summary>
    /// Get Input
    /// Check if path is valid
    /// if path is valid, move validator
    /// check type of tile
    /// execute tile effect
    /// move player
    /// </summary>


    private void OnDisable()
    {
        AgeManager.Age -= GetCurrentAge;
        FreeMovementTile.modifier -= GetMovementModifier;
        GameManager.resetting -= GetResetStatus;
        StarterTile.startingPosition -= GetStartingPosition;
    }

    public void GetCurrentAge(int age)
    {
        currentAge = age;
    }

    public void GetMovementModifier(int modifier)
    {
        movementModifier = modifier;
    }

    //public void ResetMovementModifier(int modifier)
    //{
    //    movementModifier = modifier;
    //}

    private void GetResetStatus(bool status)
    {
        resetting = status;
    }

    private void GetStartingPosition(Vector3 pos)
    {
        startingPosition = pos;
    }

    public void GetMovementDirection()
    {
        if (input.xInput != 0)
            xMovement = input.xInput;
        else if (swipe.xAxis != 0)
        {
            xMovement = swipe.xAxis;
            Debug.Log(xMovement);
        }
        else
            xMovement = 0;

        if (input.yInput != 0)
            yMovement = input.yInput;
        else if (swipe.yAxis != 0)
        {
            yMovement = swipe.yAxis;
            Debug.Log(yMovement);
        }
        else
            yMovement = 0;

        Broadcaster.Send(y_movement, (int)yMovement);
        Broadcaster.Send(x_movement, (int)xMovement);

    }

    public void CheckPath()
    {
        checkXPath = Physics2D.OverlapCircle(targetCell.position + new Vector3(/*input.xInput*/xMovement, 0f, 0f), 0.25f, pathway);
        checkYPath = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, /*input.yInput*/yMovement, 0f), 0.25f, pathway);
    }

    public void CheckForObstacle()
    {
        checkXObstacle = Physics2D.OverlapCircle(targetCell.position + new Vector3(/*input.xInput*/xMovement, 0f, 0f), 0.25f, obstacle);
        checkYObstacle = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, /*input.yInput*/yMovement, 0f), 0.25f, obstacle);
    }

    public float ValidateInput(float input)
    {
        return Mathf.Abs(input);
    }

    public void FlipSprite(float input)
    {
        if (input == -1)
        {
            Vector3 rotation = new Vector3(0f, 180f, 0f);
            Quaternion q = Quaternion.Euler(rotation);
            player.transform.rotation = q;
        }
        else if (input == 1)
        {
            Vector3 rotation = new Vector3(0f, 0f, 0f);
            Quaternion q = Quaternion.Euler(rotation);
            player.transform.rotation = q;
        }
    }

    public void MoveValidator(float xInput, float yInput)
    {
        if (movementModifier == 1)
        {
            targetCell.position += new Vector3(xInput, yInput, 0f);
        }
        else if (movementModifier > 1)
        {
            Debug.Log(movementModifier);
            if (yInput != 0)
            {
                var landingTile = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, yInput * movementModifier, 0f), 0.25f, pathway);
                if (landingTile)
                {
                    while (landingTile.tag == "Doubler")
                    {
                        movementModifier++;
                        landingTile = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, yInput * movementModifier, 0f), 0.25f, pathway);
                    }

                    if (landingTile)
                    {
                        targetCell.position += new Vector3(xInput, yInput * movementModifier, 0f);
                        CheckTileType(landingTile);
                    }
                }
            }
            if (xInput != 0)
            {
                var landingTile = Physics2D.OverlapCircle(targetCell.position + new Vector3(xInput * movementModifier, 0f, 0f), 0.25f, pathway);
                if (landingTile)
                {
                    while (landingTile.tag == "Doubler")
                    {
                        movementModifier++;
                        landingTile = Physics2D.OverlapCircle(targetCell.position + new Vector3(xInput * movementModifier, 0f, 0f), 0.25f, pathway);
                    }

                    if (landingTile)
                    {
                        targetCell.position += new Vector3(xInput * movementModifier, yInput, 0f);
                        CheckTileType(landingTile);
                    }
                }
            }
        }
    }

    public void MovePlayer()
    {
        //transform.position = Vector2.MoveTowards(transform.position, targetCell.position, movementSpeed * Time.deltaTime);
        transform.position = Vector2.Lerp(transform.position, targetCell.position,movementSpeed * Time.deltaTime);
    }

    public void SetAnimatorValues()
    {
        playerAnimator.SetInteger("age", currentAge);
        playerAnimator.SetInteger("x_axis", (int)/*input.xInput*/xMovement);
        playerAnimator.SetInteger("y_axis", (int)/*input.yInput)*/yMovement);
    }

    public void XAxisAnimations()
    {
        playerAnimator.SetInteger("direction_x", (int)/*input.xInput*/xMovement);
        playerAnimator.SetInteger("direction_y", 0);
    }

    public void YAxisAnimations()
    {
        playerAnimator.SetInteger("direction_y", (int)/*input.yInput)*/yMovement);
        playerAnimator.SetInteger("direction_x", 0);
    }

    public bool ValidateMovementThreshold()
    {
        return Vector2.Distance(transform.position, targetCell.position) <= movementCheckThreshold;
    }

    public string CheckTileType(Collider2D tile)
    {
        Broadcaster.Send(currentTile, tile);
        return tile.tag;
    }


    public void ValidateMovement()
    {
        if (ValidateMovementThreshold())
        {
            if (ValidateInput(/*input.xInput*/xMovement) == 1)
            {
                FlipSprite(/*input.xInput*/xMovement);
                XAxisAnimations();
                if (/*!checkXObstacle && */checkXPath)
                {
                    CheckTileType(checkXPath);
                    MoveValidator(/*input.xInput*/xMovement, 0f);
                }
                //else
                //    Debug.Log("Unable to Move");
            }
            if (ValidateInput(/*input.yInput*/yMovement) == 1)
            {
                YAxisAnimations();
                if (/*!checkYObstacle && */checkYPath)
                {
                    CheckTileType(checkYPath);
                    MoveValidator(0f, /*input.yInput*/yMovement);
                }
                //else
                //    Debug.Log("Unable to Move");
            }
        }
    }

    private void Start()
    {
        if (transform.position != startingPosition)
        {
            transform.position = startingPosition;
            targetCell.gameObject.transform.position = startingPosition;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!resetting)
        {
            GetMovementDirection();
            CheckPath();
            //CheckForObstacle();
            //MovePlayer();
            SetAnimatorValues();
            ValidateMovement();
            //MovePlayer();
        }
    }

    private void FixedUpdate()
    {
        if (!resetting)
        {
            MovePlayer();
        }
    }

    public void ReturnToIdle()
    {
        playerAnimator.SetInteger("y_axis", 0);
        playerAnimator.SetInteger("x_axis", 0);
    }



    private void OnDrawGizmos()
    {
        if (Physics2D.OverlapCircle(targetCell.position + new Vector3(/*input.xInput*/xMovement, 0f, 0f), 0.25f, obstacle) || 
            Physics2D.OverlapCircle(targetCell.position + new Vector3(/*input.yInput*/yMovement, 0f, 0f), 0.25f, obstacle))
        {
            Gizmos.color = new Color32(255, 0, 0, 100);
        }
        else
            Gizmos.color = new Color32(0, 255, 0, 100);
        Gizmos.DrawCube(targetCell.position, new Vector3(0.5f, 0.5f));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Crumbler")
        {

            if (!Physics2D.OverlapCircle(this.transform.position, 0.25f, pathway))
            {
                Broadcaster.Send(playerDied);
            }
        }
    }

}
