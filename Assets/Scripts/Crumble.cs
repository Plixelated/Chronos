using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{
    public float crumbleTimer;
    public float timer;
    // Start is called before the first frame update

    public void SetTimer()
    {
        if (timer == 0)
        {
            this.timer = this.crumbleTimer;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            if (this.timer > 0)
            {
                if (this.timer > 0)
                {
                    this.timer -= Time.deltaTime;
                    if (this.timer <= 0)
                    {
                        this.timer = 0;
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

}
