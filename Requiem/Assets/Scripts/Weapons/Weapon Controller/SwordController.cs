using UnityEngine;

public class SwordController : WeaponController
{
    private Animator animator;
    private SlashHitboxController hitbox;

    protected override void Awake()
    {
        base.Awake();
        hitbox = GetComponentInChildren<SlashHitboxController>();
    }

    protected override void Start()
    {
        base.Start();
        animator = GetComponentInParent<Animator>();
    }

    protected override void Attack()
    {
        base.Attack();

        if (animator != null)
            animator.SetTrigger("Attack");

        if (hitbox != null)
            hitbox.DealDamage();
    }
}