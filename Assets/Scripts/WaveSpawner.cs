using UnityEngine;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour {

    public List<GameObject> enemyPrefabs;  
    public Transform spawnPoint;    
    public float minSpawnInterval = 2f;   
    public float maxSpawnInterval = 20f;  
    public int enemyCount = 5;      

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private System.Collections.IEnumerator SpawnEnemies()
    {
        WaitForSeconds waitTime = new WaitForSeconds(maxSpawnInterval);

        for (int i = 0; i < enemyCount; i++)
        {
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
