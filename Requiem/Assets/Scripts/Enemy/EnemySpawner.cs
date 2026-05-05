using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //lista con gruppo di nemici da far spawnare in questa wave
        public int waveQuota; //total enemies spawning
        public float spawnInterval; // intervallo di sapwn degli enemies
        public int spawnCount;// numero di nemici già spawnati in questa wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; //quanti enemies spawnano in questa wave
        public int spawnCount; // il numero di enemies dello stesso tipo già spawnati nella wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves; // lista di tutte le wave del gioco
    public int currentWaveCount; // indice della wave corrente

    [Header("Spawner Attributes")]
    float spawnTimer;//timer per determinare quando spawnare i prossimi nemici
    public int enemiesAlive;
    public int maxEnemiesAllowed;//nume di nemici max accettati sulla mappa 
    public bool maxEnemiesReached = false;//flag per indicare se è stato raggiunto il numero massimo di nemici
    public float waveInteraval; //intervallo ffra ciascuna wave


    [Header ("Spawn Positions")]
    public List<Transform> relativeSpawnPoints; //lista per immagazzinare i relativi spawn points

    Transform player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    void Update()
    {
         
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) // contorllo se la wave è terminata e se la prossima wave deve cominciare
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.deltaTime;

        //controllo per vedere se è il momento di spawnare il nemico successivo
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
        {

            //wave per "waveInterval" secondi prima di incominciare con la prossima wave
            yield return new WaitForSeconds(waveInteraval);

            //se ci sono più waves da far partire dopo quella corrente, passa alla prossima wave
            if (currentWaveCount < waves.Count - 1)
            {
                currentWaveCount++;
                CalculateWaveQuota();
            }
        }
    

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach ( var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }


    //tutto questo metodo (SpawnEnemies) smette di spawnare nemici se il numero di nemici sulla mappa ha raggiunto il massimo.
    //Questo metodo spawnerà solamente nemici di una wave in parrticolare finchè non arriva il momento della wave successiva 
    void SpawnEnemies()
    {
           //controllo per vedere se il minimo di nemici è stato spawnato nella wave
           if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
           {
                //spawnare ogni gruppo di nemici finchè quota non è completo
                foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
                {
                    //controllo se il numero minimo di nemici di questo tipo è stato spawnato 
                    if (enemyGroup.spawnCount <enemyGroup.enemyCount)
                    {
                        //limite per il numero di nemici che possono spawnare
                        if (enemiesAlive >= maxEnemiesAllowed)
                        {
                            maxEnemiesReached = true;
                            return;
                        }

                    //spawna il nemico in una posizione random vicino al player
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    //Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(-10f, 10f), player.transform.position.y + Random.Range(-10f, 10f));
                    //Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity );

                    enemyGroup.spawnCount++;
                        waves[currentWaveCount].spawnCount++;
                        enemiesAlive++;
                    }
                }
           }

        //reset della flag maxEnemiesReached se il numero di nemici vivi è al di sotto del max  
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }


    //chiamare questa funzione quando un nemico viene ucciso
    public  void OnEnemyKilled()
    {
        //decrementa il numero dei nemici vivi
        enemiesAlive--;
    }
}
 