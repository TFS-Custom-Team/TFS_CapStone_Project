using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Controller set up - Joshua
    //Player Variables
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5.0f;
    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //setting up movement and normalizing the vector
        moveInput.x = Input.GetAxisRaw("Horizontal");   //setting up moveInput 'x' for horizontal input
        moveInput.y = Input.GetAxisRaw("Vertical");     //setting up moveInput 'y' for vertical input
        moveInput.Normalize();                          //normalize moveInput vector

        rb.velocity = moveInput * moveSpeed;            //setting up rigidbody velocity
    }
}
