using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public float scrollSpeed;
    public float length;
    private float startpos;

    private void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Scroll()
    {

        Vector2 currentPos = transform.position;
        currentPos.x -= scrollSpeed * Time.deltaTime;
        transform.position = currentPos;

        if (transform.position.x < -(length/2))
        {
            Reposition();
        }

    }

    private void Update()
    {
        Scroll();

    }

    private void Reposition()
    {
        transform.position = new Vector2(transform.position.x + length, transform.position.y);
    }

}
