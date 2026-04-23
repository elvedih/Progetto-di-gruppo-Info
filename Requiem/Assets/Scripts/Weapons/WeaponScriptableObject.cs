using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "Scriptable Objects/WeaponScriptableObject")]

public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; set => prefab = value; }

    public float Damage { get => damage; set => damage = value; }
    public float Speed { get => speed; set => speed = value; }
    public float CooldownDuration { get => cooldownDuration; set => cooldownDuration = value; }
    public int Pierce { get => pierce; set => pierce = value; }

    [SerializeField]
    float speed;
    [SerializeField]
    float damage;
    [SerializeField]
    float cooldownDuration;
    [SerializeField]
    int pierce;
}
