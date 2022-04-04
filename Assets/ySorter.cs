using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ySorter : MonoBehaviour
{

    public int y_pos;
    public int y_sort;

    public bool child;

    // Start is called before the first frame update
    void Start()
    {
        if (!child)
        {
            this.y_pos = Mathf.FloorToInt(this.transform.position.y);
            this.GetComponent<SpriteRenderer>().sortingOrder = -y_pos;
            y_sort = -y_pos;
        }
        if (child)
        {
            this.y_pos = Mathf.FloorToInt(this.GetComponentInParent<Transform>().position.y);
            this.GetComponentInParent<SpriteRenderer>().sortingOrder = (int)-y_pos;
            y_sort = -y_pos;
        }
    }
}
