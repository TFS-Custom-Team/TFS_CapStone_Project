using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static ColorSwitching;

public class WaveManager : MonoBehaviour
{
    
    public int currentWave;
    public int maxWaves = 10;
    public int enemiesSpawned;
    public GameObject enemyPrefab; //need to assign the enemy prefab
    public Transform spawnPoint; //spawnpoint position
    private IEnumerator coroutine;

    public int startingEnemiesPerWave;
    public int enemyGrowthPerWave;

    public float spawnDelay = 1.0f; // Delay between enemy spawns (in seconds) initalize

    

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        currentWave = 0;
        enemiesSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //We can use this for testing
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartNextWave();
        }
    }

    public void StartNextWave() 
    {
        
        if(currentWave <= maxWaves)
        {

            //determines the number of enemies to spawn for the current wave using the starting amount of enemies and increasing the amount based on the current wave
            //Also increments cuurent wave
            int enemiesToSpawn = startingEnemiesPerWave + enemyGrowthPerWave*currentWave++;
            //start the coroutine, with the determined number of enemies from the routine below
            coroutine = SpawnEnemies(enemiesToSpawn);
            StartCoroutine(coroutine);
            

        } else
        {
            Debug.Log("Reached max waves");
        }

        
    }

    private IEnumerator SpawnEnemies(int enemiesToSpawn)
    {
        //loops through the number of enemies to spawn 
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            //calls the spawnenemy function and then waits for the spawn delay seconds before spawning the next enemy
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        //Instantiates the enemy prefab near the spawn point and rotation (do I need the spawn point rotation?)
        float xOffset = UnityEngine.Random.Range(-1f, 1f);
        float yOffset = UnityEngine.Random.Range(-1f, 1f);
        Vector2 spawn = (Vector2)spawnPoint.position + new Vector2(xOffset, yOffset);

        GameObject enemy = Instantiate(enemyPrefab, spawn, spawnPoint.rotation);
        pointAndShoot temp = enemy.GetComponent<pointAndShoot>();
        Debug.Log(temp == null ? "spawned enemy does not have a point and shoot component" : 0);
        switch (UnityEngine.Random.Range(0,4))
        {
            case 0:
                temp.swapColor(Colors.Red);
                break;
            case 1:
                temp.swapColor(Colors.Green);
                break;
            case 2:
                temp.swapColor(Colors.Blue);
                break;
            case 3:
                temp.swapColor(Colors.Black);
                break;
        }
        enemiesSpawned++;
    }
}
