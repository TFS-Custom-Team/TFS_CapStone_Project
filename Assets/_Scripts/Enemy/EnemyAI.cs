using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float detectionRadius = 10f;

    private float lastShootTime; // Last time the enemy shot

    void Update()
    {
        if (player != null)
        {
            // Calculating the direction from the enemy to the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize(); // Normalize the vector to ensure constant speed in all directions

            // Moving the enemy towards the player
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);

            // Checking if Player is within detection radius
            if (directionToPlayer.magnitude <= detectionRadius)
            {
                             
            }
        }
    }

    
}