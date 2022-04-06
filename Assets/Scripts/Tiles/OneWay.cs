using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWay : MonoBehaviour
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    public GameObject blocker;

    private void Start()
    {
        if (up)
        {
            Vector3 rotation = new Vector3(0f, 0f ,90f);
            Quaternion q = Quaternion.Euler(rotation);
            blocker.transform.rotation = q;

            blocker.transform.localPosition = new Vector3(0f, 0.45f,0f);
        }

        if (down)
        {
            Vector3 rotation = new Vector3(0f, 0f, 90f);
            Quaternion q = Quaternion.Euler(rotation);
            blocker.transform.rotation = q;

            blocker.transform.localPosition = new Vector3(0f, -0.45f, 0f);
        }

        if (left)
        {
            Vector3 rotation = new Vector3(0f, 0f, 0f);
            Quaternion q = Quaternion.Euler(rotation);
            blocker.transform.rotation = q;

            blocker.transform.localPosition = new Vector3(-0.45f, 0f, 0f);
        }

        if (right)
        {
            Vector3 rotation = new Vector3(0f, 0f, 0f);
            Quaternion q = Quaternion.Euler(rotation);
            blocker.transform.rotation = q;

            blocker.transform.localPosition = new Vector3(0.45f, 0f, 0f);
        }
    }
}
