using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbleTile : Tile
{
    public float crumbleTimer;
    public float timer;
    public Animator tileAnimator;
    public Transform initalScale;
    public bool resetting;

    // Start is called before the first frame update

    public override void Effect()
    {
        this.SetTimer();
    }

    public override void OnReset()
    {
        this.resetting = false;
    }

    private void ResetAnimations()
    {
        if (!resetting)
        {
            if (tileAnimator.GetBool("falling"))
                tileAnimator.SetBool("falling", false);
            if (tileAnimator.GetBool("shaking"))
                tileAnimator.SetBool("shaking", false);
        }
    }

    private void ChangeAnimationState()
    {
        tileAnimator.SetBool("falling", true);
        tileAnimator.SetBool("shaking", false);
    }


    public void SetTimer()
    {
        if (this.timer == 0)
        {
            this.timer = this.crumbleTimer;
            Debug.Log(this.name + " has Activated");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!this.resetting)
        {
            if (this.gameObject.activeSelf)
            {
                if (this.timer > 0)
                {
                    this.timer -= Time.deltaTime;
                    if (!tileAnimator.GetBool("shaking"))
                        tileAnimator.SetBool("shaking", true);

                    if (this.timer <= 0.5f)
                    {
                        ChangeAnimationState();
                    }

                    if (this.timer <= 0)
                    {
                        this.timer = 0;
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            this.resetting = false;
            this.timer = 0;
        }
    }

}
