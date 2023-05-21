using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemigo;
    public GameObject spawner;
    public float enemyNumber = 3;
    public float enemyToSpawn;
    public float aliveEnemy;
    public float wave;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("spawner");
        //enemigo = GameObject.FindWithTag("enemigo");
        //enemigo = (GameObject)Resources.Load("prefabs/zombie 03_3", typeof(GameObject));
        enemyToSpawn = enemyNumber; 
        aliveEnemy = enemyNumber;
        wave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyToSpawn <= 0)
        {
            StartCoroutine(spawnZombie());
            enemyToSpawn = enemyToSpawn - 1;
            if(enemigo.GetComponent<Target>().health == 0)
            {
                aliveEnemy = aliveEnemy - 1;
            }
        }
        if(aliveEnemy <= 0)
        {
            Debug.Log("nueva ronda");
            StartCoroutine(newWave());
        }
    }

    IEnumerator newWave()
    {
        wave = wave + 1;
        Debug.Log(wave);
        yield return new WaitForSeconds(3f);
        enemyToSpawn = enemyNumber * wave;
        aliveEnemy = enemyToSpawn;
    }

    IEnumerator spawnZombie()
    {
        float randomNumber = Random.Range(1, 10);
        yield return new WaitForSeconds(randomNumber);
        Instantiate(enemigo, spawner.transform.position, spawner.transform.rotation);
    }
}
