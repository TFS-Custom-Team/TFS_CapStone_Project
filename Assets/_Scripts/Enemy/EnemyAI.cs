using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public Projectile projectile;
    
    public float moveSpeed = 5f;
    public float detectionRadius = 10f;

    private float lastShootTime = 0; // Last time the enemy shot

    void Update()
    {
        if (player != null)
        {
            // Calculating the direction from the enemy to the player
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize(); // Normalize the vector to ensure constant speed in all directions

            // Moving the enemy towards the player
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);

            // Checking if Player is within detection radius
            if (directionToPlayer.magnitude <= detectionRadius)
            {
                             
            }
        }
        if (player != null && (Time.time - lastShootTime > 5))
        {
            lastShootTime = Time.time;
            Shoot();
            
        }
    }

    /// <summary>
    /// Creates and initializes a projectile to shoot directly at the player. 
    /// </summary>
    void Shoot()
    {
        Projectile temp = Instantiate(projectile);
        Vector3 projectileSpawn = transform.position;
        projectileSpawn += (player.transform.position - transform.position).normalized;
        temp.shoot((player.transform.position - transform.position), 2, 1, ColorSwitching.Colors.Red, this.gameObject, projectileSpawn);
        Debug.Log(player.transform.position);

    }

    
}