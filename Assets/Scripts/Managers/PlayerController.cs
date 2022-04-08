using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public InputMonitor input;

    public Vector2 currentCell;

    public Transform targetCell;

    public float movementSpeed;

    public int movementModifier;

    public float movementCheckThreshold;

    public LayerMask obstacle;
    public LayerMask pathway;

    public PathManager pathManager;

    public int currentAge;
    //public int startingAge;
    //public int agingRate;


    public static Action playerDied;
    public static Action<string> tileType;
    public static Action<Collider2D> currentTile; 
    public static Action<int, string>modifiedAge;

    public Animator playerAnimator;

    Collider2D checkXPath;
    Collider2D checkYPath;
    Collider2D checkXObstacle;
    Collider2D checkYObstacle;

    private void OnEnable()
    {
        AgeManager.Age += GetCurrentAge;
        FreeMovementTile.modifier += GetMovementModifier;
        FreeMovementTile.resetModifier += ResetMovementModifier;
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
    }

    public void GetCurrentAge(int age)
    {
        currentAge = age;
    }

    public void GetMovementModifier(int modifier)
    {
        movementModifier += modifier;
    }

    public void ResetMovementModifier(int modifier)
    {
        movementModifier = modifier;
    }

    public void CheckPath()
    {
        checkXPath = Physics2D.OverlapCircle(targetCell.position + new Vector3(input.xInput, 0f, 0f), 0.25f, pathway);
        checkYPath = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, input.yInput, 0f), 0.25f, pathway);
    }

    public void CheckForObstacle()
    {
        checkXObstacle = Physics2D.OverlapCircle(targetCell.position + new Vector3(input.xInput, 0f, 0f), 0.25f, obstacle);
        checkYObstacle = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, input.yInput, 0f), 0.25f, obstacle);
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

    public void ValidateTile(float xInput, float yInput)
    {
        var landingTile = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, input.yInput * movementModifier, 0f), 0.25f, pathway);
        if (movementModifier > 1 && yInput != 0)
            landingTile.GetComponent<Tile>().Effect();

        landingTile = Physics2D.OverlapCircle(targetCell.position + new Vector3(input.xInput * movementModifier, 0f, 0f), 0.25f, pathway);
        if (movementModifier > 1 && xInput != 0)
            landingTile.GetComponent<Tile>().Effect();


        targetCell.position += new Vector3(xInput * movementModifier, yInput * movementModifier, 0f);
    }

    public void MovePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetCell.position, movementSpeed * Time.deltaTime);
    }

    public void SetAnimatorValues()
    {
        playerAnimator.SetInteger("age", currentAge);
        playerAnimator.SetInteger("x_axis", (int)input.xInput);
        playerAnimator.SetInteger("y_axis", (int)input.yInput);
    }

    public void XAxisAnimations()
    {
        playerAnimator.SetInteger("direction_x", (int)input.xInput);
        playerAnimator.SetInteger("direction_y", 0);
    }

    public void YAxisAnimations()
    {
        playerAnimator.SetInteger("direction_y", (int)input.yInput);
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
            if (ValidateInput(input.xInput) == 1)
            {
                FlipSprite(input.xInput);
                XAxisAnimations();
                if (!checkXObstacle && checkXPath)
                {
                    CheckTileType(checkXPath);
                    ValidateTile(input.xInput, 0f);
                }
            }
            if (ValidateInput(input.yInput) == 1)
            {
                YAxisAnimations();
                if (!checkYObstacle && checkYPath)
                {
                    CheckTileType(checkYPath);
                    ValidateTile(0f, input.yInput);
                }
            }
        }
    }



    // Update is called once per frame
    void Update()
    {

        CheckPath();
        CheckForObstacle();
        //MovePlayer();
        SetAnimatorValues();
        ValidateMovement();
        MovePlayer();
    }

    public void ReturnToIdle()
    {
        playerAnimator.SetInteger("y_axis", 0);
        playerAnimator.SetInteger("x_axis", 0);
    }



    private void OnDrawGizmos()
    {
        if (Physics2D.OverlapCircle(targetCell.position + new Vector3(input.xInput, 0f, 0f), 0.25f, obstacle) || Physics2D.OverlapCircle(targetCell.position + new Vector3(input.yInput, 0f, 0f), 0.25f, obstacle))
        {
            Gizmos.color = new Color32(255, 0, 0, 100);
        }
        else
            Gizmos.color = new Color32(0, 255, 0, 100);
        Gizmos.DrawCube(targetCell.position, new Vector3(0.5f, 0.5f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Reducer" || collision.tag == "Ager")
        {
            var modifier = collision.GetComponent<AgeModifier>();
            Broadcaster.Send(modifiedAge, modifier.ageModification, collision.tag);
            collision.gameObject.SetActive(false);
        }
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
