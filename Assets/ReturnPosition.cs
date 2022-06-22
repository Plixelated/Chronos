using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPosition : MonoBehaviour
{
    public Vector2 mousePosition;
    public Vector2 tilePosition;
    public Collider2D tileCollider;
    public static Action<Vector2> selectedPosition;

    private void OnEnable()
    {
        InputMonitor.StartTouch += GetMousePosition;
    }

    private void GetMousePosition(Vector2 position, float time)
    { 
        mousePosition = position;

        RaycastHit2D hit = Physics2D.BoxCast(position, new Vector2(0.01f, 0.01f), 0f, Vector2.zero);

        if (hit.collider == tileCollider)
        {
            DisplayPosition();
        }
    }

    private void DisplayPosition()
    {
        tilePosition.x = Mathf.Round(mousePosition.x);
        tilePosition.y = Mathf.Round(mousePosition.y);
        Debug.Log($"{this.name}'s position: {tilePosition}");
        Broadcaster.Send(selectedPosition, tilePosition);
    }

}
