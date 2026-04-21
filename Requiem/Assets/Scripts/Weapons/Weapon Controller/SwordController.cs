using UnityEngine;

public class SwordController : WeaponController
{
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInParent<Animator>();
    }

    protected override void Attack()
    {
        base.Attack();

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
}