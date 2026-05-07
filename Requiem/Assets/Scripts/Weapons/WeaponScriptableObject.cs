using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "Scriptable Objects/WeaponScriptableObject")]

public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

    public float Damage { get => damage; private set => damage = value; }
    public float Speed { get => speed; private set => speed = value; }
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    float speed;
    [SerializeField]
    float damage;
    [SerializeField]
    float cooldownDuration;
    [SerializeField]
    int pierce;
    [SerializeField]
    int level;
    public int Level { get => level; private set => level = value; }
    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }
    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }
}
