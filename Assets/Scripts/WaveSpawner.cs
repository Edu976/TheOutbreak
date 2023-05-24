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
    public float maxSpawnInterval = 3f;
    public int enemyCount;
    public static float currentEnemyCount;
    public static int roundNumber;

    private void Start()
    {
        spawnNumber = 1;
        roundNumber = 0;
        enemyCount = 5;
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds waitTime = new WaitForSeconds(maxSpawnInterval);

        for (int i = 0; i < enemyCount; i++)
        {
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject enemyPrefab = enemyPrefabs[randomIndex];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            currentEnemyCount = currentEnemyCount + 1;
            Debug.Log("enemigos restantes" + currentEnemyCount);
            yield return new WaitForSeconds(spawnInterval);
            //enemigo = GameObject.FindWithTag("enemigo").transform.GetChild(0).gameObject;
        }
        enemyCount = enemyCount * 2;

        /*
                if(this.enemigo.GetComponent<Target>().health <= 0f)
                {
                    Debug.Log("Entramos");
                    currentEnemyCount--;
                    Debug.Log(currentEnemyCount);
                    if(currentEnemyCount == 0)
                    {
                        roundNumber++;
                        Debug.Log(roundNumber);
                    }
                }
                */
    }

    public void enemigoMuerto()
    {
        Debug.Log("enemigos restantes" + currentEnemyCount);
        Debug.Log("Entramos");
        currentEnemyCount = currentEnemyCount - 1;
        if (currentEnemyCount == 0)
        {
            roundNumber++;
            Debug.Log(roundNumber);
        }
    }

}
