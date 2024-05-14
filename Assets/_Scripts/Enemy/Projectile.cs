using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static ColorSwitching;

public class Projectile : MonoBehaviour
{
    
    //tracks  direction of travel
    Vector2 dir;
    float speed = 1;
    float size = 1;
    float laserWidth = 0.4f;
    //Tracks reference to the enemy that spawned this projectile
    GameObject enemy;
    //Tracks the color of the projectile
    public ColorSwitching.Colors color = ColorSwitching.Colors.Blank;

    Rigidbody2D rb;
    SpriteRenderer sr;
    LineRenderer lr;

    Gradient line;
    


    // Start is called before the first frame update
    //Start handles default instantiation. Enemy controllers should call the Shoot function, but by default the projectile will shoot toward the player.
    void Awake()
    {
        //Will work when GameManager created
        //dir = GameManager.Player.transform.position - transform.position;
        //Until then:
        dir = Vector2.zero - (Vector2)transform.position;
        dir.Normalize();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = dir * speed;
    }
    /// <summary>
    /// To be called immediately after spawning.
    /// Sets normalized direction vector for projectile to travel in, speed of projectile, size of projectile, and stores the enemy who shot the projectile (This enemy)
    /// 
    /// </summary>
    /// <param name="direction">Vector2 representing direciton for projectile to travel in</param>
    /// <param name="spd">speed of projectile</param>
    /// <param name="sz">size of projectile</param>
    /// <param name="col">Color of the projectile</param>
    /// <param name="shooter">the GameObject calling this function</param>
    /// <param name="pos">The Vector3 of where the enemy wants the projectile to spawn</param>
    public void shoot(Vector2 direction, float spd, float sz, ColorSwitching.Colors col,GameObject shooter, Vector3 pos)
    {
        dir = direction;
        dir.Normalize();
        speed = spd;
        size = sz;
        color = col;
        enemy = shooter;

        switch (col)
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


        transform.position = pos;

        Debug.Log("dir = " + dir);
        Debug.Log("direction = " + direction);


    }
    /// <summary>
    /// Shoots beam back at the enemy who shot this projectile, then destroys this. All the Gradient stuff sucks but I didnt see a good way around it. If you see one, fix it. 
    /// </summary>
    public void Bounce()
    {
        Debug.Log("Bounce Ran");
        //Spawn Beam
        //Gets the points of the projectile on collision and the enemy to draw a laser between
        var points = new Vector3[2];
        points[0] = transform.position;
        points[1] = enemy.transform.position;


        //Builds the gradients that the laser's color is based on. 
        var gradAlphaOn = new GradientAlphaKey[2];
        gradAlphaOn[0] = new GradientAlphaKey(1.0f, 0.0f);
        gradAlphaOn[1] = new GradientAlphaKey(1.0f, 1.0f);

        var gradAlphaOff = new GradientAlphaKey[2];
        gradAlphaOff[0] = new GradientAlphaKey(0.0f, 0.0f);
        gradAlphaOff[1] = new GradientAlphaKey(0.0f, 1.0f);


        var gradRed = new GradientColorKey[2];
        gradRed[0] = new GradientColorKey(Color.red, 0.0f);
        gradRed[1] = new GradientColorKey(Color.red, 1.0f);
        Gradient redGrad = new Gradient();
        redGrad.SetKeys(gradRed, gradAlphaOn);

        var gradGreen = new GradientColorKey[2];
        gradGreen[0] = new GradientColorKey(Color.green, 0.0f);
        gradGreen[1] = new GradientColorKey(Color.green, 1.0f);
        Gradient greenGrad = new Gradient();
        greenGrad.SetKeys(gradGreen, gradAlphaOn);


        var gradBlue = new GradientColorKey[2];
        gradBlue[0] = new GradientColorKey(Color.blue, 0.0f);
        gradBlue[1] = new GradientColorKey(Color.blue, 1.0f);
        Gradient blueGrad = new Gradient();
        blueGrad.SetKeys(gradBlue, gradAlphaOn);

        var gradBlack = new GradientColorKey[2];
        gradBlack[0] = new GradientColorKey(Color.black, 0.0f);
        gradBlack[1] = new GradientColorKey(Color.black, 1.0f);
        Gradient blackGrad = new Gradient();
        blackGrad.SetKeys(gradBlack, gradAlphaOn);

        //Changes the color Gradient of the laser
        switch (color)
        {
            case Colors.Red:
                lr.colorGradient = redGrad;
                break;
            case Colors.Green:
                lr.colorGradient = greenGrad;
                break;
            case Colors.Blue:
                lr.colorGradient = blueGrad;
                break;
            case Colors.Black:
                lr.colorGradient = blackGrad;
                break;
        }



        //sets parameters of laser to make it visible
        lr.SetPositions(points);
        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;

        //Disables components not to be used anymore
        sr.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        //Effects (camera shake, particles, etc)
        //Damage enemy

        //Waits for laser then destroys projectile
        StartCoroutine(DestroyAfterlaser());

        enemy.GetComponent<Health>().damageTaken = 1;
    }

    private IEnumerator DestroyAfterlaser()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.transform.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.transform.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            if (player.GetComponent<ColorSwitching>().currColor == color)
            {
                Bounce();
            }
            else
            {
                player.GetComponent<Health>().damageTaken = 1;
                Destroy(gameObject);
            }
        }
    }

}
