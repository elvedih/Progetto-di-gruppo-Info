using UnityEngine;

[CreateAssetMenu(fileName ="EnemyScriptableObject", menuName ="ScriptableObjects/Enemy")]

public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float damage;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
}
