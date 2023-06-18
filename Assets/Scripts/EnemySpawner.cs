using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=hI7zH3OE8Y8
// Mezclar con este https://www.youtube.com/watch?v=Rdj1ZW-ylDg

// Esta clase controla el spawn de los enemigos y el control de las oleadas

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> spawnPoints;
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

    // Spawnea un numero de enemigos indicado desde el inspector de unity
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

    /*
        En este método se indica el lugar en el que va a spawnear cada uno delos enemigos lanzando 
        un random index que segun cual sea indicara un punto de spawn o otro 
    */
    Vector3 findSpawnLoc()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No hay puntos de spawn definidos en la lista spawnPoints.");
            return Vector3.zero;
        }

        int randomIndex = Random.Range(0, spawnPoints.Count);
        Vector3 spawnPosition = spawnPoints[randomIndex].transform.position;

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