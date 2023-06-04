using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=hI7zH3OE8Y8

public class EnemySpawner : MonoBehaviour
{
    public Text waveNumberDisplay;
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 3f;
    [System.Serializable]
    public class WaveContent
    {
        [SerializeField] GameObject[] EnemySpawner;
        public GameObject[] getEnemySpawnList()
        {
            return EnemySpawner;
        }
    }

    [SerializeField] WaveContent[] waves;
    private int curentWave = 0;
    float spawnRange = 10;
    public List<GameObject> currentEnemies;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        waveNumberDisplay.text = (curentWave + 1).ToString();
        if (currentEnemies.Count == 0)
        {
            curentWave++;
            StartCoroutine(spawnWave());
        }
    }

    public IEnumerator spawnWave()
    {
        //yield return new WaitForSeconds(5);
        for (int i = 0; i < waves[curentWave].getEnemySpawnList().Length; i++)
        {
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            GameObject newSpawn = Instantiate(waves[curentWave].getEnemySpawnList()[i], findSpawnLoc(), Quaternion.identity);
            currentEnemies.Add(newSpawn);

            EnemyAI enemy = newSpawn.GetComponent<EnemyAI>();
            enemy.setSpawner(this);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 findSpawnLoc()
    {
        Vector3 spawnPosition;

        float xLoc = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zLoc = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float yLoc = transform.position.y;

        spawnPosition = new Vector3(xLoc, yLoc, zLoc);

        if (Physics.Raycast(spawnPosition, Vector3.down, 5))
        {
            return spawnPosition;
        }
        else
        {
            return findSpawnLoc();
        }
    }
}