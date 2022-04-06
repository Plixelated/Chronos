using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    [ExecuteInEditMode]
    void OnRenderObject()
    {
        if (transform.position.x != Mathf.RoundToInt(transform.position.x))
        {
            transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), transform.position.y);
        }
        if (transform.position.y != Mathf.RoundToInt(transform.position.y))
        {
            transform.position = new Vector2(transform.position.x, Mathf.RoundToInt(transform.position.y));
        }
    }
}
