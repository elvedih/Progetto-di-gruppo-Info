using UnityEngine;

public class KnifeController : WeaponController
{
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.prefab, transform.position, Quaternion.identity);
        spawnedKnife.transform.position = transform.position;
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastDirection);
        KnifeBehaviour kb = GetComponent<KnifeBehaviour>();
    }
}
