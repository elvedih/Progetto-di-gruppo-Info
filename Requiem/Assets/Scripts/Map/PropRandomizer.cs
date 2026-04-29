using UnityEngine;
using System.Collections.Generic;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {

        /*foreach(GameObject sp in propSpawnPoints ){
                int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.localPosition = new Vector3(0, 0, -1f);
            prop.transform.localRotation = Quaternion.identity;
        }*/
        foreach (GameObject sp in propSpawnPoints)
        {
            Vector3 pos = sp.transform.position;
            pos.z = -1f;

            if (propPrefabs.Count == 0) continue;

            int rand = Random.Range(0, propPrefabs.Count);

            // 1. Spawna il prop nella posizione esatta dello SpawnPoint
            // Usiamo la posizione e rotazione del punto di spawn (World Space)
            GameObject prop = Instantiate(propPrefabs[rand], pos, sp.transform.rotation);

            // 2. Imposta il parent al CHUNK (this.gameObject), NON allo spawn point.
            // Questo evita problemi di scala se lo spawn point è ruotato o scalato.
            prop.transform.SetParent(this.transform);

            // 3. OPZIONALE: Se sono ancora leggermente "dentro" il terreno, 
            // alziamoli di un soffio sulla Z (se 2D) o sulla Y (se 3D)
            // Esempio 2D: prop.transform.position += new Vector3(0, 0, -0.1f);
        }
    }
}
