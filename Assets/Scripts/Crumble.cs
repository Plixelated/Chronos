using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{
    public float crumbleTimer;
    public float timer;
    public Animator tileAnimator;
    public Transform initalScale;

    // Start is called before the first frame update

    private void OnEnable()
    {
        GameManager.hasReset += ResetAnimations;
    }

    private void OnDisable()
    {
        GameManager.hasReset -= ResetAnimations;
    }

    private void ResetAnimations()
    {
        tileAnimator.SetBool("falling", false);
        tileAnimator.SetBool("shaking", false);
        timer = 0;
    }

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
                    if (!tileAnimator.GetBool("shaking"))
                        tileAnimator.SetBool("shaking", true);

                    if (this.timer <= 0)
                    {
                        this.timer = 0;
                        tileAnimator.SetBool("falling", true);
                        tileAnimator.SetBool("shaking", false);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }

}
