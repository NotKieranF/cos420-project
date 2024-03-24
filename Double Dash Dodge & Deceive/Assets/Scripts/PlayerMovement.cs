using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // This is the input system package for WASD movement

public class PlayerMovement : MonoBehaviour
{
    // Internal component references
    private Rigidbody2D m_RigidBody;
    private Collider2D m_Collider;
    private Animator m_Animator;
    
    // Player movement parameters
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float jumpCancelFactor = 0.5f;
    public float wallSlideSpeed = 2;
    bool isWallSliding;
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);

    // Stores input for horizontal movement
    private float xAxisMovement;

    private bool isFacingRight = false;
    private bool isGrounded = false;
    private bool isJumping = false;

    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
    public LayerMask wallLayer;

    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallGravityMult = 2f;

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

        ProcessGravity();
        ProcessWallSlide();
        ProcessWallJump();

        if (!isWallJumping)
        {
            // Updates the velocity of the player and maintains the vertical velocity
            m_RigidBody.velocity = new Vector2(xAxisMovement * speed, m_RigidBody.velocity.y);
            // Updates the animator component with the player's velocity
            m_Animator.SetFloat("xVelocity", Math.Abs(m_RigidBody.velocity.x));
            FlipAnimation();
        }
    }

    // This method is called by an input action like WASD or arrow keys 
    public void Move(InputAction.CallbackContext context)
    {
        // Vector2 describes the X and Y position of a game object
        xAxisMovement = context.ReadValue<Vector2>().x;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValue<float>() > 0;
        if (isJumping && isGrounded)
        {
            m_RigidBody.velocity += Vector2.up * jumpVelocity;
        }
        else if (m_RigidBody.velocity.y > 0)
        {
            // Reduce upwards velocity if jump is released early
            m_RigidBody.velocity *= new Vector2(1.0f, jumpCancelFactor);
        }
        
        // Wall Jumping lasts 0.5 seconds, jump again after 0.6 seconds
        if(context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            m_RigidBody.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;

            if(transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f); 
        }

    }
    private void ProcessWallSlide()
    {
        if (!isGrounded & WallCheck() & xAxisMovement != 0)
        {
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f)
        {

            wallJumpTimer -= Time.deltaTime;
        }
    }
    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f) {
        
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump() {

        isWallJumping = false;
    }
    void CheckGround()
    {
        Vector2 min = new Vector2(m_Collider.bounds.min.x + 0.05f, m_Collider.bounds.min.y - 0.05f);
        Vector2 max = new Vector2(m_Collider.bounds.max.x - 0.05f, m_Collider.bounds.min.y - 0.05f);
        isGrounded = Physics2D.OverlapArea(min, max) != null;
        //Debug.DrawLine(min, max, Color.red);
    }

    private void ProcessGravity()
    {
        if (m_RigidBody.velocity.y < 0)
        {
            m_RigidBody.gravityScale = baseGravity * fallGravityMult;
            m_RigidBody.velocity = new Vector2(m_RigidBody.velocity.x, Mathf.Max(m_RigidBody.velocity.y, -maxFallSpeed));
        }
        else
        {
            m_RigidBody.gravityScale = baseGravity;
        }
    }
    private bool WallCheck()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
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
}
