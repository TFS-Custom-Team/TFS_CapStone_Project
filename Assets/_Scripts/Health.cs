using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using Unity.VisualScripting;

using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public int hearts = 3;

    public int health = 50;
    private int maxHealth;
    public int damageTaken = 0;
    public float hitInvulnerabilityTime = 2;
    public UnityEvent onDeath;
    private float timer = 0;
    [SerializeField] private AudioClip clip;

    

    public bool noHealthCap = false;
    bool hitInvulnerability = false;

    // These are temporary placeholders
    float ownColour = 1;
    float projectileColour = 2;

    // Start is called before the first frame update
    // Nicholas H
    // 5/7/2024
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
        timer += Time.deltaTime;
        // To damage the entity. Make a pointer towards damageTaken and add to it.
        if (damageTaken > 0)
        {
            // applies to the player
            if (gameObject.tag == "Player")
            { 
                // if the colour doesn't match and you're not invincible, you get hurt.
                if (hitInvulnerability == false || (hitInvulnerability == false && ownColour != projectileColour))
                {
                    takeDamage();
                    switch (hearts)
                    {
                        case 1:
                            hearts--;
                            health3.SetActive(false);
                            break;   
                        case 2:
                            hearts--;
                            health2.SetActive(false);
                            break;
                            case 3:
                            hearts--;
                            health1.SetActive(false); 
                            break;
                    }


                    AudioSource.PlayClipAtPoint(clip, transform.position, 1f);

                    hitInvulnerability = true;
                    timer = 0;
                } else
                {
                    damageTaken = 0; // remove illegal damage
                    if (ownColour == projectileColour)
                    {
                        //reflect projectile placeholder
                        //projectile.GetComponent<projectilePlaceholderScript>().trajectory = -(projectile.GetComponent<projectilePlaceholderScript>().trajectory);
                    }
                }
            }
            else // if the damaged entity is not the player
            {
                takeDamage();
            }


            // Just in case there is a healing mechanic. This prevents any unwanted effects
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            // if the health is below or zero, destroy the gameObject
            if (health <= 0)
            {
                killEntity();
            }
        }
        // Remove hit invulnerability after a time
            if (timer > hitInvulnerabilityTime && hitInvulnerability == true)
            {
            hitInvulnerability = false;
            timer = 0;
            Debug.Log("Invincibility Off");
            }
    }

    void killEntity()
    {
        onDeath.Invoke();
		Destroy(gameObject);
    }
    void takeDamage()
    {
        health -= damageTaken;
        damageTaken = 0;
    }
}
