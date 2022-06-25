using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HorizontalSnapScrolling : MonoBehaviour
{
    public ScrollRect scrollRect;
    public ScrollViewSystem scrollViewSystem;
    public float[] snapPositions;
    public float lerpSpeed;
    public float leftBuffer;
    public float rightBuffer;

    private void Update()
    {
        if (scrollRect.horizontalNormalizedPosition < leftBuffer && !scrollViewSystem.leftButton.isDown && !scrollViewSystem.rightButton.isDown)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition,snapPositions[0], lerpSpeed);
        }
        else if (scrollRect.horizontalNormalizedPosition > rightBuffer && !scrollViewSystem.rightButton.isDown && !scrollViewSystem.leftButton.isDown)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, snapPositions[2], lerpSpeed);
        }
        else if (scrollRect.horizontalNormalizedPosition < rightBuffer && scrollRect.horizontalNormalizedPosition > leftBuffer && !scrollViewSystem.rightButton.isDown && !scrollViewSystem.leftButton.isDown)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, snapPositions[1], lerpSpeed);
        }
    }

    ////public float width;
    ////public float leftPadding;
    ////public float rightPadding;
    ////public GameObject containers;
    ////public float containerWidth;
    ////public int numberOfElements;
    ////public HorizontalLayoutGroup view;
    ////public float snappingPosition;

    //private ScrollRect scrollRect;

    //[SerializeField] private ScrollButton leftButton;
    //[SerializeField] private ScrollButton rightButton;

    //public float[] snappingPositions;
    //public int scrollIndex = 1;

    ////public float GetSnapPosition()
    ////{
    ////    var itemWidth = width - (leftPadding + rightPadding);
    ////    if (numberOfElements % 2 > 0)
    ////    {
    ////        return(itemWidth / numberOfElements);
    ////    }
    ////    else
    ////    {
    ////        return (itemWidth / numberOfElements)/2;
    ////    }
    ////}

    ////private void Start()
    ////{
    ////    width = view.GetComponent<RectTransform>().rect.width;
    ////    leftPadding = view.padding.left;
    ////    rightPadding = view.padding.right;
    ////    containerWidth = containers.GetComponent<RectTransform>().rect.width;
    ////    snappingPosition = GetSnapPosition();
    ////    scrollRect = GetComponent<ScrollRect>();
    ////}

    //private void Update()
    //{


    //    if (leftButton != null)
    //    {
    //        if (leftButton.isDown)
    //        {
    //            if (scrollIndex >= 0 && scrollIndex <= snappingPositions.Length)
    //            {
    //                scrollIndex -= 1;

    //                SnapLeft(scrollIndex);
    //            }
    //        }
    //    }
    //    if (rightButton != null)
    //    {
    //        if (rightButton.isDown)
    //        {
    //            if (scrollIndex >= 0 && scrollIndex <= snappingPositions.Length)
    //            {
    //                scrollIndex += 1;

    //                SnapRight(scrollIndex);
    //            }
    //        }
    //    }
    //}

    //private void SnapLeft(int index)
    //{
    //    //var horizontalScrollPosition = scrollRect.horizontalNormalizedPosition;
    //    //var targetPosition = snappingPosition/width;
    //    //targetPosition = 0.5f-targetPosition;
    //    //targetPosition = 0 + targetPosition;

    //    if (scrollRect.horizontalNormalizedPosition >= 0f)
    //    {
    //        scrollRect.horizontalNormalizedPosition = snappingPositions[index];
    //    }
    //}

    //private void SnapRight(int index)
    //{
    //    //var horizontalScrollPosition = scrollRect.horizontalNormalizedPosition;
    //    //var targetPosition = snappingPosition / width;
    //    //targetPosition = 0.5f - targetPosition;
    //    //targetPosition = 1 - targetPosition;

    //    if (scrollRect.horizontalNormalizedPosition <= 1f)
    //    {
    //        scrollRect.horizontalNormalizedPosition = snappingPositions[index];
    //    }
    //}
}
