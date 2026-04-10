using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.moveDir.x == 0 && pm.moveDir.y == 0)
        {
            am.SetBool("Move", false);
            
        }
        else
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
    }

    void SpriteDirectionChecker()
    {
        if (pm.moveDir.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
