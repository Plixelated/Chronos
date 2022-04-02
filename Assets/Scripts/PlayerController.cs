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

    public TextMeshProUGUI ageCount;

    private void OnEnable()
    {
       
    }


    private void OnDisable()
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        currentAge = startingAge;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetCell.position, movementSpeed * Time.deltaTime);

        var checkXObstacle = Physics2D.OverlapCircle(targetCell.position + new Vector3(input.xInput, 0f, 0f), 0.25f, obstacle);
        var checkXPath = Physics2D.OverlapCircle(targetCell.position + new Vector3(input.xInput, 0f, 0f), 0.25f, pathway);

        var checkYObstacle = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, input.yInput, 0f), 0.25f, obstacle);
        var checkYPath = Physics2D.OverlapCircle(targetCell.position + new Vector3(0f, input.yInput, 0f), 0.25f, pathway);

        if (Vector2.Distance(transform.position, targetCell.position) <= movementCheckThreshold)
        {

            if (Mathf.Abs(input.xInput) == 1)
            {

                if (!checkXObstacle && checkXPath)
                {
                    currentAge += agingRate;
                    ageCount.text = $"Age: {currentAge.ToString()}";
                    targetCell.position += new Vector3(input.xInput, 0f, 0f);

                    if (checkXPath.tag == "Swapper")
                    {
                        currentAge = startingAge;
                        ageCount.text = $"Age: {currentAge.ToString()}";
                        pathManager.ChangePath(checkXPath.gameObject.GetComponent<Swapper>().PathID);
                    }
                    if (checkXPath.tag == "Crumbler")
                    {
                        checkXPath.gameObject.GetComponent<Crumble>().SetTimer();
                    }
                    if (checkXPath.tag == "Breaker")
                    {
                        checkXPath.gameObject.GetComponent<Breaker>().NumberOfPasses--;
                    }

                    if (checkXPath.tag == "Finisher")
                    {
                        pathManager.levelCompleted = true;
                    }
                }
            }

            if (Mathf.Abs(input.yInput) == 1)
            {

                if (!checkYObstacle && checkYPath)
                {
                    currentAge += agingRate;
                    ageCount.text = $"Age: {currentAge.ToString()}";
                    targetCell.position += new Vector3(0f, input.yInput, 0f);
                    
                    if (checkYPath.tag == "Swapper")
                    {
                        currentAge = startingAge;
                        ageCount.text = $"Age: {currentAge.ToString()}";
                        pathManager.ChangePath(checkYPath.gameObject.GetComponent<Swapper>().PathID);
                    }

                    if (checkYPath.tag == "Crumbler")
                    {
                        checkYPath.gameObject.GetComponent<Crumble>().SetTimer();
                    }

                    if (checkYPath.tag == "Breaker")
                    {
                        checkYPath.gameObject.GetComponent<Breaker>().NumberOfPasses--;
                    }

                    if (checkYPath.tag == "Finisher")
                    {
                        pathManager.levelCompleted = true;
                    }
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (Physics2D.OverlapCircle(targetCell.position + new Vector3(input.xInput, 0f, 0f), 0.25f, obstacle) || Physics2D.OverlapCircle(targetCell.position + new Vector3(input.yInput, 0f, 0f), 0.25f, obstacle))
    //    {
    //        Gizmos.color = new Color32(255, 0, 0, 100);
    //    }
    //    else
    //    Gizmos.color = new Color32(0, 255, 0, 100);
    //    Gizmos.DrawCube(targetCell.position, new Vector3(0.5f, 0.5f));
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Clock")
        {
            currentAge -= agingRate;
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
    }

}
