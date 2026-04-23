using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;
    

    protected PlayerMovement pm;
    protected virtual void Awake()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
    }

    protected virtual void Start()
    {
        Debug.Log("WeaponData = " + weaponData);
        Debug.Log("Prefab = " + (weaponData != null ? weaponData.Prefab : null));

        currentCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
