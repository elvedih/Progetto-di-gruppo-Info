using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterStats;

    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnetRange;

    public List<GameObject> spawnedWeapons;

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    

    void Awake()
    {
        characterStats = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        currentHealth = characterStats.MaxHealth;
        currentRecovery = characterStats.Recovery;
        currentMoveSpeed = characterStats.MoveSpeed;
        currentMight = characterStats.Might;
        currentProjectileSpeed = characterStats.ProjectileSpeed;
        currentMagnetRange = characterStats.MagnetRange;

        spawnWeapon(characterStats.StartingWeapon);
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach(LevelRange range in levelRanges)
            {
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            currentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
       
    }

    public void Kill()
    {
        Debug.Log("PLAYER DEATH");
    }

    public void RestoreHealth(float amount)
    {
        if (currentHealth < characterStats.MaxHealth)
        {
            currentHealth += amount;

                if (currentHealth > characterStats.MaxHealth)
                {
                    currentHealth = characterStats.MaxHealth;
                }
        }

       
        
    }

    public void Recover()
    {
        if (currentHealth < characterStats.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
        }
    }

    public void spawnWeapon(GameObject weapon)
    {
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        } else if (isInvincible)
        {
            isInvincible = false;
        }

        if (currentHealth > characterStats.MaxHealth)
        {
            currentHealth = characterStats.MaxHealth;
        }

        Recover();
    }

    
}
