using Unity.VisualScripting;
using UnityEngine;

public class SlashHitboxController : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected float currentDamage;

    void Awake()
    {
        currentDamage = weaponData.Damage;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
        transform.position,
        GetComponent<Collider2D>().bounds.size,
        0f
    );

        foreach(var col in hits)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyStats enemy = col.GetComponent<EnemyStats>();
                enemy.TakeDamage(currentDamage);
            }
        }
    }
}
