using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Salute " + currentHealth;
                }
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Ripristino " + currentRecovery;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Velocità " + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Forza " + currentMight;
                }
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Velocità Proiettili " + currentProjectileSpeed;
                }
            }
        }
    }

    public float CurrentMagnetRange
    {
        get { return currentMagnetRange; }
        set
        {
            if (currentMagnetRange != value)
            {
                currentMagnetRange = value;
                currentMagnetRange = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnete  " + currentMagnetRange;
                }
            }
        }
    }
    #endregion

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

    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public Text levelText;

    void Awake()
    {
        characterStats = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();

        CurrentHealth = characterStats.MaxHealth;
        CurrentRecovery = characterStats.Recovery;
        CurrentMoveSpeed = characterStats.MoveSpeed;
        CurrentMight = characterStats.Might;
        CurrentProjectileSpeed = characterStats.ProjectileSpeed;
        CurrentMagnetRange = characterStats.MagnetRange;

        SpawnWeapon(characterStats.StartingWeapon);
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.instance.currentHealthDisplay.text = "Salute " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Ripristino " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Velocità " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Forza " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Velocità Proiettili " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnete  " + currentMagnetRange;

        GameManager.instance.AssignChosenCharacterUI(characterStats);

        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
        UpdateExpBar();
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

            UpdateLevelText();

            GameManager.instance.StartLevelUp();
        }
    }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
            CurrentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if (CurrentHealth <= 0)
            {
                Kill();
            }

            UpdateHealthBar();
        }
    }

    public void Kill()
    {
        GameManager.instance.AssingLevelReachedUI(level);
        GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.passiveItemUISlots, inventory.weaponUISlots);
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.GameOver();
        }
    }

    public void RestoreHealth(float amount)
    {
        if (CurrentHealth < characterStats.MaxHealth)
        {
            CurrentHealth += amount;

                if (CurrentHealth > characterStats.MaxHealth)
                {
                    CurrentHealth = characterStats.MaxHealth;
                }
        }
    }

    public void Recover()
    {
        if (CurrentHealth < characterStats.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        Debug.Log($"Trying to spawn: {weapon?.name}, weaponIndex: {weaponIndex}, slots: {inventory.weaponSlots.Count}");
        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.Log("Inventory slots full");
            return;
        }

        Debug.Log($"Spawning: {weapon.name}");
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        Debug.Log($"WeaponController found: {spawnedWeapon.GetComponent<WeaponController>()}");
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (weaponIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.Log("Inventory slots full");
            return;
        }
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        weaponIndex++;
        passiveItemIndex++;
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

        if (CurrentHealth > characterStats.MaxHealth)
        {
            CurrentHealth = characterStats.MaxHealth;
        }

        Recover();
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterStats.MaxHealth;
    }

    void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText()
    {
        levelText.text = "LV " + level.ToString();
    }
}
