using UnityEngine;
using System.Collections.Generic;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    PlayerMovement pm;
    public GameObject currentChunk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();

    }

    void ChunkChecker()
    {

        if (!currentChunk)
        {
            return;
        }


        if (pm.moveDir.x > 0 && pm.moveDir. y == 0) //destra
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(20,0,0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(20,0,0);
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir. y == 0) //sinistra
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-20,0,0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-20,0,0);
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir. y > 0) //in alto
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, 20,0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(0, 20,0);
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir. y < 0 ) //in basso
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(0,-20,0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(0,-20,0);
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir. y > 0) //in alto a destra
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(20, 20,0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(20, 20,0);
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir. y < 0 ) //in basso a destra
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(20,-20,0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(20,-20,0);
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir. y < 0 ) //in alto a sinistra
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-20,20,0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-20,20,0);
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir. y > 0) //in basso a sinistra
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-20, -20,0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-20, -20,0);
                SpawnChunk();
            }
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

    
