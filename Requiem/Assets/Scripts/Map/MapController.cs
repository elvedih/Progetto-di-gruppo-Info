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

    void Start()
    {
        SpawnChunksAroundPlayer(); // ← seed the world immediately
    }

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
        if (dir.magnitude < 0.1f) return; // still fine here — only skips extra checks while idle

        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                Vector2Int grid = currentGrid + new Vector2Int(x, y);
                SpawnChunkAt(grid);
            }
        }

        RemoveFarChunks(currentGrid);
    }

    // Called once at startup, no guards needed
    void SpawnChunksAroundPlayer()
    {
        Vector3 playerPos = player.transform.position;

        Vector2Int currentGrid = new Vector2Int(
            Mathf.RoundToInt(playerPos.x / chunkSize),
            Mathf.RoundToInt(playerPos.y / chunkSize)
        );

        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                SpawnChunkAt(currentGrid + new Vector2Int(x, y));
            }
        }
    }

    void SpawnChunkAt(Vector2Int grid)
    {
        if (spawnedChunks.ContainsKey(grid)) return;

        Vector3 pos = new Vector3(Mathf.Round(grid.x * chunkSize),Mathf.Round(grid.y * chunkSize),0);

        GameObject newChunk = Instantiate(
            terrainChunks[Random.Range(0, terrainChunks.Count)],
            pos,
            Quaternion.identity
        );

        spawnedChunks.Add(grid, newChunk);
    }

    void RemoveFarChunks(Vector2Int playerGrid)
    {
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();

        foreach (var chunk in spawnedChunks)
        {
            if (Vector2Int.Distance(playerGrid, chunk.Key) > 3)
            {
                Destroy(chunk.Value);
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (var pos in chunksToRemove)
            spawnedChunks.Remove(pos);
    }
}