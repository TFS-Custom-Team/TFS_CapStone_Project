using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public int currentWave;
    public int EnemiesPerWave;
    public int enemiesSpawned;

    public int[] minEnemiesPerWave;
    public int[] maxEnemiesPerWave;

   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNextWave()
    {
        int maxWaves = 10;
        
        if(currentWave <= maxWaves)
        {
            currentWave++;

            //need to spawn enemies
            //spawn a random amount of enemies per round - corutine.

        } else
        {
            Debug.Log("Reached max waves");
        }

        
    }
}
