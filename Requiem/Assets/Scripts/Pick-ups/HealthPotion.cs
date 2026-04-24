using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectible
{

    public int healthToRestore;

    public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
        Destroy(gameObject);
    }

}
