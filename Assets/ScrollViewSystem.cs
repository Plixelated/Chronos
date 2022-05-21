using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    private ScrollRect scrollRect;

    [SerializeField] private ScrollButton leftButton;
    [SerializeField] private ScrollButton rightButton;
    [SerializeField] private ScrollButton topButton;
    [SerializeField] private ScrollButton bottomButton;

    [SerializeField] private float scrollSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leftButton != null)
        {
            if (leftButton.isDown)
            {
                ScrollLeft();
            }
        }
        if (rightButton != null)
        {
            if (rightButton.isDown)
            {
                ScrollRight();
            }
        }
        if (bottomButton != null)
        {
            if (bottomButton.isDown)
            {
                ScrollDown();
            }
        }
        if (topButton != null)
        {
            if (topButton.isDown)
            {
                ScrollUp();
            }
        }
    }

    private void ScrollLeft()
    {
        if (scrollRect.horizontalNormalizedPosition >= 0f)
        {
            scrollRect.horizontalNormalizedPosition -= scrollSpeed;
        }
    }
    private void ScrollRight()
    {
        if (scrollRect.horizontalNormalizedPosition <= 1f)
        {
            scrollRect.horizontalNormalizedPosition += scrollSpeed;
        }
    }
    private void ScrollDown()
    {
        if (scrollRect.horizontalNormalizedPosition >= 0f)
        {
            scrollRect.horizontalNormalizedPosition -= scrollSpeed;
        }
    }
    private void ScrollUp()
    {
        if (scrollRect.horizontalNormalizedPosition <= 1f)
        {
            scrollRect.horizontalNormalizedPosition += scrollSpeed;
        }
    }
}
