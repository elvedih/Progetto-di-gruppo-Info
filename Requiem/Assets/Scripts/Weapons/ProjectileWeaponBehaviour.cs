using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;
    private Vector3 lastDirection;
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
        lastDirection = Vector3.right;
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 45f);
    }
}
