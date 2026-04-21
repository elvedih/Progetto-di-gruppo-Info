using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "Scriptable Objects/WeaponScriptableObject")]

public class WeaponScriptableObject : ScriptableObject
{
    public GameObject prefab;
    //base stats for weapons
    public float damage;
    public float speed;
    public float cooldownDuration;
    public int pierce;
}
