using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = 0.2f;
    [SerializeField]
    private float maximumTime = 1;
    [SerializeField, Range(0f,1f)]
    private float directionThreshold = 0.9f;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    [SerializeField]
    private GameObject trail;

    private Coroutine coroutine;

    private InputMonitor input;

    [SerializeField]
    private float inputThreshold;

    public float xAxis;
    public float yAxis;

    private void Awake()
    {
        if (input == null)
            input = FindObjectOfType<InputMonitor>();
    }

    private void OnEnable()
    {
        InputMonitor.StartTouch += SwipeStart;
        InputMonitor.EndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        InputMonitor.StartTouch -= SwipeStart;
        InputMonitor.EndTouch -= SwipeEnd;
    }


    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        trail.transform.position = position;
        trail.SetActive(true);
        coroutine = StartCoroutine(Trail());
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
        
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = input.PrimaryPosition();
            yield return null;
        }
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
            (endTime - startTime) <= maximumTime)
        {
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private IEnumerator ResetInput()
    {
        yield return new WaitForSeconds(inputThreshold);
        yAxis = 0;
        xAxis = 0;
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            xAxis = 0;
            yAxis = 1;

        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            xAxis = 0;
            yAxis = -1;
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            yAxis = 0;
            xAxis = -1;
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            yAxis = 0;
            xAxis = 1;
        }

        StartCoroutine(ResetInput());
    }

}
