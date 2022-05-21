using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tile;
    public int columns, rows;
    public float x, y;
    public GameObject parent;


    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var gameObject = Instantiate(tile, new Vector2(x+i,y+j), Quaternion.identity, parent.transform);
                gameObject.name = $"{x+i},{y+j}";
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
