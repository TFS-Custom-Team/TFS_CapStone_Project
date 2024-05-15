using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorSwitching;

public class pointAndShoot : MonoBehaviour
{
    private GameObject player;
    public GameObject projectilePrefab;
    public float shootingCooldown = 5;
    public Colors col = Colors.Blank;
    SpriteRenderer sr;

    float timer;
    // Start is called before the first frame update
    // Nicholas H
    // 5/1/2024
    // Place whichever projectile we end up using as the projectilePrefab
    void Awake()
    {
        col = Colors.Green;
        player = GameObject.FindGameObjectWithTag("Player");
        timer = 0;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            // Find the difference in position to get the angle using trig
            Vector2 playerPos = player.transform.position;
            Vector2 ownPos = transform.position;

            // Find the difference in position to get the angle using trig
            float xDif = playerPos.x - ownPos.x;
            float yDif = playerPos.y - ownPos.y;


            float pointingAngle = Mathf.Rad2Deg * Mathf.Atan2(xDif, yDif);
            transform.eulerAngles = new Vector3(0f, 0f, -pointingAngle);
            //points towards the player using pointingAngle float

            // ###############################################
            // shoot based on cooldown
            if (projectilePrefab != null)
            {
                timer += Time.deltaTime;
                //Debug.Log(timer);
                if (timer > shootingCooldown)
                {
                    timer = 0;
                    GameObject projectile = Instantiate(projectilePrefab, ownPos, Quaternion.identity);
 
                    //projectile.GetComponent<projectilePlaceholderScript>().colour = gameObject.GetComponent<enemyPlaceholderScript>().colour;
                    projectile.GetComponent<Projectile>().shoot(transform.up, 1, 1, col, gameObject, ownPos);
                }
            }
        }
    }
    public void swapColor(ColorSwitching.Colors color)
    {
        col = color;

        switch (color)
        {
            case Colors.Red:
                sr.color = Color.red;
                break;
            case Colors.Green:
                sr.color = Color.green;
                break;
            case Colors.Blue:
                sr.color = Color.blue;
                break;
            case Colors.Black:
                sr.color = Color.black;
                break;
        }
    }
}
