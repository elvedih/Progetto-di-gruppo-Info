using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
    Dictionary<Vector2Int, GameObject> spawnedChunks = new Dictionary<Vector2Int, GameObject>();
    public float chunkSize = 20f;
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    [SerializeField] private PlayerMovement pm;
    public GameObject currentChunk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();

    }

    void ChunkChecker()
    {
        if (!currentChunk) return;

        Vector3 playerPos = player.transform.position;

        Vector2Int currentGrid = new Vector2Int(
            Mathf.RoundToInt(playerPos.x / chunkSize),
            Mathf.RoundToInt(playerPos.y / chunkSize)
        );

        Vector2 dir = pm.moveDir.normalized;

        if (dir.magnitude < 0.1f) return;

        Vector2Int gridDir = new Vector2Int(
            Mathf.RoundToInt(dir.x),
            Mathf.RoundToInt(dir.y)
        );

        Vector2Int targetGrid = currentGrid + gridDir;

        if (!spawnedChunks.ContainsKey(targetGrid))
        {
            Vector3 spawnPos = new Vector3(
                targetGrid.x * chunkSize,
                targetGrid.y * chunkSize,
                0
            );

            GameObject newChunk = Instantiate(
                terrainChunks[Random.Range(0, terrainChunks.Count)],
                spawnPos,
                Quaternion.identity
            );

            spawnedChunks.Add(targetGrid, newChunk);
        }

        RemoveFarChunks(currentGrid);
    }

    void RemoveFarChunks(Vector2Int playerGrid)
    {
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();

        foreach (var chunk in spawnedChunks)
        {
            float distance = Vector2Int.Distance(playerGrid, chunk.Key);

            if (distance > 2) // adjust this number!
            {
                Destroy(chunk.Value);
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var pos in chunksToRemove)
        {
            spawnedChunks.Remove(pos);
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(1, terrainChunks.Count);

        if (terrainChunks[rand] == null)
        {
             Debug.LogError("Elemento NULL nella lista terrainChunks!");
             return;
        }

            Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
    }
}

    
