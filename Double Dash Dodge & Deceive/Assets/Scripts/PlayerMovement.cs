using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // This is the input system package for WASD movement

public class PlayerMovement : MonoBehaviour
{
// Making the player sprite be affected by gravity
    public Rigidbody2D rigidBody;

// Animator class initialization
    Animator animator;
    // Player speed is 5 units per second
    public float speed = 5f;

// Stores input for horizontal movement
    float xAxisMovement;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
    // Updates the velocity of the player and maintains the vertical velocity
        rigidBody.velocity = new Vector2 (xAxisMovement * speed, rigidBody.velocity.y);

    // Updates the animator component with the player's velocity
        animator.SetFloat("xVelocity", Math.Abs(rigidBody.velocity.x));
    }

    // This method is called by an input action like WASD or arrow keys 
    public void Move(InputAction.CallbackContext context)
    {
    // Vector2 describes the X and Y position of a game object
        xAxisMovement = context.ReadValue<Vector2>().x;
    }
}
