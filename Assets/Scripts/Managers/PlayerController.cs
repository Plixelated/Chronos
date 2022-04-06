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

    public float movementCheckThreshold;

    public LayerMask obstacle;
    public LayerMask pathway;

    public PathManager pathManager;

    public int currentAge;
    public int startingAge;
    public int agingRate;


    public static Action playerDied;

    public Animator playerAnimator;

    //Move to UI Manager

    public float notificationTimer;
    public float notificationlength;

    public TextMeshProUGUI ageCount;
    public TextMeshProUGUI ageModifierNotification;

    Collider2D checkXPath;
    Collider2D checkYPath;
    Collider2D checkXObstacle;
    Collider2D checkYObstacle;

    private void OnEnable()
    {

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

    public void ValidateTile(float xInput, float yInput, float movementModifier)
    {
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
        return tile.tag;
    }

    public void ExecuteTileEffect(Collider2D path, float inputx, float inputy)
    {
        if (CheckTileType(path) != "Ager" || CheckTileType(path) != "Reducer")
            SetAge(agingRate);

        if (CheckTileType(path) == "Doubler")
        {
            ValidateTile(inputx, inputy, 2f);
        }
        else
            ValidateTile(inputx, inputy, 1f);

        if (CheckTileType(path) == "Swapper")
        {
            currentAge = startingAge;
            ageCount.text = $"Age: {currentAge.ToString()}";
            pathManager.ChangePath(path.gameObject.GetComponent<Swapper>().PathID);
        }

        if (CheckTileType(path) == "Crumbler")
        {
            path.gameObject.GetComponent<Crumble>().SetTimer();
        }

        if (CheckTileType(path) == "Breaker")
        {
            path.gameObject.GetComponent<Breaker>().NumberOfPasses--;
        }

        if (CheckTileType(path) == "Finisher")
        {
            pathManager.levelCompleted = true;
        }
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
                    ExecuteTileEffect(checkXPath, input.xInput, 0f);
                }
            }
            if (ValidateInput(input.yInput) == 1)
            {
                YAxisAnimations();
                if (!checkYObstacle && checkYPath)
                {
                    ExecuteTileEffect(checkYPath, 0f, input.yInput);
                }
            }
        }
    }

    //Move To Age Manager

    public void SetAge(int age)
    {
        currentAge += age;
        SetAgeNotification($"+{age}");
        ageCount.text = $"Age: {currentAge.ToString()}";
    }

    public void ResetAge()
    {
        currentAge = startingAge;
        ageCount.text = $"Age: {currentAge.ToString()}";
    }



    // Start is called before the first frame update
    void Start()
    {
        currentAge = startingAge;
    }

    // Update is called once per frame
    void Update()
    {

        CheckPath();
        CheckForObstacle();
        MovePlayer();
        SetAnimatorValues();
        ValidateMovement();

        if (notificationTimer > 0)
        {
            notificationTimer -= Time.deltaTime;
            if (notificationTimer <= 0)
            {
                notificationTimer = 0;
                ageModifierNotification.gameObject.SetActive(false);
            }
        }

    }

    public void ReturnToIdle()
    {
        playerAnimator.SetInteger("y_axis", 0);
        playerAnimator.SetInteger("x_axis", 0);
    }

    public void SetAgeNotification(string notif)
    {
        if (!ageModifierNotification.gameObject.activeSelf)
        {
            ageModifierNotification.gameObject.SetActive(true);
            ageModifierNotification.text = notif;
            notificationTimer = notificationlength;
        }
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
        if (collision.tag == "Reducer")
        {
            var reducer = collision.GetComponent<AgeModifier>();
            currentAge -= reducer.ageModification;
            SetAgeNotification($"-{reducer.ageModification}");
            ageCount.text = $"Age: {currentAge.ToString()}";
            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "Ager")
        {
            var reducer = collision.GetComponent<AgeModifier>();
            currentAge += reducer.ageModification;
            SetAgeNotification($"+{reducer.ageModification+5}");
            ageCount.text = $"Age: {currentAge.ToString()}";
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Breaker")
        {
            var breaker = collision.GetComponent<Breaker>();
            if (breaker.NumberOfPasses == 0)
            {
                breaker.Deactivate();
            }
        }

        if (collision.tag == "Crumbler")
        {

            if (!Physics2D.OverlapCircle(this.transform.position, 0.25f, pathway))
            {
                if (playerDied != null)
                    playerDied();
            }
        }
    }

}
