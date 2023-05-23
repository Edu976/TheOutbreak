using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{

    public GameObject enemigo = GameObject.FindWithTag("enemigo").transform.GetChild(0).gameObject;
    public int spawnNumber;
    public List<GameObject> enemyPrefabs;
    public Transform spawnPoint;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 20f;
    public int enemyCount;
    public float currentEnemyCount;
    public int roundNumber = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        enemyCount = 5;
        currentEnemyCount = enemyCount * spawnNumber;
        WaitForSeconds waitTime = new WaitForSeconds(maxSpawnInterval);

        for (int i = 0; i < enemyCount; i++)
        {
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
        enemyCount = enemyCount * 2;
    }

    public void enemyDiscount()
    {
        if(this.enemigo.GetComponent<Target>().health <= 0f)
        {
            currentEnemyCount--;
            if(currentEnemyCount == 0)
            {
                roundNumber++;
                Debug.Log(roundNumber);
            }
        }
    }

}
