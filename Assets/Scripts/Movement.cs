using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObject;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float MoveDirection;
    private bool isJumping = false;
    private bool isGrounded = false;

    // Double jump variables
    private int jumpCount = 0;
    public int maxJumps = 2;

    // Ground check radius for overlap detection
    public float groundCheckRadius = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Keeps the character upright
    }

    void Update()
    {
        // Get horizontal movement
        MoveDirection = Input.GetAxis("Horizontal");

        // Update ground check to determine if character is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundObject);

        // Reset jump count when grounded
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Check for jump input and if jumps are available
        if (Input.GetButtonDown("Jump") && (isGrounded || jumpCount < maxJumps))
        {
            isJumping = true;
            jumpCount++; // Increment jump count after each jump
        }

        // Flip character if needed
        if (MoveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (MoveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }

        // Set horizontal velocity
        rb.velocity = new Vector2(MoveDirection * MoveSpeed, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        // Apply jump force if jump was triggered
        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical velocity before jumping
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            isJumping = false; // Reset jump flag after applying force
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

