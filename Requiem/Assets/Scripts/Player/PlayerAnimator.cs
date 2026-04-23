using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;
    [SerializeField]
    Collider2D slashHitbox;
    Vector3 pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
        slashHitbox.enabled = false;
        pos = slashHitbox.transform.position;
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
        else if (pm.moveDir.x > 0)
        {
            sr.flipX = false;
        }
        float dir = pm.moveDir.x < 0 ? -1f : 1f;
        slashHitbox.transform.position = transform.position + new Vector3(0.8f * dir, 0f, 0f);
    }

    void EnableHitbox()
    {
        slashHitbox.enabled = true;
        slashHitbox.GetComponent<SlashHitboxController>().DealDamage();
    }

    void DisableHitbox()
    {
        slashHitbox.enabled = false;
    }
}
