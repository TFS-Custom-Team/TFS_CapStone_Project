using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 50;
    private int maxHealth;
    public int damageTaken = 0;

    public bool noHealthCap = false;
    public bool hitInvulnerability = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!noHealthCap)
        {
            maxHealth = health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // To damage the entity. Make a pointer towards damageTaken and add to it.
        if (damageTaken > 0)
        {
            health -= damageTaken;
            damageTaken = 0;
            // if the health is below or zero, destroy the gameObject
            if (health <= 0)
            {
                killEntity();
            }
            // (hitInvulnerability) if the entity with this script is damaged, makes them temporarily invincible
            if (hitInvulnerability)
            {
                // not yet implemented
            }
        }
        // Just in case there is a healing mechanic. This prevents any unwanted effects
        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }

    void killEntity()
    {
        Destroy(gameObject);
    }
}
