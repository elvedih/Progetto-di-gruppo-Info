using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{
    KnifeController kc;
    private void Awake()
    {
        kc = FindAnyObjectByType<KnifeController>();
    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * kc.speed * Time.deltaTime;
    }
}
