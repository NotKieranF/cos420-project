using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // This is the input system package for WASD movement

public class PlayerMovement : MonoBehaviour
{

    //reference for menu
    public GameObject MainMenu;

    // Internal component references
    private Rigidbody2D m_RigidBody;
    private Collider2D m_Collider;
    private Animator m_Animator;
    
    // Player movement parameters
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float jumpCancelFactor = 0.5f;
    [SerializeField] private float wallSlideSpeed = 3f;
    [SerializeField] private float wallJumpVelocity = 8f;
    
    [SerializeField] private LayerMask wallLayer;

    // Stores input for horizontal movement
    private float xAxisMovement;

    private bool isFacingRight = false;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isTouchingWall = false;
    private bool isWallSliding = false;
    private bool menuPressed = true;
    private Vector2 wallNormal;


    // Start is called before the first frame update
    void Start()
    {
        // Get references to internal components
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for groundedness
        CheckGround();

        // Check if touching wall
        CheckWall();

        // Updates the velocity of the player and maintains the vertical velocity
        m_RigidBody.velocity = new Vector2 (xAxisMovement * speed, m_RigidBody.velocity.y);
        
        // Updates the animator component with the player's velocity
        m_Animator.SetFloat("xVelocity", Math.Abs(m_RigidBody.velocity.x));
        m_Animator.SetFloat("yVelocity", m_RigidBody.velocity.y);

        FlipAnimation();

        WallSlide();
        
    }

    // This method is called by an input action like WASD or arrow keys 
    public void Move(InputAction.CallbackContext context)
    {
        // Vector2 describes the X and Y position of a game object
        xAxisMovement = context.ReadValue<Vector2>().x;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Jump input started, check if grounded and jump
            if (isGrounded)
            {
                isJumping = true;
                m_RigidBody.velocity += Vector2.up * jumpVelocity;
                m_Animator.SetBool("isJumping", isJumping);
            }
            else if (isWallSliding)
            {
                WallJump();
            }
        }
        else if (context.canceled)
        {
            // Jump input released, reduce upwards velocity if jump is released early
            if (m_RigidBody.velocity.y > 0)
            {
                m_RigidBody.velocity *= new Vector2(1.0f, jumpCancelFactor);
            }
            isJumping = false;
            m_Animator.SetBool("isJumping", isJumping);
        }
    }

    void WallJump()
    {
        Vector2 jumpDirection = wallNormal + Vector2.up;
        isJumping = true;
        m_RigidBody.velocity = new Vector2(jumpDirection.x * wallJumpVelocity, jumpDirection.y * jumpVelocity);
        m_Animator.SetBool("isJumping", isJumping);
    }
    void CheckWall()
    {
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, m_Collider.bounds.extents.x + 0.1f, wallLayer)
                     || Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, m_Collider.bounds.extents.x + 0.1f, wallLayer);
        if (isTouchingWall)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, m_Collider.bounds.extents.x + 0.1f, wallLayer);
            if (hit)
                wallNormal = hit.normal;
        }
    }
    void WallSlide()
    {
        if (isTouchingWall && !isGrounded && m_RigidBody.velocity.y < 0)
        {
            isWallSliding = true;
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }
        m_Animator.SetBool("isWallSliding", isWallSliding);
    }

    void CheckGround()
    {
        Vector2 min = new Vector2(m_Collider.bounds.min.x + 0.05f, m_Collider.bounds.min.y - 0.05f);
        Vector2 max = new Vector2(m_Collider.bounds.max.x - 0.05f, m_Collider.bounds.min.y - 0.05f);
        isGrounded = Physics2D.OverlapArea(min, max) != null;
        //Debug.DrawLine(min, max, Color.red);
    }

    void FlipAnimation()
    {
        // If the character is moving right, flip them to the right, and vice versa
        if (isFacingRight && xAxisMovement > 0f || !isFacingRight && xAxisMovement < 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 ls = transform.localScale;

            ls.x *= -1f;

            transform.localScale = ls;

        }
    }

    public void Pause(){

        MainMenu.SetActive(true);
        Time.timeScale = 0;

    }

    public void Continue(){

        MainMenu.SetActive(false);
        Time.timeScale = 1;

    }

}
