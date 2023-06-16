using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] enemies;
    public int waveCount;
    public int wave;
    public int enemyType;
    private int currentEnemies;
    public bool spawning;
    private int enemySpawned;

    //private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        waveCount = 2;
        wave = 1;
        spawning = false;
        enemySpawned = 0;
        currentEnemies = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawning == false && currentEnemies == 0)
        {
            StartCoroutine(spawnWave(waveCount));
        }
    }

    IEnumerator spawnWave(int waveC)
    {
        spawning = true;
        yield return new WaitForSeconds(4);
        for(int i = 0; i < waveC; i++)
        {
            spawnEnemy(wave);
            yield return new WaitForSeconds(2);
        }
        wave += 1;
        waveCount +=2;
        spawning = false;
        yield break; 
    }

    void spawnEnemy(int wave)
    {
        
    }
}
