using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static ColorSwitching;

public class WaveManager : MonoBehaviour
{

    public int currentWave;
    public int enemiesSpawned;
    public GameObject enemyPrefab; //need to assign the enemy prefab
    public Transform spawnPoint; //spawnpoint position

    public int[] minEnemiesPerWave;
    public int[] maxEnemiesPerWave;

    public float spawnDelay = 1.0f; // Delay between enemy spawns (in seconds) initalize

    

    // Start is called before the first frame update
    void Start()
    {
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
        int maxWaves = 10;
        
        if(currentWave <= maxWaves)
        {
            currentWave++; //increments the current wave

            //determines the number of enemies to spawn for the current wave using 'random.Range' to randomize the amount of enemies spawned
            int enemiesToSpawn = Random.Range(minEnemiesPerWave[currentWave], maxEnemiesPerWave[currentWave] + 1);
            //start the coroutine, with the determined number of enemies from the routine below
            StartCoroutine(SpawnEnemies(enemiesToSpawn));
            

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
        //Instantiates the enemy prefab at the spawn point and rotation (do I need the spawn point rotation?)
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        switch (Random.Range(0,3))
        {
            case 0:
                enemy.GetComponent<pointAndShoot>().swapColor(Colors.Red);
                break;
            case 1:
                enemy.GetComponent<pointAndShoot>().swapColor(Colors.Blue);
                break;
            case 2:
                enemy.GetComponent<pointAndShoot>().swapColor(Colors.Green);
                break;
            case 3:
                enemy.GetComponent<pointAndShoot>().swapColor(Colors.Black);
                break;
        }
        enemiesSpawned++;
    }
}
