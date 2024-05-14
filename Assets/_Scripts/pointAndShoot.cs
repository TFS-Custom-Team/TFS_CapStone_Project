using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointAndShoot : MonoBehaviour
{
    private GameObject player;
    public GameObject projectilePrefab;
    public float shootingCooldown = 5;

    float timer;
    // Start is called before the first frame update
    // Nicholas H
    // 5/1/2024
    // Place whichever projectile we end up using as the projectilePrefab
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timer = 0;
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
                Debug.Log(timer);
                if (timer > shootingCooldown)
                {
                    timer = 0;
                    GameObject projectile = Instantiate(projectilePrefab, ownPos, Quaternion.identity);
                    // optionally here, we can instantiate the projectile's angle while also using the "pointingAngle" float.


                    // THESE DON'T EXIST YET. SO THEY ARE PLACEHOLDERS
                    // WE DON'T HAVE ENEMY WITH COLOURS YET, SO IDK HOW IT WILL ACTUALLY WORK.

                    //projectile.GetComponent<projectilePlaceholderScript>().colour = gameObject.GetComponent<enemyPlaceholderScript>().colour;
                    //projectile.GetComponent<projectilePlaceholderScript>().trajectory = transform.up;
                }
            }
        }
    }
}
